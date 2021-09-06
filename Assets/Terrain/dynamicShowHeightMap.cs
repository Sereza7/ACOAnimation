using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class dynamicShowHeightMap : MonoBehaviour
{
	internal float[,] heightMap;
	static internal int SIZE = 5;
	static internal float RANDOMI = 0.5f; //The higher this value, and the more positive the map will be. Pretty much useless, just perform a translation if needed
	static internal float SMOOTH = 0.3f; //The lower the value, the lower the spikes on the terrain. 0.8 is terribly shacky, 0.1 is almost flat
	void Awake()
	{
		Terrain terrainComponent = this.GetComponent<Terrain>();
		this.generateHeightMap();
		terrainComponent.terrainData.heightmapResolution = heightMap.GetLength(0);
		terrainComponent.terrainData.size = new Vector3Int(heightMap.GetLength(0)-1, heightMap.GetLength(0) , heightMap.GetLength(1)-1);
		terrainComponent.terrainData.SetDetailResolution(heightMap.GetLength(0) , heightMap.GetLength(0));
		

		terrainComponent.terrainData.SetHeights(0, 0, heightMap);
	}

	private int mod(int x, int m)
	{
		return (x % m + m) % m;
	}

	void generateHeightMap()
	{
		
		float min = 1;
		float max = 0;

		float[,] values = new float[2,2];
		values[0, 0] = 0f;
		values[1, 0] = 0f;
		values[0, 1] = 0f;
		values[1, 1] = 0f;

		for (int iter = 0; iter < SIZE; iter++)
		{
			min = 1;
			max = 0;
			Debug.Log("Generation of the terrain step "+iter.ToString());
			float[,] nvalues = new float[values.GetLength(0) * 2 - 1, values.GetLength(1) * 2 - 1];
			for (int i = 0; i < values.GetLength(0) - 1; i++)
			{
				for (int j = 0; j < values.GetLength(1) - 1; j++)
				{
					float val1 = values[i, j];
					float val2 = values[i + 1, j + 1];
					float val3 = values[i, j + 1];
					float val4 = values[i + 1, j];
					float rnd = Random.Range(-1f, 1f);
					float rd = rnd * RANDOMI;

					nvalues[2 * i, 2 * j] = val1;
					nvalues[2 * i + 1, 2 * j + 1] = (val1 + val2 + val3 + val4) / 4 + rd;
					min = Math.Min(min, nvalues[2 * i + 1, 2 * j + 1]);
					max = Math.Max(max, nvalues[2 * i + 1, 2 * j + 1]);
					
				}
				nvalues[2 * i, nvalues.GetLength(1) - 1] = values[i, values.GetLength(1) - 1];
			}
			for (int j = 0; j < values.GetLength(1) - 1; j++)
			{
				nvalues[nvalues.GetLength(0) - 1, 2 * j] = values[values.GetLength(0) - 1,j];
			}

				for (int i = 0; i < nvalues.GetLength(0); i++)
			{
				for (int j = 0; j < nvalues.GetLength(1); j++)
				{
					if (mod(i + j, 2) == 1)
					{
						float val1 = nvalues[mod(i - 1, nvalues.GetLength(0)), j];
						float val2 = nvalues[i, mod(j - 1, nvalues.GetLength(1))];
						float val3 = nvalues[mod(i + 1,nvalues.GetLength(0)), j];
						float val4 = nvalues[i, mod(j + 1,nvalues.GetLength(1))];

						//deactivate this part if you just want the terrain to loop on itself. This value should give relatively flat borders without any regular oscillation pattern
						if (i == 0) { val1 = (val2 + val4) / 2; }
						if (j == 0) { val2 = (val1 + val3) / 2; }
						if (i == nvalues.GetLength(0)-2) { val3 = (val2 + val4) / 2; }
						if (j == nvalues.GetLength(1) - 2) { val4 = (val1 + val3) / 2; }

						float rnd = Random.Range(-1f, 1f);
						float rd = rnd * RANDOMI;
						nvalues[i, j] = (val1 + val2 + val3 + val4) / 4 + rd;
						min = Math.Min(min, nvalues[i, j]);
						max = Math.Max(max, nvalues[i, j]);
					}
				}
			}
			RANDOMI = RANDOMI * SMOOTH;
			values = nvalues;
		}
		//normalisation
		for (int i = 0; i < values.GetLength(0); i++)
		{
			for (int j = 0; j < values.GetLength(1); j++)
			{
				values[i, j] = (values[i, j] - min) / (max - min);
			}
		}
		this.heightMap = values;
	}
}
