using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AntControllerDynamic : MonoBehaviour
{
	//Parameters (see the ParameterManager  for more info and further ideas)
	static internal float GAMMA = 0.01f;
	static internal float Q = 1;

	static internal Color antColor;
	static internal bool antColorVariation = true;
	static internal float antSize = 1.5f;

	private List<Vector2> path;
	public GameObject context;
	public GameObject textUI;
	private Vector2Int target;
	private Vector2Int source;
	public GameObject terrain;
	private float[,] heightMap;
	private PheromoneGrid pheromones;

	private Vector3 oldPosition;
	private Vector3 newPosition;
	private Vector3 oldForward;
	private Vector3 newForward;
	private float lastTickTime;

	private Boolean isSearching;



	private Vector3 coords;
	private float costPath;
	internal float tickSpeed;

	// Start is called before the first frame update
	void Start()
	{
		this.path = new List<Vector2>();
		this.isSearching = true;
		this.target = new Vector2Int(0, 0);
		try { this.target = context.GetComponent<placeContext>().foodPos; }
		catch { this.target = context.GetComponent<dynamicPlaceContext>().foodPos; }
		this.source = new Vector2Int(0, 0);
		try { this.source = context.GetComponent<placeContext>().spawnPos; }
		catch { this.source = context.GetComponent<dynamicPlaceContext>().spawnPos; }

		this.path.Add(source);
		this.heightMap = new float[1, 1];
		try { this.heightMap = terrain.GetComponent<showHeightMap>().heightMap; }
		catch { this.heightMap = terrain.GetComponent<dynamicShowHeightMap>().heightMap; }
		this.newPosition = Vector3.zero;
		this.oldPosition = Vector3.one;
		this.coords = new Vector3(this.source.x, this.source.y, this.heightMap[this.source.x, this.source.y]);
		this.pheromones = terrain.GetComponent<PheromoneGrid>();

		
		if (antColorVariation)
		{
			float colorDispersion = 0.5f;
			Color.RGBToHSV(antColor, out float H, out float S, out float V);
			Color customColor = Color.HSVToRGB(H, 
				Math.Min(Math.Abs(S + Random.Range(-colorDispersion, colorDispersion)), 1),
				Math.Min(Math.Abs(V + Random.Range(-colorDispersion, colorDispersion)), 1));
			this.transform.Find("Model").GetComponent<MeshRenderer>().material.color = customColor;
		}
		else { this.transform.Find("Model").GetComponent<MeshRenderer>().material.color = antColor; }

		this.transform.localScale *= antSize;

	}

	// Update is called once per frame
	void Update()
	{
		this.transform.position = Vector3.Lerp(this.oldPosition, this.newPosition, Math.Min((Time.time - this.lastTickTime)/this.tickSpeed, 1f));
		
		if (this.transform.forward != this.newForward)
		{
			this.transform.forward = Vector3.Lerp(this.oldForward, this.newForward, Math.Min(5*(Time.time - this.lastTickTime) / this.tickSpeed, 1f));
		}
	}
	internal void updatePosition(int simulationTick)
	{
		//Debug.Log(this.path.Count);
		Vector3 newCoords = this.coords;
		if (this.isSearching)
		{
			newCoords = this.chooseNextStep();
		}
		else
		{
			newCoords = this.backTrackOneStep();

		}
		if (newCoords.x == this.target.x && newCoords.y == this.target.y)
		{
			this.isSearching = false;
			this.transform.Find("apple").gameObject.SetActive(!isSearching);
			this.costPath = this.CostPath();
			this.path.RemoveAt(this.path.Count-1);
		}
		if (newCoords.x == this.source.x && newCoords.y == this.source.y && path.Count==0)
		{
			this.isSearching = true;
			this.transform.Find("apple").gameObject.SetActive(!isSearching);
			this.pheromones.dissipatePheromones();
			this.path.Add(newCoords);
			this.textUI.GetComponent<TextMeshProUGUI>().text = "Last cost: " + (((int)(this.costPath * 100)) / 100f).ToString();
		}
		this.coords = newCoords;
		this.lastTickTime = Time.time;
		this.oldPosition = this.transform.position;
		this.newPosition = new Vector3(newCoords.y, 0.015f + (int)terrain.GetComponent<Terrain>().terrainData.size.y * this.heightMap[(int)(newCoords.x), (int)(newCoords.y)], newCoords.x);
		this.oldForward = this.transform.forward;
		this.newForward = this.newPosition - this.oldPosition;
		if (this.newForward == Vector3.zero)
		{
			this.newForward = Vector3.right;
		}
	}

	private float CostPath()
	{
		float totalCost = 0f;

		for (int stepI = 0; stepI < this.path.Count - 1; stepI++) {
			Vector3 stepA = path[stepI];
			Vector3 stepB = path[stepI + 1];
			float stepCost = distance(stepA, stepB);
			totalCost += stepCost;
		}
		return totalCost;
	}

	private Vector3 backTrackOneStep()
	{
		Vector3 r = this.path[this.path.Count-1];
		if (this.path.Count >= 2) {
			float pheromoneQtt = AntControllerDynamic.Q / this.costPath;
			this.pheromones.addPheromone(this.path[this.path.Count - 2], r, pheromoneQtt);
		}
		this.path.RemoveAt(this.path.Count - 1);
		return r;
	}

	public int getPathLength()
	{
		return this.path.Count;
	}

	internal float distance(Vector3 spotA, Vector3 spotB)
	{
		if (spotA.z == spotB.z) {
			//print("samePoint");
			return 0.1f;
		} else if (Math.Abs(spotA.x - spotB.x) <= 1 && Math.Abs(spotA.y - spotB.y) <= 1) {
			return (float)Math.Pow(spotA[2] * 100 - spotB[2] * 100,2) + 0.1f;
		} else {
			return 9999999999;
		}
			
 	}
    

	internal Vector3 chooseNextStep()
	{
		
		List<Vector3> nextSteps = this.pheromones.neighborList(this.coords);

		float decision = Random.Range(0f, 1f);

		List<float> PCCpasdiv = new List<float>();
		PCCpasdiv.Add(0f);
		float currentPCC = 0;

		for (int i = 0; i < nextSteps.Count; i++)
		{
			Vector3 v = nextSteps[i];
			float pheromoneValue = v.z;
			Vector3 terrainStep = new Vector3(v.x, v.y, this.heightMap[(int)v.x, (int)v.y]);
			currentPCC += AntControllerDynamic.GAMMA + pheromoneValue / distance(this.coords, terrainStep);

			PCCpasdiv.Add(currentPCC);
		}
		/*string result = "PCCpasDiv: ";
		foreach (var item in PCCpasdiv)
		{
			result += item.ToString() + ", ";
		}
		Debug.Log(result);
		*/

		float sum = PCCpasdiv[PCCpasdiv.Count-1];
		Vector3 nextStep = this.coords;
		for (int pccI = 0; pccI < PCCpasdiv.Count; pccI++) {
			float pcc = PCCpasdiv[pccI] / sum;
			if (pcc < decision)
			{
				nextStep = nextSteps[pccI];
			}
                
		}
		Vector3 terrainNextStep = new Vector3(nextStep.x, nextStep.y, this.heightMap[(int)nextStep.x, (int)nextStep.y]);
		if (this.path.Contains(terrainNextStep)){
			int i = path.IndexOf(terrainNextStep);
			this.path.RemoveRange(i + 1, this.path.Count - i - 1);
		}
		else
		{

			this.path.Add(terrainNextStep);
		}
		//Debug.Log("Chose next step."+nextStep.x.ToString()+"#"+nextStep.y.ToString());
		return terrainNextStep;
	}
}
