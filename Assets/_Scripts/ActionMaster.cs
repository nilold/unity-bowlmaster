using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{

    public enum Action { Tidy, Reset, EndTurn, EndGame };

    private int[] bowls = new int[21];
    private int bowl = 1;


    public static Action NextAction(List<int> pinFalls){
        ActionMaster am = new ActionMaster();
        Action currentAction = new Action();

        foreach(int pinFall in pinFalls){
            currentAction = am.Bowl(pinFall);
        }

        return currentAction;
    }

    Action Bowl(int pins)
    {
        Debug.Log("bowl: " + bowl);

        if (pins < 0 || pins > 10)
            throw new UnityException("Invalid number of pins: " + pins.ToString());


        bowls[bowl - 1] = pins;

        //Last bowl always ends game
        if (bowl == 21)
        {
            bowl = 1;
            return Action.EndGame;
        }

        if (bowl >= 20 && !Bowl21Awarded())
        {
            bowl = 1;
            return Action.EndGame;
        }

        if (bowl == 20 && pins == 10)
        {
            bowl++;
            return Action.Reset;
        }


        if (bowl == 19 && pins == 10)
        {
            bowl++;
            return Action.Reset;
        }
        else if (bowl == 20)
        {
            bowl++;
            if (bowls[19 - 1] + pins == 10 && pins > 0)
            {
                return Action.Reset;
            }
            else if (Bowl21Awarded())
            {
                return Action.Tidy;
            }
            else
            {
                return Action.EndGame;
            }
        }


        if (bowl % 2 == 0) //2nd bowl of frame, always ends turn
        {
            bowl++;
            return Action.EndTurn;
        }

        //Strike
        if (pins == 10)
        {
            bowl += 2;
            return Action.EndTurn;
        }

        bowl++;
        return Action.Tidy;

    }


    private bool Bowl21Awarded()
    {
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }
}
