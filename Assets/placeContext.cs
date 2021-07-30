using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeContext : MonoBehaviour
{
	public string filePath;
	public GameObject terrain;
	internal Vector2Int spawnPos;
	internal Vector2Int foodPos;
	// Start is called before the first frame update
	void Awake()
	{
		string fileData = System.IO.File.ReadAllText(filePath);
		string[] splitFileData = fileData.Split('\n');
		string[] spawnPosS = splitFileData[0].Split(',');
		string[] foodPosS = splitFileData[1].Split(',');
		this.spawnPos = new Vector2Int((int)Double.Parse(spawnPosS[0].Replace("e", "E"), System.Globalization.NumberStyles.Float),
			(int)Double.Parse(spawnPosS[1].Replace("e", "E"), System.Globalization.NumberStyles.Float));
		this.foodPos = new Vector2Int((int)Double.Parse(foodPosS[0].Replace("e", "E"), System.Globalization.NumberStyles.Float),
			(int)Double.Parse(foodPosS[1].Replace("e", "E"), System.Globalization.NumberStyles.Float));
		float[,] heightMap = terrain.GetComponent<showHeightMap>().heightMap;
		//Debug.Log(heightMap);

		this.transform.Find("antNest").localPosition = new Vector3(spawnPos.y, 33 * heightMap[(int)(spawnPos.x), (int)(spawnPos.y)], spawnPos.x);
		this.transform.Find("apple").localPosition = new Vector3(foodPos.y, 33 * heightMap[(int)(foodPos.x), (int)(foodPos.y)], foodPos.x);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
