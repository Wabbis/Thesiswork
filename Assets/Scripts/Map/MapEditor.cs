using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Pathfinding;
[CustomEditor(typeof(MapGeneration))]
public class MapEditor : Editor
{
	public override void OnInspectorGUI()
	{
		
		MapGeneration map = (MapGeneration)target;
		DrawDefaultInspector();
		
		if(GUILayout.Button("Generate"))
		{
			map.DeleteMap();
			map.GenerateMap();
			AstarPath.FindAstarPath();
			AstarPath.active.Scan();
		}
	}
}
