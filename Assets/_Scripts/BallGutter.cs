using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGutter : MonoBehaviour {

    PinSetter pinSetter;

	// Use this for initialization
	void Start () {
        pinSetter = FindObjectOfType<PinSetter>();

        if(!pinSetter){
            Debug.LogError("Could not find pinSetter");
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Ball>()){
            pinSetter.BallLeftTheBox();
        }
    }
}
