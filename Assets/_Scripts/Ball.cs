using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody ballBody;
    private AudioSource audioSource;

    private Floor floor;
    private bool launched = false;

    [SerializeField] Vector3 launchVelocity;


    // Use this for initialization
    void Start()
    {
        ballBody = GetComponent<Rigidbody>();
        ballBody.useGravity = false;
        audioSource = GetComponent<AudioSource>();
        floor = FindObjectOfType<Floor>();

        //Launch(launchVelocity);
    }

    public void MoveStart(float xNudge)
    {
        if (!launched)
        {
            float newX = gameObject.transform.position.x + xNudge;
            float newY = gameObject.transform.position.y;
            float newZ = gameObject.transform.position.z;

            newX = ClampX(newX);

            transform.position = new Vector3(newX, newY, newZ);
        }
    }

    private float ClampX(float newX)
    {
        float laneBound = floor.GetComponent<Renderer>().bounds.size.x / 2;

        if (newX > laneBound) newX = laneBound;
        if (newX < -laneBound) newX = -laneBound;

        return newX;
    }

    public void Launch(Vector3 velocity)
    {
        launched = true;
        ballBody.useGravity = true;
        ballBody.velocity = velocity;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
