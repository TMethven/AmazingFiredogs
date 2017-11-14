using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControllerScript : MonoBehaviour
{
    public enum PieceType { LOBBY, I, T, S, L, WALL};
    public static int NumberOfPieceTypes = 5;
    public LayerMask layerMask;

    public PieceType pType; 

    public int offsetFromLeft = 0, offsetFromBottom = 0;

    public static float unitSize = 5.15f;

	Vector3 startPos;
	Vector3 target;
	bool targetSet = false;

	float dropTime = 1.0f;
	float dropTimer;

	void Start()
    {
		dropTimer = 0;
	}
	
	void Update()
    {
		if (targetSet && dropTimer <= dropTime) {
			dropTimer += Time.deltaTime;
			float lerpAmount = Mathf.Min(dropTimer / dropTime, 1);
			transform.localPosition = Vector3.Lerp(startPos, target, lerpAmount);
		}
	}

    public void initPiece(GameObject parent, PieceType piece, int offsetFromLeft)
    {
        this.transform.SetParent(parent.transform);
        this.offsetFromLeft = offsetFromLeft;
    }

    public void setHeightAndPosition(int height)
    {
        offsetFromBottom = height;

		targetSet = true;
        target = new Vector3(offsetFromLeft * unitSize, offsetFromBottom * unitSize);
		startPos = new Vector3(target.x, target.y + 30);
		transform.localPosition = startPos;
        findLowerStairWell();
    }

    private void findLowerStairWell()
    {
        if (pType == PieceType.LOBBY || pType == PieceType.WALL)
            return;

        Transform sprite = this.gameObject.transform.Find("Sprite");
        GameObject thisStairWell = sprite.Find("Stairwell").gameObject;
        Stairwell sourceSW = thisStairWell.GetComponent<Stairwell>();

        RaycastHit2D hit = Physics2D.Raycast(thisStairWell.transform.position - new Vector3(0, 2.5f, 0), Vector2.down, 50f, layerMask);
        Debug.DrawRay(thisStairWell.transform.position - new Vector3(0, 2.5f, 0), Vector2.down, Color.magenta, 10f);

        if(hit.collider != null)
        {
            GameObject target = hit.collider.gameObject;
            Stairwell targetSW = target.GetComponent<Stairwell>();

            targetSW.stairwellAbove = sourceSW.transform;
            sourceSW.stairwellBelow = targetSW.transform;
        }
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
        else if (pType == PieceType.T)
            return TPiece;
        else
            return Wall;
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
        { true, true, true, true },
        { false, false, false, true }
        
    };

    private bool[,] Wall = new bool[,]
    {
        { true }
    };
}
