using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;


public class MapGeneration : MonoBehaviour
{
	public Renderer texRender;

	public int mapWidth;

	public List<GameObject> listOfMapObjects;

	public int mapHeight;
	public float scale;
	public bool autoUpdate;

	public RegionType[] regions;

	public void Start()
	{
		
	}
	public void GenerateMap()
	{
		float[,] noiseMap = NoiseMap.CreateNoiseMap(mapWidth, mapHeight, scale);
		//DrawNoiseMap(noiseMap);
		DrawColorMap(noiseMap);
	}
	public void DrawColorMap(float[,] noiseMap)
	{
		
		Color[] colorMap = new Color[mapWidth * mapHeight];

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float height = noiseMap[x, y];
				for (int i = 0; i < regions.Length; i++)
				{

					if (height < regions[i].height)
					{
						// colorMap[y * mapWidth + x] = regions[i].color;

						GameObject tile = Instantiate(regions[i].prefab, new Vector3(x*5, 0, y*5), Quaternion.identity);
						
						listOfMapObjects.Add(tile);
						break;
					}
				}
			}
		}


		Texture2D tex = new Texture2D(mapWidth, mapHeight);
		tex.SetPixels(colorMap);
		tex.Apply();
		Debug.Log("Color map done");
		texRender.sharedMaterial.mainTexture = tex;
		texRender.transform.localScale = new Vector3(mapWidth, 1, mapHeight);

		//	Debug.Log("----");
		//	for (int y = 0; y < mapHeight; y++)
		//	{
		//		for (int x = 0; x < mapWidth; x++)
		//		{
		//			Debug.Log(colorMap[y * mapWidth + x]);
		//		}
		//	}
	}

	public void DrawNoiseMap(float[,] noiseMap)
	{
		Texture2D tex = new Texture2D(mapWidth, mapHeight);

		Color[] colorMap = new Color[mapWidth * mapHeight];

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapHeight; x++)
			{
				colorMap[y * mapWidth + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
			}
		}

		tex.SetPixels(colorMap);
		tex.Apply();

		texRender.sharedMaterial.mainTexture = tex;
		texRender.transform.localScale = new Vector3(mapWidth, 1, mapHeight);
	}
	public void DeleteMap()
	{
		foreach (GameObject go in listOfMapObjects) 
		{
			DestroyImmediate(go);
		}

		listOfMapObjects.Clear();
	}

	private void OnDestroy()
	{
		listOfMapObjects.Clear();
	}
}
[System.Serializable]
public struct RegionType
{
	public string name;
	public float height;
	public Color color;
	public GameObject prefab;
}
