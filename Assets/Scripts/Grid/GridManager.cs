using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GridManager : SingletonMono<GridManager>
{
    [Header("Grid Settings")] 
    public float cellDistance = 0.8f; // 格子间距
    public float size = 6f; // 格子缩放大小

    [Header("Grid Data")] 
    public List<GridSO> gridDatas; // 多个地图配置数据（多个关卡）
    private GridSO gridData; // 当前使用的地图配置（缓存）
    private Dictionary<Vector2Int, GridBase> gridDic = new Dictionary<Vector2Int, GridBase>();
        
    [Header("GridType Sprite List")]
    public List<GameObject> prefabs = new List<GameObject>(); // GridType -> prefabs 映射
    private List<GameObject> spawnedCells = new(); // 记录已生成格子

    protected override void Awake()
    {
        base.Awake();
        ClearGrid(); // 避免残留
    }

    void Start()
    {
        // 默认加载第一个关卡
        if (gridDatas != null && gridDatas.Count > 0)
            StartLevel(0);
    }

    public void StartLevel(int index)
    {
        if (index < 0 || index >= gridDatas.Count)
        {
            Debug.LogError("GridManager: 关卡索引超出范围！");
            return;
        }

        gridData = gridDatas[index];
        ClearGrid();
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        if (gridData == null || gridData.gridRows == null)
        {
            Debug.LogWarning("GridManager: 缺少 Grid 数据！");
            return;
        }

        int rows = gridData.rows;
        int columns = gridData.columns;

        float gridWidth = columns * cellDistance;
        float gridHeight = rows * cellDistance;

        Vector2 startPos = new Vector2(
            -gridWidth / 2 + cellDistance / 2,
            -gridHeight / 2 + cellDistance / 2
        );

        for (int y = 0; y < rows; y++)
        {
            int flippedY = rows - 1 - y;

            for (int x = 0; x < columns; x++)
            {
                Vector2 pos = new Vector2(
                    startPos.x + x * cellDistance,
                    startPos.y + y * cellDistance
                );

                GridType type = gridData.GetGridType(x, flippedY);
                CreateCell(pos, x, flippedY, type);
            }
        }
    }

    void CreateCell(Vector2 position, int x, int y, GridType type)
    {
        int index = (int)type;

        if (index < 0 || index >= prefabs.Count || prefabs[index] == null)
        {
            Debug.LogWarning($"GridManager: GridType {type} 没有对应的 Prefab！");
            return;
        }

        GameObject prefab = prefabs[index];
        GameObject cell = Instantiate(prefab, position, UnityEngine.Quaternion.identity, this.transform);
        cell.name = $"Cell_{x}_{y}";
        cell.transform.localScale = new Vector3(size, size, 1);

        // 可选：记录或挂脚本
        spawnedCells.Add(cell);

        // 可选：保存到字典
        if (cell.TryGetComponent<GridBase>(out var gridBase))
        {
            Vector2Int coord = new Vector2Int(x, y);
            gridDic[coord] = gridBase;
            gridBase.Init(coord, type); // 你可以定义一个 Init 方法
        }
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        spawnedCells.Clear(); // 清空列表以便运行时再记录
        gridDic.Clear();
    }

    #region 编辑器模式
#if UNITY_EDITOR
    public int previewLevelIndex = 0; // 在 Inspector 中切换关卡
#endif
    private Color GetColorByType(GridType type)
    {
        return type switch
        {
            GridType.Road => Color.white,
            GridType.Block => Color.black,
            GridType.Food => Color.blue,
            GridType.Target => Color.green,
            _ => Color.white,
        };
    }

    private void OnDrawGizmosSelected()
    {
        if (gridDatas == null || gridDatas.Count == 0)
            return;

        if (previewLevelIndex < 0 || previewLevelIndex >= gridDatas.Count)
            return;

        gridData = gridDatas[previewLevelIndex]; // 根据预览索引获取 gridData
        if (gridData == null || gridData.gridRows == null)
            return;

        int rows = gridData.rows;
        int columns = gridData.columns;

        float gridWidth = columns * cellDistance;
        float gridHeight = rows * cellDistance;

        Vector2 startPos = new Vector2(
            -gridWidth / 2 + cellDistance / 2,
            -gridHeight / 2 + cellDistance / 2
        );

        for (int y = 0; y < rows; y++)
        {
            int flippedY = rows - 1 - y;

            for (int x = 0; x < columns; x++)
            {
                Vector2 center = new Vector2(
                    startPos.x + x * cellDistance,
                    startPos.y + y * cellDistance
                );
                GridType type = gridData.GetGridType(x, flippedY);
                Gizmos.color = GetColorByType(type);
                Gizmos.DrawCube(center, new Vector3(cellDistance * 0.95f, cellDistance * 0.95f, 0.1f));
            }
        }
    }
    

    #endregion
}