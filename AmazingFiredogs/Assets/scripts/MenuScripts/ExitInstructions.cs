using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ExitInstructions : MonoBehaviour {
	public Button Exit; 
	// Use this for initialization
	void Start () 
	{
		Button btn_exit = Exit.GetComponent<Button> ();
		btn_exit.onClick.AddListener (ExitFromHere);
	}

    void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            EventSystem.current.SetSelectedGameObject(Exit.gameObject);
    }

	void ExitFromHere()
	{
		SceneManager.LoadScene ("Menu");
	}

}
