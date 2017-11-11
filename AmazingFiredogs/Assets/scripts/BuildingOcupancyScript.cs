using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOcupancyScript : MonoBehaviour
{
    public float maxTimeToNextBlock = 1f;
    public GameObject[] piecePrefab;

    public const int GridWidth = 8;
    public const int GridHeight = 100;

    public bool[,] OcupancyGrid;
    public GameObject[] HighestStairCase;
    public int[] HighestStairCaseHeight;

    private int currentHeight = 0;

    private float timeSinceLastBlock = 0f; 

    // Use this for initialization
    void Start ()
    {
        OcupancyGrid = new bool[GridHeight, GridWidth];
        HighestStairCase = new GameObject[GridWidth];
        HighestStairCaseHeight = new int[GridWidth];

        //Always start with a lobby piece!
        addPiece(PieceControllerScript.PieceType.LOBBY);
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
	void Update ()
    {
        timeSinceLastBlock += Time.deltaTime;

        if(timeSinceLastBlock > maxTimeToNextBlock)
        {
            timeSinceLastBlock = 0f;
            addPiece(getRandomPiece());
        }
	}

    //Do a nicer random, based roughly on Tetris!
    private int pieceIndex = 0;
    private PieceControllerScript.PieceType[] pieceList;
    private PieceControllerScript.PieceType getRandomPiece()
    {
        if (pieceList == null || pieceIndex == pieceList.Length)
        {
            pieceList = new PieceControllerScript.PieceType[PieceControllerScript.NumberOfPieceTypes * 2];

            //Populate list...
            for (int i = 0; i < pieceList.Length; i++)
                pieceList[i] = (PieceControllerScript.PieceType)((i % PieceControllerScript.NumberOfPieceTypes) + 1);

            //Shuffle list...
            for (int i = 0; i < pieceList.Length; i++)
            {
                PieceControllerScript.PieceType tempPiece;
                int randomIndex = Random.Range(i, pieceList.Length);
                tempPiece = pieceList[i];
                pieceList[i] = pieceList[randomIndex];
                pieceList[randomIndex] = tempPiece;
            }

            pieceIndex = 0;
        }

        return pieceList[pieceIndex++];
    }

    private void addPiece(PieceControllerScript.PieceType piece)
    {
        GameObject tempGO;
        PieceControllerScript tempPC;
        int offsetFromLeft = 0;
        int height;

        if(piece == PieceControllerScript.PieceType.LOBBY)
        {
            offsetFromLeft = 0;
            tempGO = Instantiate(piecePrefab[0]);
            tempPC = tempGO.GetComponent<PieceControllerScript>();
            tempPC.initPiece(this.gameObject, PieceControllerScript.PieceType.LOBBY, offsetFromLeft);

            height = 0;
        }
        else if(piece == PieceControllerScript.PieceType.I)
        {
            offsetFromLeft = Random.Range(0, GridWidth - 3);
            tempGO = Instantiate(piecePrefab[1]);
            tempPC = tempGO.GetComponent<PieceControllerScript>();
            tempPC.initPiece(this.gameObject, PieceControllerScript.PieceType.I, offsetFromLeft);

            height = checkDropHeight(offsetFromLeft, tempPC.getPieceOccupancy());
        }
        else if (piece == PieceControllerScript.PieceType.S)
        {
            offsetFromLeft = Random.Range(0, GridWidth - 1);
            tempGO = Instantiate(piecePrefab[2]);
            height = 0;
            tempPC = tempGO.GetComponent<PieceControllerScript>();
            tempPC.initPiece(this.gameObject, PieceControllerScript.PieceType.S, offsetFromLeft);

            height = checkDropHeight(offsetFromLeft, tempPC.getPieceOccupancy());
        }
        else if (piece == PieceControllerScript.PieceType.L)
        {
            offsetFromLeft = Random.Range(0, GridWidth - 1);
            tempGO = Instantiate(piecePrefab[3]);
            height = 0;
            tempPC = tempGO.GetComponent<PieceControllerScript>();
            tempPC.initPiece(this.gameObject, PieceControllerScript.PieceType.L, offsetFromLeft);

            height = checkDropHeight(offsetFromLeft, tempPC.getPieceOccupancy());
        }
        else
        {
            offsetFromLeft = Random.Range(0, GridWidth - 2);
            tempGO = Instantiate(piecePrefab[4]);
            height = 0;
            tempPC = tempGO.GetComponent<PieceControllerScript>();
            tempPC.initPiece(this.gameObject, PieceControllerScript.PieceType.T, offsetFromLeft);

            height = checkDropHeight(offsetFromLeft, tempPC.getPieceOccupancy());
        }

        addBlockAtHeight(height, offsetFromLeft, tempPC.getPieceOccupancy());
        tempPC.setHeightAndPosition(height);

        Debug.Log("Piece Added!");
    }
}
