using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSelectController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
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
                    GlobalInput.Player1Controller = i;
                }
                else if(GlobalInput.Player2Controller == -1)
                {
                    GlobalInput.Player2Controller = i;
                }
            }
        }
	}
}
