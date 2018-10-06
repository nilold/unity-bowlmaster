using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{

    private Pin[] pins;
    private Ball ball;
    private bool ballLefTheBox = false;
    private float lastChangeTime;
    private int fallenPins = 0;

    public float settleTime = 3f;
    private int lastStandingCount = -1;
    [SerializeField] Text pinsCountText;
    [SerializeField] GameObject pinsSet;

    ActionMaster actionMaster = new ActionMaster();

    Animator animator;

    void Start()
    {
        pins = FindObjectsOfType<Pin>();
        ball = FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ballLefTheBox)
        {
            UpdateStandingPins();
        }

    }

    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
                pin.Raise();
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            //if (pin.IsStanding())
            pin.Lower();
        }
        int standinPinsCount = CountStandingPins();
        pinsCountText.text = standinPinsCount.ToString();
    }

    public void RenewPins()
    {
        fallenPins = 0;
        pinsCountText.text = "10";
        Instantiate(pinsSet, new Vector3(0, 40, 1908), Quaternion.identity);

    }


    private void UpdateStandingPins()
    {
        int standinPinsCount = CountStandingPins();
        pinsCountText.text = standinPinsCount.ToString();

        if (standinPinsCount != lastStandingCount)
        {
            lastStandingCount = standinPinsCount;
            lastChangeTime = Time.time;
        }
        else
        {
            if (Time.time > lastChangeTime + settleTime)
            {
                PinsHaveSettled();
            }
        }
    }

    private void PinsHaveSettled()
    {
        fallenPins = 10 - lastStandingCount - fallenPins;
        ActionMaster.Action action = actionMaster.Bowl(fallenPins);

        Debug.Log("fallen pins: " + fallenPins.ToString() + " Action: " + action);

        if (action == ActionMaster.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
        }
        else if (action == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Dont know how to handle endGame action");
        }

        pinsCountText.color = Color.green;
        ballLefTheBox = false;
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Ball>())
    //    {
    //        BallLeftTheBox();
    //    }
    //}

    public void BallLeftTheBox()
    {
        ballLefTheBox = true;
        pinsCountText.color = Color.red;
    }

}
