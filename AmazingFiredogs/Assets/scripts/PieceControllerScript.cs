using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControllerScript : MonoBehaviour
{
    public enum PieceType { LOBBY, I, T, S, L};
    public static int NumberOfPieceTypes = 4;

    public PieceType pType; 

    public int offsetFromLeft = 0, offsetFromBottom = 0;

    private float unitSize = 1f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void initPiece(GameObject parent, PieceType piece, int offsetFromLeft)
    {
        this.transform.SetParent(parent.transform);

        this.offsetFromLeft = offsetFromLeft;
    }

    public void setHeightAndPosition(int height)
    {
        offsetFromBottom = height;

        this.transform.localPosition = new Vector3(offsetFromLeft * unitSize, offsetFromBottom * unitSize);
    }

    public bool[,] getPieceOccupancy()
    {
        if (pType == PieceType.LOBBY)
            return LobbyPiece;
        else if (pType == PieceType.I)
            return IPiece;
        else if (pType == PieceType.L)
            return LPiece;
        else if (pType == PieceType.S)
            return SPiece;
        else
            return TPiece;
    }


    /**
     * Here I define the piece shapes in arrays. Please note, they are defined 'upside down' for
     * simplicity when iterating through them in the BuildingOccupancyScript.
     */
    private bool[,] LobbyPiece = new bool[,]
    {
        { true, true, true, true, true, true, true, true }
    };

    private bool[,] IPiece = new bool[,]
    {
        { true, true, true, true }
    };

    private bool[,] TPiece = new bool[,]
    {
        { true, true, true },
        { false, true, false }
    };

    private bool[,] SPiece = new bool[,]
    {
        {true, false },
        {true, true },
        {false, true }
    };

    private bool[,] LPiece = new bool[,]
    {
        { true, true, true },
        { false, false, true }
        
    };
}
