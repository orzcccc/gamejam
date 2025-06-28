#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridSO))]
public class GridSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridSO gridSO = (GridSO)target;

        gridSO.Level = EditorGUILayout.IntField("Level", gridSO.Level);
        gridSO.rows = EditorGUILayout.IntField("Rows", gridSO.rows);
        gridSO.columns = EditorGUILayout.IntField("Columns", gridSO.columns);

        if (GUILayout.Button("Resize Grid"))
        {
            gridSO.Resize(gridSO.rows, gridSO.columns);
        }

        if (gridSO.gridRows != null &&
            gridSO.gridRows.Length == gridSO.rows)
        {
            for (int y = 0; y < gridSO.rows; y++)
            {
                if (gridSO.gridRows[y] == null || gridSO.gridRows[y].row == null || gridSO.gridRows[y].row.Length != gridSO.columns)
                    continue;

                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < gridSO.columns; x++)
                {
                    gridSO.gridRows[y].row[x] = (GridType)EditorGUILayout.EnumPopup(
                        GUIContent.none,
                        gridSO.gridRows[y].row[x],
                        GUILayout.Width(70)
                    );
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("请点击 'Resize Grid' 初始化数据", MessageType.Info);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(gridSO);
        }
    }
}
#endif