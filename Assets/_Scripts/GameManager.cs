using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private Pin[] pins;
    private Ball ball;
    private bool ballLefTheBox;
    private float lastChangeTime;
    private int fallenPins = 0;

    public float settleTime = 3f;
    private int lastStandingCount = -1;
    [SerializeField] Text pinsCountText;
    //[SerializeField] GameObject pinsSet;

    PinSetter pinSetter;
    ActionMaster actionMaster = new ActionMaster();


    void Start () {
        pins = FindObjectsOfType<Pin>();
        ball = FindObjectOfType<Ball>();
        pinSetter = FindObjectOfType<PinSetter>();
    }
	
	void Update () {
        if (ballLefTheBox)
        {
            UpdateStandingPins();
        }
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

    private void PinsHaveSettled()
    {
        fallenPins = 10 - lastStandingCount - fallenPins;
        ActionMaster.Action action = actionMaster.Bowl(fallenPins);

        Debug.Log("fallen pins: " + fallenPins.ToString() + " Action: " + action);

        if (action == ActionMaster.Action.Tidy)
        {
            pinSetter.TidyPins();
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            pinSetter.ResetPins();
            fallenPins = 0;
            pinsCountText.text = "10";
        }
        else if (action == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Dont know how to handle endGame action");
        }

        int standinPinsCount = CountStandingPins();
        pinsCountText.text = standinPinsCount.ToString();

        pinsCountText.color = Color.green;
        ballLefTheBox = false;
        lastStandingCount = -1;
        ball.Reset();
    }

    public void BallLeftTheBox()
    {
        ballLefTheBox = true;
        pinsCountText.color = Color.red;
    }

}
