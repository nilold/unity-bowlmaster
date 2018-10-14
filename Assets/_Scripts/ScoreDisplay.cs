using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] GameObject[] frames;
    Text[] bowlScores = new Text[21];
    Text[] frameScores = new Text[10];

    private void Start()
    {
        WireUp();
        Reset();

    }

    public void FillRolls(List<int> rolls)
    {
        string output = FormatRolls(rolls);
        for (int i = 0; i < output.Length; i++){
            bowlScores[i].text = output[i].ToString();
        }
    }

    public void FillFrames (List<int> frames){
        for (int i = 0; i < frames.Count; i++){
            frameScores[i].text = frames[i].ToString();
        }
    }

    public static string FormatRolls (List<int> rolls){
        string output = "";

        for (int i = 0; i < rolls.Count; i++){
            if(rolls[i] == 0){
                output += "-";
            }else if(rolls[i] == 10){
                output += "X";             // STRIKE
                if (i < 18) { output += " "; }
            }else if(i % 2 == 1 && rolls[i] + rolls[i-1] == 10){
                output += "/";              // SPARE
            }else{
                output += rolls[i].ToString();
            }
        }

        return output;
    }


    private void Reset()
    {
        foreach (Text text in bowlScores)
            text.text = "-";

        foreach (Text text in frameScores)
            text.text = "-";

    }

    private void WireUp()
    {

        int bowlIndex = 0;
        int frameIndex = 0;
        foreach (GameObject frame in frames)
        {
            Text[] boxes = frame.GetComponentsInChildren<Text>();

            bowlScores[bowlIndex] = boxes[0];
            bowlIndex++;
            bowlScores[bowlIndex] = boxes[1];
            bowlIndex++;

            if (frameIndex == 9)
            {
                bowlScores[bowlIndex] = boxes[2];
                print(boxes[2].name);
                frameScores[frameIndex] = boxes[3];
                print(boxes[3].name);
            }
            else
            {
                frameScores[frameIndex] = boxes[2];
                frameIndex++;
            }

        }
    }


}
