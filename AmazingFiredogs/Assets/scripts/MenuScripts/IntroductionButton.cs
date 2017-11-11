using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class IntroductionButton : MonoBehaviour {

	public Button Next; 
	void Start () 
	{
		Button btn_next = Next.GetComponent<Button> ();

		btn_next.onClick.AddListener (StartGame);
	}
	
	void StartGame()
	{
		SceneManager.LoadScene ("DogSelect");
	}
}
