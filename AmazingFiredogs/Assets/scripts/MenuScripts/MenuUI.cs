using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour {

	public Button StartSingle;
	public Button StartDouble;
	public Button Exit;
	public Button Instructions; 

	void Start () 
	{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

		StartSingle.onClick.AddListener(StartSingleOnClick);
		StartDouble.onClick.AddListener(StartDoubleOnClick);
		Exit.onClick.AddListener(ExitTaskOnClick);
		Instructions.onClick.AddListener(InstrTaskOnClick);

		EventSystem.current.SetSelectedGameObject(StartSingle.gameObject);
	}

    void Update()
    {
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			EventSystem.current.SetSelectedGameObject(StartSingle.gameObject);

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
    }

	void StartSingleOnClick()
	{
		GlobalInput.GameMode = "OneBuilding";
		SceneManager.LoadScene("Introduction");
	}

	void StartDoubleOnClick()
	{
		GlobalInput.GameMode = "TwoBuildings";
		SceneManager.LoadScene("Introduction");
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
