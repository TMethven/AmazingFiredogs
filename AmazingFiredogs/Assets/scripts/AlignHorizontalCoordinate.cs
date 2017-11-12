using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignHorizontalCoordinate : MonoBehaviour {

    public GameObject baseGameObject;
    public GameObject offsetGameObject;
    public float offset;
    bool aligned = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!aligned && offsetGameObject)
        {
            Vector3 offsetPos = offsetGameObject.transform.position;
            Vector3 gameObjectPos = this.transform.position;
            offset = gameObjectPos.x - offsetPos.x;
        }
        if (!aligned && baseGameObject)
        {
            Vector3 basePos = baseGameObject.transform.position;
            Vector3 gameObjectPos = this.transform.position;
            float alignDiffX = gameObjectPos.x - offset - basePos.x;
            gameObjectPos.x -= alignDiffX;
            this.transform.position = gameObjectPos;
            aligned = true;
        }
	}
}
