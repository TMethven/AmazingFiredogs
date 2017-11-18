using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroductionButton : MonoBehaviour {
	public Button Next; 
	void Start () 
	{
		Button btn_next = Next.GetComponent<Button> ();

		btn_next.onClick.AddListener (StartGame);
	}
	
    void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            EventSystem.current.SetSelectedGameObject(Next.gameObject);
    }

	void StartGame()
	{
		SceneManager.LoadScene ("DogSelect");
	}
}
