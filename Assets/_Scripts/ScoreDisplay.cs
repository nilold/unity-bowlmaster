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

    public void FillRollCard(List<int> rolls, List<int> scores)
    {
        int i = 0;
        foreach(int roll in rolls){
            bowlScores[i].text = roll.ToString();
            i++;
        }

        i = 0;
        foreach(int score in scores){
            frameScores[i].text = score.ToString();
            i++;
        }

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
