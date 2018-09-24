using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{

    public enum Action { Tidy, Reset, EndTurn, EndGame };

    private int[] bowls = new int[21];
    private int bowl = 1;

    public Action Bowl(int pins)
    {
        Debug.Log("bowl: " + bowl);

        if (pins < 0 || pins > 10)
            throw new UnityException("Invalid number of pins: " + pins.ToString());


        bowls[bowl - 1] = pins;

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

        if (pins == 10)
        {
            bowl += 2;
            return Action.EndTurn;
        }

        if (bowl % 2 != 0)
        { //Middle of frame (or last frame)
            bowl++;
            return Action.Tidy;
        }

        bowl++;
        return Action.EndTurn;

    }


    private bool Bowl21Awarded()
    {
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }
}
