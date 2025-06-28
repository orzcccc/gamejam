using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewGridConfig", menuName = "Grid/Grid Config")]
public class GridSO : ScriptableObject
{
    public int Level;
    public int rows = 5;
    public int columns = 5;

    public GridRow[] gridRows;

    [Serializable]
    public class GridRow
    {
        public Grid[] row;
    }

    public GridType GetGridType(int x, int y)
    {
        if (gridRows == null || y < 0 || y >= gridRows.Length || x < 0 || x >= gridRows[y].row.Length)
            return GridType.Block;

        return gridRows[y].row[x].type;
    }

    public void Resize(int newRows, int newColumns)
    {
        GridRow[] newGrid = new GridRow[newRows];

        for (int y = 0; y < newRows; y++)
        {
            newGrid[y] = new GridRow();
            newGrid[y].row = new Grid[newColumns];

            for (int x = 0; x < newColumns; x++)
            {
                // 初始化每个格子对象
                newGrid[y].row[x] = new Grid();

                if (gridRows != null &&
                    y < rows && x < columns &&
                    gridRows.Length > y && gridRows[y].row != null && gridRows[y].row.Length > x &&
                    gridRows[y].row[x] != null)
                {
                    newGrid[y].row[x].type = gridRows[y].row[x].type;
                    newGrid[y].row[x].HP = gridRows[y].row[x].HP;
                }
                else
                {
                    newGrid[y].row[x].type = GridType.Road;
                    newGrid[y].row[x].HP = 0;
                }
            }
        }

        rows = newRows;
        columns = newColumns;
        gridRows = newGrid;
    }

}