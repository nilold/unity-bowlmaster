using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;
    [SerializeField] float distanceToRaise = 40f;
    
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Raise(){
        Vector3 pinPos = transform.position;
        float newY = pinPos.y + distanceToRaise;
        transform.position = new Vector3(pinPos.x, newY, pinPos.z);
        GetComponent<Rigidbody>().useGravity = false;
    }

    public bool IsStanding(){
        //TODO: it works only when pins area completely still
        float zAngle = Mathf.Abs(transform.eulerAngles.z);
        float xAngle = Mathf.Abs(270 - transform.eulerAngles.x);
        return zAngle < standingThreshold && xAngle < standingThreshold;
    }

    public void Lower()
    {
        Vector3 pinPos = transform.position;
        float newY = pinPos.y - distanceToRaise;
        transform.position = new Vector3(pinPos.x, newY, pinPos.z);
        GetComponent<Rigidbody>().useGravity = true;
    }
}
