using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGutter : MonoBehaviour {

    GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();

        if(!gameManager)
        {
            Debug.LogError("Could not find gameManager");
        }
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Ball>()){
            gameManager.BallLeftTheBox();
        }
    }
}
