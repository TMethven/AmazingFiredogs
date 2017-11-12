using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControllerScript : MonoBehaviour
{
    public float Speed = 0.1f;
    public GameObject fireSprite;

    private int[,] FireArray;
    private BuildingOcupancyScript buildingScript;
    private GameObject[,] fireSprites;
    private bool gameActive = true;
    private int fireLevels = 5;

    private bool firstFireSet = false;

    private int GridHeight, GridWidth;

	// Use this for initialization
	void Start ()
    {
        GridHeight = BuildingOcupancyScript.GridHeight;
        GridWidth = BuildingOcupancyScript.GridWidth;

        FireArray = new int[GridHeight, GridWidth];

        buildingScript = this.gameObject.GetComponent<BuildingOcupancyScript>();

        createFireSprites();

        StartCoroutine(checkFireSpread());
	}

    private void createFireSprites()
    {
        fireSprites = new GameObject[GridHeight, GridWidth];

        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                FireArray[y, x] = 0;
                fireSprites[y, x] = Instantiate(fireSprite, this.transform);
                fireSprites[y, x].transform.localPosition = new Vector3(x, y, -1f);
                fireSprites[y, x].SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update ()
    {
		if(!firstFireSet)
        {
            int x = Random.Range(0, GridWidth);
            int y = Random.Range(0, GridHeight);
            
            if(buildingScript.OcupancyGrid[y, x])
            {
                FireArray[y, x] = 1;
                firstFireSet = true;
            }
        }
	}

    private IEnumerator checkFireSpread()
    {
        while(gameActive)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
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

                    if (FireArray[y, x] == 0)
                        fireSprites[y, x].SetActive(false);
                    else
                    {
                        fireSprites[y, x].SetActive(true);
                        fireSprites[y, x].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f * FireArray[y, x]);
                    }
                }
            }
            yield return new WaitForSeconds(Speed);
        }
    }

    private void checkSpreadToNeighbours(int y, int x)
    {
        //Check up!
        if(y > 0 && FireArray[y - 1, x] == 0 && buildingScript.OcupancyGrid[y - 1, x] && Random.Range(0f, 1f) > 0.25f)
        {
            
            FireArray[y - 1, x] = 1;
        }
        //Check down!
        if (y < GridHeight - 1 && FireArray[y + 1, x] == 0 && buildingScript.OcupancyGrid[y + 1, x] && Random.Range(0f, 1f) > 0.25f)
        {
            FireArray[y + 1, x] = 1;
        }

        //Check up!
        if (x > 0 && FireArray[y, x - 1] == 0 && buildingScript.OcupancyGrid[y, x - 1] && Random.Range(0f, 1f) > 0.8f)
        {
            FireArray[y, x - 1] = 1;
        }
        //Check down!
        if (x < GridWidth - 1 && FireArray[y, x + 1] == 0 && buildingScript.OcupancyGrid[y, x + 1] && Random.Range(0f, 1f) > 0.8f)
        {
            FireArray[y, x + 1] = 1;
        }
    }
}
