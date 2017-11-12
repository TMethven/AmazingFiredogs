using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControllerScript : MonoBehaviour
{
    public float Speed = 0.1f, fireDelay = 10f;
    public GameObject fireSprite;

    private int[,] FireArray;
    private BuildingOcupancyScript buildingScript;
    private GameObject[,] fireSprites;
    private bool gameActive = true;
    private int fireLevels = 5;

    private float timeSinceLastFire = 0;

    private int GridHeight, GridWidth;

	AudioSource fireSound;

	// Use this for initialization
	void Start ()
    {
        GridHeight = BuildingOcupancyScript.GridHeight;
        GridWidth = BuildingOcupancyScript.GridWidth;

        FireArray = new int[GridHeight, GridWidth];

        buildingScript = this.gameObject.GetComponent<BuildingOcupancyScript>();

        createFireSprites();

		fireSound = GetComponent<AudioSource>();

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
                fireSprites[y, x].transform.localPosition = new Vector3(x * PieceControllerScript.unitSize, y * PieceControllerScript.unitSize, -1f);
                fireSprites[y, x].SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update ()
    {
        timeSinceLastFire += Time.deltaTime;
		if(timeSinceLastFire > fireDelay)
        {
            int x = Random.Range(0, GridWidth);
            int y = Random.Range(0, GridHeight);
            
            if(buildingScript.OcupancyGrid[y, x])
            {
                FireArray[y, x] = 1;
                timeSinceLastFire = 0;
            }
        }
	}

    private IEnumerator checkFireSpread()
    {
        while(gameActive)
        {
			int totalFire = 0;
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
                        fireSprites[y, x].GetComponent<FireSpriteController>().setTransparency(0.2f * FireArray[y, x]);
                        //fireSprites[y, x].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f * FireArray[y, x]);
                    }
					totalFire += FireArray[y, x];
                }
            }

			// Set the fire sound volume to get louder as the fire spreads.
			fireSound.volume = Mathf.Min(totalFire / 50.0f, 1.0f);

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

    public void reduceFire(Vector2Int? buildingPosition)
    {
        if (buildingPosition == null)
        {
            return;
        }

        if (FireArray[buildingPosition.Value.y, buildingPosition.Value.x] > 0)
        {
            --FireArray[buildingPosition.Value.y, buildingPosition.Value.x];
        }
    }

    public int checkFireLevel(Vector3 in_worldPosition)
    {
        Vector2Int? buildingPosition = buildingScript.worldToBuildingCoord(in_worldPosition);
        if(buildingPosition == null)
        {
            return 0;
        }
        return FireArray[buildingPosition.Value.y, buildingPosition.Value.x];
    }
}
