using System;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{

    private Pin[] pins;
    private Ball ball;
    private bool ballEnteredBox = false;
    private float lastChangeTime;

    public float settleTime = 3f;
    public int lastStandingCount = -1;
    [SerializeField] Text pinsCountText;

    // Use this for initialization
    void Start()
    {
        pins = FindObjectsOfType<Pin>();
        ball = FindObjectOfType<Ball>();
        //print(ball);

    }

    // Update is called once per frame
    void Update()
    {
        if (ballEnteredBox){
            int standinPinsCount = CountStandingPins();
            pinsCountText.text = standinPinsCount.ToString();
            CheckLastStandingCount(standinPinsCount);
        }
           
    }

    private void CheckLastStandingCount(int standinPinsCount)
    {
        if(standinPinsCount != lastStandingCount){
            lastStandingCount = standinPinsCount;
            lastChangeTime = Time.time;
        } else{
            if (Time.time > lastChangeTime + settleTime)
            {
                PinsHaveSettled();
            }
        }


    }

    private void PinsHaveSettled()
    {
        pinsCountText.color = Color.green;
        ballEnteredBox = false;
        lastStandingCount = -1;
        ball.Reset();
    }

    private int CountStandingPins()
    {
        pins = FindObjectsOfType<Pin>();
        int standingPins = 0;
        foreach (Pin pin in pins)
        {
            if (pin.IsStanding()) standingPins++;
        }
        return standingPins;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>())
        {
            HandleBallEntered();
        }
    }

    private void HandleBallEntered()
    {
        ballEnteredBox = true;
        pinsCountText.color = Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pin>())
        {
            Destroy(other);
        }
    }
}
