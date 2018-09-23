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
        float zAngle = Mathf.Abs(transform.eulerAngles.z);
        float xAngle = Mathf.Abs(270 - transform.eulerAngles.x);
        return zAngle < standingThreshold && xAngle < standingThreshold;
    }
}
