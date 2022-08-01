using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
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
			
		}
	}
}
