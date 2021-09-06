using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PheromoneGrid : MonoBehaviour
{
	static internal float LOSS = 0.001f;
	static internal bool initPheromones = true;
	// Start is called before the first frame update
	internal int size;
	private float[,] N;
	private float[,] NE;
	private float[,] E;
	private float[,] SE;
	private float[,] S;
	private float[,] SW;
	private float[,] W;
	private float[,] NW;

	public GameObject context;
	private Vector2 foodPos;

	private Dictionary<Vector2Int, float[,]> grids;
	

	internal void addPheromone(Vector2Int start, Vector2Int end, float value)
	{
		if (start.x < 0 || start.x > this.size ||
			start.y < 0 || start.y > this.size ||
			end.x < 0 || end.x > this.size ||
			end.y < 0 || end.y > this.size ||
			Math.Abs(start[0] - end[0]) > 1 || Math.Abs(start[1] - end[1]) > 1)
		{
			Debug.Log("The values given are not correct.");
		}

		else
		{
			Vector2Int delta = new Vector2Int(end[0] - start[0], end[1] - start[1]);
			this.grids[delta][start[0], start[1]] += value;
		}

	}
	internal void addPheromone(Vector3 start, Vector3 end, float value)
	{
		this.addPheromone(
			new Vector2Int((int)start.x, (int)start.y),
			new Vector2Int((int)end.x, (int)end.y),
			value);
	}
		void Start()
    {
		this.foodPos = new Vector2(0, 0);
		try { this.foodPos = this.context.GetComponent<placeContext>().foodPos; }
		catch { this.foodPos = this.context.GetComponent<dynamicPlaceContext>().foodPos; }

		int gridSize = (int)(GetComponent<Terrain>().terrainData.size.y);
		this.size = gridSize;
		this.N = new float[gridSize, gridSize];
		this.NE= new float[gridSize, gridSize];

		this.E= new float[gridSize, gridSize];

		this.SE= new float[gridSize, gridSize];

		this.S= new float[gridSize, gridSize];

		this.SW= new float[gridSize, gridSize];

		this.W= new float[gridSize, gridSize];

		this.NW= new float[gridSize, gridSize];


		this.grids = new Dictionary<Vector2Int, float[,]>();

		this.grids.Add(new Vector2Int(1, 0), this.N);
		this.grids.Add(new Vector2Int(1, 1), this.NE);
		this.grids.Add(new Vector2Int(1, -1), this.NW);
		this.grids.Add(new Vector2Int(-1, 0), this.S);
		this.grids.Add(new Vector2Int(-1, 1), this.SE);
		this.grids.Add(new Vector2Int(-1, -1), this.SW);
		this.grids.Add(new Vector2Int(0, 1), this.E);
		this.grids.Add(new Vector2Int(0, -1), this.W);
		
		if (initPheromones)
		{
			for (int y = 0; y < gridSize; y++)
			{
				for (int x = (int)foodPos.x + 1; x < gridSize; x++) { this.addPheromone(new Vector2Int(x, y), new Vector2Int(x - 1, y), 2 / (Math.Abs(x - foodPos.x) + Math.Abs(y - foodPos.y))); }
				for (int x = 0; x < (int)foodPos.x; x++) { this.addPheromone(new Vector2Int(x, y), new Vector2Int(x + 1, y), 20 / (Math.Abs(x - foodPos.x) + Math.Abs(y - foodPos.y))); }

			}
			for (int x = 0; x < gridSize; x++)
			{
				for (int y = (int)foodPos.y + 1; y < gridSize; y++) { this.addPheromone(new Vector2Int(x, y), new Vector2Int(x, y - 1), 2 / (Math.Abs(x - foodPos.x) + Math.Abs(y - foodPos.y))); }
				for (int y = 0; y < (int)foodPos.y; y++) { this.addPheromone(new Vector2Int(x, y), new Vector2Int(x, y + 1), 20 / (Math.Abs(x - foodPos.x) + Math.Abs(y - foodPos.y))); }

			}
		}
        
}


	// Update is called once per frame
	void Update()
    {
        
    }
	
        
	internal void dissipatePheromones(){
		foreach (float[,] grid in this.grids.Values)
		{
			for (int x = 0; x < this.size; x++)
			{
				for (int y = 0; y < this.size; y++)
				{
					grid[x, y] = grid[x, y] * (1 - LOSS);
				}
			}
		}
	
	}
        
	internal List<Vector3> neighborList(Vector2Int start,Boolean printResult=true) { 
		List<Vector3> r = new List<Vector3>();
        foreach ( Vector2Int delta in this.grids.Keys){
			int x = start.x + delta.x;
			int y = start.y + delta.y;
			if (-1 < x && -1 < y && this.size > x && this.size > y) {
				float value = this.grids[delta][start.x, start.y];
				r.Add(new Vector3((float)x, (float)y, value));
			}
		}
		if (printResult)
		{
			string result = "Neighbors pheromones: ";
			foreach (var item in r)
			{
				result += item.z.ToString() + ", ";
			}
			Debug.Log(result);
		}
	return r;
	}
	internal List<Vector3> neighborList(Vector3 start)
	{
		return this.neighborList(new Vector2Int((int)start.x, (int)start.y));
	}


	}
