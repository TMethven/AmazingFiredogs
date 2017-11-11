using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignVerticalCoordinate : MonoBehaviour {

    public GameObject baseGameObject;
    public GameObject offsetGameObject;
    public float offset;
    bool aligned = false;

	// Use this for initialization
	void Start () {
        bool bla = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!aligned && offsetGameObject)
        {
            Vector3 offsetPos = offsetGameObject.transform.position;
            Vector3 gameObjectPos = this.transform.position;
            offset = gameObjectPos.y - offsetPos.y;
        }
        if (!aligned && baseGameObject)
        {
            Vector3 basePos = baseGameObject.transform.position;
            Vector3 gameObjectPos = this.transform.position;
            float alignDiffY = gameObjectPos.y - offset - basePos.y;
            gameObjectPos.y -= alignDiffY;
            this.transform.position = gameObjectPos;
            aligned = true;
        }
	}
}
