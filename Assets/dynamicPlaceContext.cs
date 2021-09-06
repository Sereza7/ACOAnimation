using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class dynamicPlaceContext : MonoBehaviour
{
	public GameObject terrain;
	public GameObject cameraOrbit;
	internal Vector2Int spawnPos;
	internal Vector2Int foodPos;
	// Start is called before the first frame update
	void Awake()
	{
		int gridsize = this.terrain.GetComponent<Terrain>().terrainData.detailResolution;

		this.spawnPos = new Vector2Int(Random.Range(0, gridsize - 1), Random.Range(0, gridsize - 1));
		this.foodPos = new Vector2Int(Random.Range(0, gridsize - 1), Random.Range(0, gridsize - 1));
		while (Math.Sqrt(Math.Pow(foodPos[0] - spawnPos[0],2)+Math.Pow( foodPos[1] - spawnPos[1],2)) < (gridsize / Math.Sqrt(2)) + 1)
		{
			foodPos = new Vector2Int(Random.Range(0, gridsize - 1), Random.Range(0, gridsize - 1));
		}

		float[,] heightMap = new float[1, 1];
		try { heightMap = terrain.GetComponent<showHeightMap>().heightMap; }
		catch { heightMap = terrain.GetComponent<dynamicShowHeightMap>().heightMap; }

		this.transform.Find("antNest").localPosition = new Vector3(spawnPos.y, (int)(terrain.GetComponent<Terrain>().terrainData.size.y) * heightMap[(int)(spawnPos.x), (int)(spawnPos.y)], spawnPos.x);
		this.transform.Find("apple").localPosition = new Vector3(foodPos.y, (int)(terrain.GetComponent<Terrain>().terrainData.size.y) * heightMap[(int)(foodPos.x), (int)(foodPos.y)], foodPos.x);
		cameraOrbit.transform.localPosition = new Vector3(gridsize / 2, gridsize / 2, gridsize / 2);
	}
	
}
