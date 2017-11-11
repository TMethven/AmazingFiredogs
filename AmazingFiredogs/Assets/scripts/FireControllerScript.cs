using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControllerScript : MonoBehaviour
{
    public GameObject fireQuad;
    public float Speed = 0.1f;


    private int arrayWidth = 4, arrayHeight = 100;
    private int[,] FireArray;
    private Material[,] GOArray;
    private bool gameActive = true;
    private int fireLevels = 5;

	// Use this for initialization
	void Start ()
    {
        FireArray = new int[arrayHeight, arrayWidth];
        GOArray = new Material[arrayHeight, arrayWidth];

        for (int y = 0; y < arrayHeight; y++)
        {
            for (int x = 0; x < arrayWidth; x++)
            {
                FireArray[y, x] = 0;
                GameObject tempObj = Instantiate(fireQuad, new Vector3(y * 1 - arrayHeight / 2, x * 1 - arrayWidth / 2, 0f), Quaternion.identity);
                GOArray[y, x] = tempObj.GetComponent<MeshRenderer>().material;
            }
        }

        FireArray[Random.Range(0, arrayHeight), Random.Range(0, arrayWidth)] = 1;

        StartCoroutine(checkFireSpread());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private IEnumerator checkFireSpread()
    {
        while(gameActive)
        {
            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    if (FireArray[y, x] > 0 && FireArray[y, x] < fireLevels)
                    {
                        Debug.Log("y x FireValue:" + y + " " + x + " " + FireArray[y, x]);
                        if (Random.Range(0f, 1f) > 0.2f)
                            FireArray[y, x]++;
                    }
                    else if(FireArray[y, x] == fireLevels)
                    {
                        checkSpreadToNeighbours(y, x);
                    }

                    GOArray[y, x].color = new Color(0.2f * FireArray[y, x], 0, 0);
                }
            }
            yield return new WaitForSeconds(Speed);
        }
    }

    private void checkSpreadToNeighbours(int y, int x)
    {
        //Check up!
        if(y > 0 && FireArray[y - 1, x] == 0 && Random.Range(0f, 1f) > 0.25f)
        {
            FireArray[y - 1, x] = 1;
        }
        //Check down!
        if (y < arrayHeight - 1 && FireArray[y + 1, x] == 0 && Random.Range(0f, 1f) > 0.25f)
        {
            FireArray[y + 1, x] = 1;
        }

        //Check up!
        if (x > 0 && FireArray[y, x - 1] == 0 && Random.Range(0f, 1f) > 0.8f)
        {
            FireArray[y, x - 1] = 1;
        }
        //Check down!
        if (x < arrayWidth - 1 && FireArray[y, x + 1] == 0 && Random.Range(0f, 1f) > 0.8f)
        {
            FireArray[y, x + 1] = 1;
        }
    }
}
