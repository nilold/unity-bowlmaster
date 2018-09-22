using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsStanding(){
        return (transform.up.y > 1 - standingThreshold/1000);
        //float zAngle = Mathf.Abs(transform.eulerAngles.z);
        //float xAngle = Mathf.Abs(transform.eulerAngles.x);
        //return zAngle < standingThreshold && xAngle < standingThreshold;
    }
}
