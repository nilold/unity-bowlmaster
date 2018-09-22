using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private Vector3 startPos, endPos;
    private float startTime, endTime;


	void Start () {
        ball = GetComponent<Ball>();
	}
	
    public void OnDragStart(){
        startPos = Input.mousePosition;
        startTime = Time.time;
    }

    public void OnDragEnd()
    {
        Vector3 launchVelocity = GetLaunchVelocity();
        ball.Launch(launchVelocity);
    }

    private Vector3 GetLaunchVelocity()
    {
        endPos = Input.mousePosition;
        endTime = Time.time;
        Vector3 posDiff = endPos - startPos;
        posDiff.z = posDiff.y;
        posDiff.y = 0;

        Vector3 speed = posDiff / (endTime - startTime);

        return speed;
    }
}
