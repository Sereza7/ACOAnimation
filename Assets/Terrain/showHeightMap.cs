using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class showHeightMap : MonoBehaviour
{
	public string filePath;
	public float[,] heightMap;
	//private int SIZE = 5;
	// Start is called before the first frame update
	void Awake()
    {
		Terrain terrainComponent = this.GetComponent<Terrain>();
		string fileData = System.IO.File.ReadAllText(filePath);
		
		string[] splitFileData = fileData.Split('\n');
		float[,] heightMap = new float[splitFileData.GetLength(0)-1,splitFileData.GetLength(0)-1];
		float min = 1;
		float max = 0;
		for (int i = 0; i<splitFileData.GetLength(0)-1; i++)
		{
			string[] line = splitFileData[i].Split(',');
			for (int j = 0; j < line.GetLength(0); j++)
			{
				line[j] = line[j].Replace("e", "E");
				float value = (float)Double.Parse(line[j], System.Globalization.NumberStyles.Float);
				min = Math.Min(min, value);
				max = Math.Max(max, value);
				heightMap[i, j] = value;
			}
		}
		for (int i = 0; i < heightMap.GetLength(0); i++)
		{
			for (int j = 0; j < heightMap.GetLength(1); j++)
			{
				heightMap[i,j] = (heightMap[i, j] - min) / (max - min);
				//Debug.Log(heightMap[i, j]);
			}
		}
		this.heightMap = heightMap;
		terrainComponent.terrainData.SetHeights(0, 0, heightMap);
	}

    // Update is called once per frame
    void Update()
    {
    }
}
