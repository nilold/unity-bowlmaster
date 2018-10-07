using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PinSetter pinSetter;
    PinCounter pinCounter;
    Ball ball;
    ActionMaster actionMaster = new ActionMaster();
    List<int> bowls = new List<int>();

    void Start()
    {
        pinSetter = FindObjectOfType<PinSetter>();
        pinCounter = FindObjectOfType<PinCounter>();
        ball = FindObjectOfType<Ball>();
    }

    //called by PinCounter
    public ActionMaster.Action Bowl(int fallenPins)
    {
        bowls.Add(fallenPins);
        ActionMaster.Action action = ActionMaster.NextAction(bowls);

        Debug.Log("fallen pins: " + fallenPins.ToString() + " Action: " + action);
        switch (action)
        {
            case ActionMaster.Action.Tidy:
                pinSetter.TidyPins();
                break;
            case ActionMaster.Action.EndTurn:
                pinSetter.ResetPins();
                pinCounter.ResetCount();
                break;
            case ActionMaster.Action.EndGame:
                throw new UnityException("Dont know how to handle endGame action");
        }

        ball.Reset();

        return action;
    }

}
