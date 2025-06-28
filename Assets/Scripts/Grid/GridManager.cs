using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonMono<GridManager>
{
    [Header("Grid Settings")] public float cellDistance = 0.8f; // 格子间距
    public float size = 6f; // 格子缩放大小

    [Header("Grid Data")] public List<GridSO> gridDatas; // 多个地图配置数据（多个关卡）
    private GridSO gridData; // 当前使用的地图配置（缓存）

    [Header("GridType Sprite List")] public List<Sprite> typeSprites; // GridType -> Sprite 映射

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
        GameObject cell = new GameObject($"Cell_{x}_{y}");
        cell.transform.position = position;
        cell.transform.parent = this.transform;

        SpriteRenderer renderer = cell.AddComponent<SpriteRenderer>();
        int index = (int)type;

        if (index >= 0 && index < typeSprites.Count)
            renderer.sprite = typeSprites[index];
        else
            Debug.LogWarning($"GridManager: GridType {type} 没有对应的 Sprite！");

        cell.transform.localScale = new Vector3(size, size, 1);

        spawnedCells.Add(cell); // 记录生成的格子
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        spawnedCells.Clear(); // 清空列表以便运行时再记录
    }


    ////////////////////////编辑器模式
#if UNITY_EDITOR
    public int previewLevelIndex = 0; // 在 Inspector 中切换关卡
#endif
    private Color GetColorByType(GridType type)
    {
        return type switch
        {
            GridType.Road => Color.white,
            GridType.Block => Color.black,
            GridType.Water => Color.blue,
            GridType.Grass => Color.green,
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
}