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

        int index = 1;
        int frameScore = 0;
        foreach(int roll in rolls){
            if(index%2 == 1){ //first row of frame
                if(roll == 10){
                    //strike

                } else{
                    frameScore = roll;
                }
            } else{
                //second roll of frame
            }
        }

        return frameList;
    }
}
