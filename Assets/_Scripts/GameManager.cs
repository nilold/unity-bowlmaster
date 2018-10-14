using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PinSetter pinSetter;
    PinCounter pinCounter;
    ScoreDisplay scoreDisplay;
    Ball ball;
    //ActionMaster actionMaster = new ActionMaster();
    //ScoreMaster scoreMaster = new ScoreMaster();
    List<int> bowls = new List<int>();

    void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
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

        DisplayScores();
        ball.Reset();

        return action;
    }

    private void DisplayScores()
    {
        List<int> scores = ScoreMaster.ScoreCumulative(bowls);
        scoreDisplay.FillRollCard(bowls, scores);
    }
}
