using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{
	
	private List<Vector2> path;
	public GameObject terrain;
	private float[,] heightMap;

	private Vector3 oldPosition;
	private Vector3 newPosition;
	private Vector3 oldForward;
	private Vector3 newForward;
	private float lastTickTime;

	// Start is called before the first frame update
	void Awake()
    {
		this.path = new List<Vector2>();
		this.heightMap = terrain.GetComponent<showHeightMap>().heightMap;
    }

    // Update is called once per frame
    void Update()
    {
		this.transform.position = Vector3.Lerp(this.oldPosition, this.newPosition, Math.Min(Time.time-this.lastTickTime,1f));
		if(this.newForward == Vector3.zero && Time.time>5f)
		{
			this.gameObject.SetActive(false);
		}
		else if (this.transform.forward != this.newForward)
		{
			this.transform.forward = Vector3.Lerp(this.oldForward, this.newForward, Math.Min((Time.time - this.lastTickTime) * 5+0.01f, 1f));
		}
	}
	internal void setPathFromLStr(string antPath)
	{
		string[] antPathL = antPath.Split(',');
		foreach (string element in antPathL)
		{
			string[] splitEl = element.Split('#');
			this.path.Add(new Vector2(int.Parse(splitEl[0]), int.Parse(splitEl[1])));
		}
	}
	internal void updatePosition(int simulationTick)
	{
		this.lastTickTime = Time.time;
		Vector2 newCoords = this.path[simulationTick];
		this.oldPosition = this.transform.position;
		this.newPosition = new Vector3(newCoords.y, 0.015f + 33 * this.heightMap[(int)(newCoords.x), (int)(newCoords.y)], newCoords.x);
		this.oldForward = this.transform.forward;
		this.newForward = this.newPosition - this.oldPosition;
		if (this.newForward == Vector3.zero)
		{
			this.newForward = Vector3.right;
		}
	}
	public int getPathLength()
	{
		return this.path.Count;
	}
}
