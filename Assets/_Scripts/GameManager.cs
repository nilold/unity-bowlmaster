using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    PinSetter pinSetter;
    PinCounter pinCounter;
    ActionMaster actionMaster = new ActionMaster();

    Ball ball;

    void Start()
    {
        pinSetter = FindObjectOfType<PinSetter>();
        pinCounter = FindObjectOfType<PinCounter>();
        ball = FindObjectOfType<Ball>();
    }

    //called by PinCounter
    public ActionMaster.Action PinsHaveSettled(int fallenPins)
    {
        ActionMaster.Action action = actionMaster.Bowl(fallenPins);

        Debug.Log("fallen pins: " + fallenPins.ToString() + " Action: " + action);

        if (action == ActionMaster.Action.Tidy)
        {
            pinSetter.TidyPins();
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            pinSetter.ResetPins();
            pinCounter.ResetCount();
        }
        else if (action == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Dont know how to handle endGame action");
        }

        ball.Reset();

        return action;
    }

}
