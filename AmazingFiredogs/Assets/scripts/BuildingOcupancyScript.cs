using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOcupancyScript : MonoBehaviour {

    public const uint GridWidth = 4;
    public const uint GridHeight = 100;

    public bool[,] OcupancyGrid;
    public GameObject[] HighestStairCase;
    public int[] HighestStairCaseHeight;

    private int currentHeight = 0; 

    // Use this for initialization
    void Start () {
        OcupancyGrid = new bool[GridHeight, GridWidth];
        HighestStairCase = new GameObject[GridWidth];
        HighestStairCaseHeight = new int[GridWidth];
    }

    public int checkDropHeight(int leftSideOffset, bool[,] blockOcupancyGrid)
    {
        int blockHeight = blockOcupancyGrid.GetLength(0);
        int blockWidth = blockOcupancyGrid.GetLength(1);
        bool colides = false;
        int returnHeight = 0;
        for( int heightIndex = currentHeight; heightIndex >= 0; --heightIndex)
        {
            for( int h = 0; h < blockHeight; ++h)
            {
                for(int w = 0; w < blockWidth; ++w)
                {
                    if(blockOcupancyGrid[h,w] && OcupancyGrid[heightIndex+h, leftSideOffset+w])
                    {
                        colides = true;
                        break;
                    }
                }
                if (colides) { break; }
            }
            if (colides) {
                returnHeight = heightIndex + 1;
                break;
            }
        }
        return returnHeight;
    }

    public void addBlockAtHeight(int height, int leftSideOffset, bool[,] blockOcupancyGrid)
    {
        int blockHeight = blockOcupancyGrid.GetLength(0);
        int blockWidth = blockOcupancyGrid.GetLength(1);
        int possibleCurrentHeight = height + blockHeight;
        if (possibleCurrentHeight > currentHeight)
        {
            currentHeight = possibleCurrentHeight;
        }
        for (int h = 0; h < blockHeight; ++h)
        {
            for (int w = 0; w < blockWidth; ++w)
            {
                OcupancyGrid[height + h, leftSideOffset + w] |= blockOcupancyGrid[h, w];
            }
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
