using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour {

	public Button start_btn;
	public Button exit_btn;
	public Button instr_btn; 

	void Start () 
	{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		Button btnStart = start_btn.GetComponent<Button> ();
		Button btnExit = exit_btn.GetComponent<Button> ();
		Button btnInstr = instr_btn.GetComponent<Button> ();

		btnStart.onClick.AddListener (StartTaskOnClick);
		btnExit.onClick.AddListener (ExitTaskOnClick);
		btnInstr.onClick.AddListener (InstrTaskOnClick);
	}

    void Update()
    {
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            EventSystem.current.SetSelectedGameObject(start_btn.gameObject);
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
