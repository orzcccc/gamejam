using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public float cellDistance = 0.8f;    // 格子间距
    public float size = 6f;            // 格子缩放大小（1 = 原图大小）

    [Header("Grid Data")]
    public GridSO gridData;           // 地图配置数据

    [Header("GridType Sprite List")]
    public List<Sprite> typeSprites;  // GridType -> Sprite 映射（顺序需一致）

    void Start()
    {
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
        {
            renderer.sprite = typeSprites[index];
        }
        else
        {
            Debug.LogWarning($"GridManager: GridType {type} 没有对应的 Sprite！");
        }

        cell.transform.localScale = new Vector3(size, size, 1);
    }
    
    private Color GetColorByType(GridType type)
    {
        switch (type)
        {
            case GridType.Road: return Color.gray;
            case GridType.Block: return Color.white;
            case GridType.Water: return Color.blue;
            case GridType.Grass: return Color.green;
            default: return Color.white;
        }
    }
    private void OnDrawGizmosSelected()
    {
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
            int flippedY = rows - 1 - y; // 方向翻转保持一致

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
