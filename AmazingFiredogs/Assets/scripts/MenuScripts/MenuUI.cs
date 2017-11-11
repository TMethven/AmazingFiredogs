using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {

	public Button start_btn;
	public Button exit_btn;
	public Button instr_btn; 

	void Start () 
	{
		Button btnStart = start_btn.GetComponent<Button> ();
		Button btnExit = exit_btn.GetComponent<Button> ();
		Button btnInstr = instr_btn.GetComponent<Button> ();

		btnStart.onClick.AddListener (StartTaskOnClick);
		btnExit.onClick.AddListener (ExitTaskOnClick);
		btnInstr.onClick.AddListener (InstrTaskOnClick);
	}





	void StartTaskOnClick()
	{
		SceneManager.LoadScene ("Introduction");
	}
	void ExitTaskOnClick()
	{
		Application.Quit ();
	}
	void InstrTaskOnClick()
	{
		SceneManager.LoadScene ("Instructions");
	}
}
