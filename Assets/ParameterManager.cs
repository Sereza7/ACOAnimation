using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParameterManager : MonoBehaviour
{


	//Ant decisions
	public GameObject gammaSlider;
	private UnityEngine.UI.Slider gammaSliderComp;
	public GameObject QSlider;
	private UnityEngine.UI.Slider QSliderComp;

	//Ant gestion parameters
	public GameObject tickSpeedSlider;
	private UnityEngine.UI.Slider tickSpeedSliderComp;
	public GameObject antAmountSlider;
	private UnityEngine.UI.Slider antAmountSliderComp;


	//Ant display parameters
	public GameObject antColorPicker;
	private FlexibleColorPicker antColorPickerComp;
	public GameObject antColorVariationSwitch;
	private UnityEngine.UI.Toggle antColorVariationSwitchComp;
	public GameObject antSizeSlider;
	private UnityEngine.UI.Slider antSizeSliderComp;

	//Terrain generation
	public GameObject terrainSizeSlider;
	private UnityEngine.UI.Slider terrainSizeSliderComp;
	public GameObject randomIntensitySlider;
	private UnityEngine.UI.Slider randomIntensitySliderComp;
	public GameObject terrainSmoothingSlider;
	private UnityEngine.UI.Slider terrainSmoothingSliderComp;

	//Pheromones
	public GameObject pheroLossSlider;
	private UnityEngine.UI.Slider pheroLossSliderComp;
	public GameObject pheroInitSwitch;
	private UnityEngine.UI.Toggle pheroInitSwitchComp;

	// Start is called before the first frame update
	void Awake()
    {
		Debug.Log("Parameter initialisation starting.");
		//Ant decisions
		gammaSliderComp = gammaSlider.GetComponent<UnityEngine.UI.Slider>();
		QSliderComp = QSlider.GetComponent<UnityEngine.UI.Slider>();

		//Ant gestion parameters
		tickSpeedSliderComp = tickSpeedSlider.GetComponent<UnityEngine.UI.Slider>();
		antAmountSliderComp = antAmountSlider.GetComponent<UnityEngine.UI.Slider>();

		//Ant display parameters
		antColorPickerComp = antColorPicker.GetComponent<FlexibleColorPicker>();
		antColorVariationSwitchComp = antColorVariationSwitch.GetComponent<UnityEngine.UI.Toggle>();
		antSizeSliderComp = antSizeSlider.GetComponent<UnityEngine.UI.Slider>();

		//Terrain generation
		terrainSizeSliderComp = terrainSizeSlider.GetComponent<UnityEngine.UI.Slider>();
		randomIntensitySliderComp = randomIntensitySlider.GetComponent<UnityEngine.UI.Slider>();
		terrainSmoothingSliderComp = terrainSmoothingSlider.GetComponent<UnityEngine.UI.Slider>();

		//Pheromones
		pheroLossSliderComp = pheroLossSlider.GetComponent<UnityEngine.UI.Slider>();
		pheroInitSwitchComp = pheroInitSwitch.GetComponent<UnityEngine.UI.Toggle>();
	
		
		/*
		//Ant decisions
		this.gamma = new Parameter<float>();
		this.gamma.SetDescription("Minimum value for the chance to move to another tile. " +
			"Compare this value to pheromone/distance to get it's relevance. " +
			"Higher is more chaotic. 0 means that the ants won't explore anything new after a few trips. Recommended range: 0f-1000f");
		this.gamma.SetValue(gammav);
		this.gamma.Subscribe(AntControllerDynamic.GAMMA);
		
		this.Q = new Parameter<float>();
		this.Q.SetDescription("Factor to the amount of pheromones put down by ants. " +
			"Higher means the pheromone quantities will rise faster, " +
			"making the GAMMA part of the direction choice less useful. The relation isn't easy to see. Recommended range: 0f-100f");
		this.Q.SetValue(Qv);
		this.Q.Subscribe(AntControllerDynamic.Q);

		//Ant gestion parameters
		this.tickSpeed = new Parameter<float>();
		this.tickSpeed.SetDescription("Minimum amount of time between two ant movement. " +
			"Capped by unity update rate (should be at 50/60 FPS)." +
			"1 second is quite slow and allows to follow one specific individual. " +
				"0.1 second is good for trend observation. Recommended range: 0.05f-5f");
		this.tickSpeed.SetValue(tickSpeedv);
		this.tickSpeed.Subscribe(AntSupervisorDynamic.tickSpeed);
		
		this.antAmount = new Parameter<int>();
		this.antAmount.SetDescription("Quantity of ants to render and display. Recommended range: 1-200");
		this.antAmount.SetValue(antAmountv);
		this.antAmount.Subscribe(AntSupervisorDynamic.antAmount);

		//Ant display parameters
		this.antColor = new Parameter<Color>();
		this.antColor.SetDescription("The color to give to the ants.");
		this.antColor.SetValue(this.antColorv);
		this.antColor.Subscribe(AntControllerDynamic.antColor);
		this.antSize = new Parameter<float>();
		this.antSize.SetDescription("The size of the ants. Recommended range:0.05f-5");
		this.antSize.SetValue(this.antSizev);
		this.antSize.Subscribe(AntControllerDynamic.antSize);

		//Terrain generation
		this.terrainSize = new Parameter<int>();
		this.terrainSize.SetDescription("The actual size of the terrain in units will be egal to " +
			"2 to the power of <value> plus one. Recommended range: 5-7 (33-129)");
		this.terrainSize.SetValue(terrainSizev);
		this.terrainSize.Subscribe(dynamicShowHeightMap.SIZE);
		this.randomIntensity = new Parameter<float>();
		this.randomIntensity.SetDescription("The higher this value, and the more the map will generate towards positive values. " +
			"Pretty much useless, just perform a translation if needed. Recommended range: 0f-1f");
		this.randomIntensity.SetValue(randomIntensityv);
		this.randomIntensity.Subscribe(dynamicShowHeightMap.RANDOMI);
		this.terrainSmoothing = new Parameter<float>();
		this.terrainSmoothing.SetDescription("The lower the value, the lower the spikes on the terrain. " +
			"0.8 is terribly shacky, 0.1 is almost flat. Recommended range: 0f<x<1f");
		this.terrainSmoothing.SetValue(terrainSmoothingv);
		this.terrainSmoothing.Subscribe(dynamicShowHeightMap.SMOOTH);


		//Pheromones
		this.pheroLoss = new Parameter<float>();
		this.pheroLoss.SetDescription("This number multiplies the values on the pheromone grid at each ant's " +
			"finished path. Recommended value: 0.001f (0<x<=1)");
		this.pheroLoss.SetValue(pheroLossv);
		this.pheroLoss.Subscribe(PheromoneGrid.LOSS);
		this.pheroInit = new Parameter<bool>();
		this.pheroInit.SetDescription("Wether or not initialize the grid with a direction towards the food.");
		this.pheroInit.SetValue(pheroInitv);
		this.pheroInit.Subscribe(PheromoneGrid.initPheromones);
		*/
	}

	// Update is called once per frame
	void Update()
    {
		//Ant decisions
		AntControllerDynamic.GAMMA = gammaSliderComp.value;
		AntControllerDynamic.Q = QSliderComp.value;

		//Ant gestion parameters
		AntSupervisorDynamic.tickSpeed = tickSpeedSliderComp.value;
		AntSupervisorDynamic.antAmount = (int)antAmountSliderComp.value;

		//Ant display parameters
		AntControllerDynamic.antColor = antColorPickerComp.color;
		AntControllerDynamic.antColorVariation = antColorVariationSwitchComp.isOn;
		AntControllerDynamic.antSize = antSizeSliderComp.value;

		//Terrain generation
		dynamicShowHeightMap.SIZE = (int)terrainSizeSliderComp.value;
		dynamicShowHeightMap.RANDOMI = randomIntensitySliderComp.value;
		dynamicShowHeightMap.SMOOTH = terrainSmoothingSliderComp.value;

		//Pheromones
		PheromoneGrid.LOSS = pheroLossSliderComp.value;
		PheromoneGrid.initPheromones = pheroInitSwitchComp.isOn;

	}

	public void StartSimu()
	{
		SceneManager.LoadScene("GenerateScene");
	}
	public void Reset()
	{
		SceneManager.LoadScene("ParameterSelect");
	}
	public void Exit()
	{
		SceneManager.LoadScene("Starting");
	}
}
