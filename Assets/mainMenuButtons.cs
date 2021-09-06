using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtons : MonoBehaviour
{
	public void Quit()
	{
		Application.Quit();
	}
	public void lessGo()
	{
		SceneManager.LoadScene("ParameterSelect");
	}
	public void openInfo()
	{
		Application.OpenURL("https://github.com/Sereza7/ACOAnimation");
	}
}
