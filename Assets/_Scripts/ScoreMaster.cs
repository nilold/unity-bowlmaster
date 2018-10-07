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

        // your code here

        return frameList;
    }
}
