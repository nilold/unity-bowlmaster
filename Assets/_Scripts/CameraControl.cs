using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {


    [SerializeField] Ball ball;
    [SerializeField] Vector3 offset;

	// Use this for initialization
	void Start () {
		//offset = new Vector3(0f, 20f, -50f);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.z < 1800){
            transform.position = ball.transform.position + offset;
        }
	}
}
