using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitInstructions : MonoBehaviour {
	public Button Exit; 
	// Use this for initialization
	void Start () 
	{
		Button btn_exit = Exit.GetComponent<Button> ();
		btn_exit.onClick.AddListener (ExitFromHere);
	}

	void ExitFromHere()
	{
		SceneManager.LoadScene ("Menu");
	}

}
