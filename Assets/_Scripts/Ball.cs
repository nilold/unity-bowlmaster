using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody ballBody;
    private AudioSource audioSource;

    private Floor floor;
    public bool launched = false;
    private Vector3 originalPostion;

    [SerializeField] Vector3 launchVelocity;



    void Start()
    {
        ballBody = GetComponent<Rigidbody>();
        ballBody.useGravity = false;
        audioSource = GetComponent<AudioSource>();
        floor = FindObjectOfType<Floor>();
        originalPostion = transform.position;
    }

    void Update()
    {

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

    public void Reset()
    {
        print("Reseting ball");
        launched = false;
        gameObject.transform.position = originalPostion;
        ballBody.velocity = Vector3.zero;
        ballBody.angularVelocity = Vector3.zero;

    }

    public void Launch(Vector3 velocity)
    {
        launched = true;
        ballBody.useGravity = true;
        ballBody.velocity = velocity;
        audioSource.Play();
    }

}
