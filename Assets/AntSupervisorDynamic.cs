using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSupervisorDynamic : MonoBehaviour
{
	public GameObject antPrefab;
	private AntControllerDynamic[] antList;

	public float tickSpeed = 1f;
	private int currentTick;
	private float startingTime;
	// Start is called before the first frame update
	void Start()
	{
		antList = new AntControllerDynamic[200];
		for (int i = 0; i < antList.GetLength(0); i++)
		{
			GameObject ant = Instantiate(antPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			antList[i] = ant.GetComponent<AntControllerDynamic>();
			antList[i].SetTickSpeed(this.tickSpeed);
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
			Debug.Log(currentTick);
			currentTick++;
		}
	}
}
