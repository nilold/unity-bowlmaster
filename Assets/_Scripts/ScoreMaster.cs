using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster
{

    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();

        int cumulativeTotal = 0;
        foreach (int score in ScoreFrames(rolls))
        {
            cumulativeTotal += score;
            cumulativeScores.Add(cumulativeTotal);
        }


        return cumulativeScores;
    }

    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>();

        int frameScore = 0;
        int bonusScore = 0;

        bool isStriked = false;
        bool handleStrike = false;
        int strikeCount = 0;
        bool isSpared = false;
        int strikeBonusCount = 0;

        int index = 0;

        foreach (int roll in rolls)
        {
            //handle bonuses
            if (isStriked)
            {
                bonusScore += roll;
                strikeBonusCount++;
                if (strikeBonusCount == 2)
                {
                    handleStrike = true;
                }
            }

            if (isSpared)
            {
                bonusScore += roll;
                isSpared = false;
                frameScore += bonusScore;
                frameList.Add(frameScore);
                bonusScore = 0;
                frameScore = 0;
            }



            if (index % 2 == 0)
            { //first row of frame
                if (roll == 10)
                {
                    //strike
                    strikeCount++;
                    isStriked = true;
                    frameScore += roll;
                    index++;
                }
                else
                {
                    frameScore += roll;
                }
            }
            else
            {
                //second roll of frame
                if ((frameScore + roll) % 10 == 0 && frameScore > 0)
                {
                    //spare
                    isSpared = true;
                    frameScore += roll;
                }
                else
                {
                    //normal end of frame
                    frameScore += roll;
                    if (!isStriked)
                    {
                        frameList.Add(frameScore);
                        frameScore = 0;
                    }
                }
            }

            if (handleStrike)
            {
                strikeCount--;
                handleStrike = false;
                frameList.Add(frameScore);
                if (strikeCount == 0)
                {
                    isStriked = false;
                    if (index < 21 && !isSpared)
                        frameList.Add(bonusScore);

                    if (!isSpared)
                    {
                        frameScore = 0;
                        bonusScore = 0;
                    }else{
                        frameScore = bonusScore;
                        bonusScore = 0;
                    }
                    strikeBonusCount = 0;

                }
                else
                {
                    frameScore = bonusScore;
                    bonusScore = roll;
                    strikeBonusCount = 1;
                }
            }

            index++;
        }

        return frameList;
    }
}
