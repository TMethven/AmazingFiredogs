using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DogSelectController : MonoBehaviour
{

    private GameObject CorgiQuad, HuskyQuad;
    private GameObject P1Quad, P2Quad;
    private TMP_Text P1Text, P2Text, CountdownText;

    private bool CorgiSelected = false, HuskySelected = false;

    private float countdown = -1;
    private float countdownTime = 3;

	string nextScene;

	// Use this for initialization
	void Start ()
    {
		nextScene = GlobalInput.GameMode;

        CorgiQuad = GameObject.Find("CorgiQuad");
        HuskyQuad = GameObject.Find("HuskyQuad");

        CorgiQuad.GetComponent<MeshRenderer>().material.color = Color.black;
        HuskyQuad.GetComponent<MeshRenderer>().material.color = Color.black;

        P1Quad = GameObject.Find("P1Quad");
        P2Quad = GameObject.Find("P2Quad");

        P1Quad.SetActive(false);
        P2Quad.SetActive(false);

        P1Text = GameObject.Find("P1Text").GetComponent<TMP_Text>();
        P2Text = GameObject.Find("P2Text").GetComponent<TMP_Text>();

        CountdownText = GameObject.Find("CountdownText").GetComponent<TMP_Text>();
        CountdownText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < GlobalInput.Fire.Length; i++)
        {
            if(Input.GetButtonDown(GlobalInput.Fire[i]))
            {
                if(GlobalInput.Player1Controller == -1)
                {
                    if (CheckIfPlayerAlreadyAssigned(i))
                        continue;

                    GlobalInput.Player1Controller = i;
                    P1Quad.SetActive(true);
                    P1Text.text = "< Select your dog! >";
                }
                else if(GlobalInput.Player2Controller == -1)
                {
                    if (CheckIfPlayerAlreadyAssigned(i))
                        continue;

                    GlobalInput.Player2Controller = i;
                    P2Quad.SetActive(true);
                    P2Text.text = "< Select your dog! >";
                }
            }
        }

        if (GlobalInput.Player1Controller != -1 && Input.GetButtonDown(GlobalInput.Back[GlobalInput.Player1Controller]))
        {
            GlobalInput.Player1Controller = -1;
            P1Quad.SetActive(false);
            P1Text.text = "Player 1: Press Fire!";

            if (GlobalInput.Player1DogType == GlobalInput.DogType.Corgi)
            {
                CorgiQuad.transform.Find("Corgi").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                CorgiSelected = false;
            }
            else if (GlobalInput.Player1DogType == GlobalInput.DogType.Husky)
            {
                HuskyQuad.transform.Find("Husky").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                HuskySelected = false;
            }

            GlobalInput.Player1DogType = GlobalInput.DogType.None;
        }

        if (GlobalInput.Player2Controller != -1 && Input.GetButtonDown(GlobalInput.Back[GlobalInput.Player2Controller]))
        {
            GlobalInput.Player2Controller = -1;
            P2Quad.SetActive(false);
            P2Text.text = "Player 1: Press Fire!";

            if (GlobalInput.Player2DogType == GlobalInput.DogType.Corgi)
            {
                CorgiQuad.transform.Find("Corgi").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                CorgiSelected = false;
            }
            else if (GlobalInput.Player2DogType == GlobalInput.DogType.Husky)
            {
                HuskyQuad.transform.Find("Husky").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                HuskySelected = false;
            }

            GlobalInput.Player2DogType = GlobalInput.DogType.None;
        }

        if (GlobalInput.Player1Controller != -1)
        {
            if (Input.GetAxis(GlobalInput.Horizontal[GlobalInput.Player1Controller]) > 0)
            {
                P1Quad.transform.position = new Vector3(7f, -0.5f, -1f);

                if (!HuskySelected && Input.GetButtonDown(GlobalInput.Fire[GlobalInput.Player1Controller]))
                {
                    HuskyQuad.transform.Find("Husky").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                    GlobalInput.Player1DogType = GlobalInput.DogType.Husky;
                    HuskySelected = true;
                    P1Text.text = "Husky Selected!";
                    P1Quad.SetActive(false);
                }
            }
            else if (Input.GetAxis(GlobalInput.Horizontal[GlobalInput.Player1Controller]) < 0)
            {
                P1Quad.transform.position = new Vector3(-7f, -0.5f, -1f);

                if (!CorgiSelected && Input.GetButtonDown(GlobalInput.Fire[GlobalInput.Player1Controller]))
                {
                    CorgiQuad.transform.Find("Corgi").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                    GlobalInput.Player1DogType = GlobalInput.DogType.Corgi;
                    CorgiSelected = true;
                    P1Text.text = "Corgi Selected!";
                    P1Quad.SetActive(false);
                }
            }
            else
            {
                P1Quad.transform.position = new Vector3(0, -0.5f, -1f);
            }
        }

        if (GlobalInput.Player2Controller != -1)
        {
            if (Input.GetAxis(GlobalInput.Horizontal[GlobalInput.Player2Controller]) > 0)
            {
                P2Quad.transform.position = new Vector3(7f, -3.5f, -1f);

                if(!HuskySelected && Input.GetButtonDown(GlobalInput.Fire[GlobalInput.Player2Controller]))
                {
                    HuskyQuad.transform.Find("Husky").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                    GlobalInput.Player2DogType = GlobalInput.DogType.Husky;
                    HuskySelected = true;
                    P2Text.text = "Husky Selected!";
                    P2Quad.SetActive(false);
                }
            }
            else if (Input.GetAxis(GlobalInput.Horizontal[GlobalInput.Player2Controller]) < 0)
            {
                P2Quad.transform.position = new Vector3(-7f, -3.5f, -1f);

                if (!CorgiSelected && Input.GetButtonDown(GlobalInput.Fire[GlobalInput.Player2Controller]))
                {
                    CorgiQuad.transform.Find("Corgi").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
                    GlobalInput.Player2DogType = GlobalInput.DogType.Corgi;
                    CorgiSelected = true;
                    P2Text.text = "Corgi Selected!";
                    P2Quad.SetActive(false);
                }
            }
            else
            {
                P2Quad.transform.position = new Vector3(0, -3.5f, -1f);
            }
        }

        if (CorgiSelected && HuskySelected && countdown == -1)
        {
            countdown = countdownTime;
            CountdownText.gameObject.SetActive(true);
        }
        else if(CountdownText.IsActive() && !(CorgiSelected && HuskySelected))
        {
            countdown = -1;
            CountdownText.gameObject.SetActive(false);
        }


        if(CountdownText.IsActive())
        {
            countdown -= Time.deltaTime;

            CountdownText.text = "Starting in:\n" + Mathf.RoundToInt(countdown);

            if(countdown <= 0)
            {
                countdown = 0;
				SceneManager.LoadScene(nextScene);
            }
        }
    }

    private bool CheckIfPlayerAlreadyAssigned(int player)
    {
        if (GlobalInput.Player1Controller == player)
            return true;

        if (GlobalInput.Player2Controller == player)
            return true;

        return false;
    }
}
