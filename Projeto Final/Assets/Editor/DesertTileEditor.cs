using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DesertTile))]
public class DesertTileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DesertTile desertTile = (DesertTile)target;

        // Exibe os campos padrão do inspetor
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate Mesh"))
        {
            desertTile.RegenerateMesh();
        }

        // Regenera o mesh automaticamente quando os valores mudam no editor
        if (GUI.changed)
        {
            desertTile.RegenerateMesh();
        }
    }

}
