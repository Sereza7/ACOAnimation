using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSupervisorDynamic : MonoBehaviour
{
	public GameObject antPrefab;
	private AntControllerDynamic[] antList;

	//Parameters
	static internal float tickSpeed = 0.1f;
	static internal int antAmount = 50;

	private int currentTick;
	private float startingTime;

	void Start()
	{
		antList = new AntControllerDynamic[antAmount];
		antList[0] = antPrefab.GetComponent<AntControllerDynamic>();
		antList[0].tickSpeed = tickSpeed;
		for (int i = 1; i < antList.GetLength(0); i++)
		{
			GameObject ant = Instantiate(antPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			antList[i] = ant.GetComponent<AntControllerDynamic>();
			antList[i].tickSpeed= tickSpeed;
		}
		this.currentTick = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if ((Time.time - this.startingTime) >= tickSpeed)
		{
			this.startingTime = Time.time;
			foreach (AntControllerDynamic ant in antList)
			{
				ant.updatePosition(currentTick);
			}
			//Debug.Log(currentTick);
			currentTick++;
		}
	}
}
