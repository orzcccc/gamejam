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
        public GridType[] row;
    }

    public GridType GetGridType(int x, int y)
    {
        if (gridRows == null || y < 0 || y >= gridRows.Length || x < 0 || x >= gridRows[y].row.Length)
            return GridType.Block;

        return gridRows[y].row[x];
    }

    public void Resize(int newRows, int newColumns)
    {
        GridRow[] newGrid = new GridRow[newRows];

        for (int y = 0; y < newRows; y++)
        {
            newGrid[y] = new GridRow();
            newGrid[y].row = new GridType[newColumns];

            for (int x = 0; x < newColumns; x++)
            {
                if (y < rows && x < columns && gridRows != null && gridRows.Length > y && gridRows[y].row.Length > x)
                {
                    newGrid[y].row[x] = gridRows[y].row[x];
                }
                else
                {
                    newGrid[y].row[x] = GridType.Road;
                }
            }
        }
        rows = newRows;
        columns = newColumns;
        gridRows = newGrid;
    }
}