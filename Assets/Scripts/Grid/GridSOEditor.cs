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
                    var grid = gridSO.gridRows[y].row[x];
                    EditorGUILayout.BeginVertical(GUILayout.Width(0));
                    grid.type = (GridType)EditorGUILayout.EnumPopup(GUIContent.none, grid.type, GUILayout.Width(60));
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("HP", GUILayout.Width(20));
                    grid.HP = EditorGUILayout.IntField(grid.HP, GUILayout.Width(40));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();

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