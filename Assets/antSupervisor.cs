using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antSupervisor : MonoBehaviour
{
	public string filePath;
	public GameObject antPrefab;
	private AntController[] antList;

	public float tickSpeed = 1f;
	private int currentTick;
	private int maxTick;
	private float startingTime;
	// Start is called before the first frame update
	void Start()
    {
		string fileData = System.IO.File.ReadAllText(filePath);
		string[] splitFileData = fileData.Split('\n');
		antList = new AntController[splitFileData.GetLength(0) - 1];
		for (int i = 0;i<splitFileData.GetLength(0)-1;i++)
		{
			string antPath = splitFileData[i];
			GameObject ant = Instantiate(antPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			antList[i] = ant.GetComponent<AntController>();
			ant.GetComponent<AntController>().setPathFromLStr(antPath);
		}
		maxTick = antList[0].getPathLength();
		this.currentTick = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - this.startingTime) >= tickSpeed &&
			currentTick<maxTick-1)
		{
			this.startingTime = Time.time;
			foreach (AntController ant in antList)
			{
				ant.updatePosition(currentTick);
			}
			//Debug.log(currentTick)
			currentTick++;
		}
    }
}
