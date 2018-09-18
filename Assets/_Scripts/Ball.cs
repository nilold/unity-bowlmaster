using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody ballBody;
    private AudioSource audioSource;

    [SerializeField] Vector3 launchVelocity;


	// Use this for initialization
	void Start ()
    {
        ballBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Launch();
    }

    public void Launch()
    {
        ballBody.velocity = launchVelocity;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
