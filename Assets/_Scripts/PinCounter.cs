using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
    private Pin[] pins;
    private float lastChangeTime;
    private int fallenPins;
    private bool ballLefTheBox;

    public float settleTime = 3f;
    private int lastStandingCount = -1;
    [SerializeField] Text pinsCountText;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pins = FindObjectsOfType<Pin>();
    }

    void Update()
    {
        if (ballLefTheBox)
        {
            pinsCountText.color = Color.red;
            UpdateStandingPins();
        }
    }

    private void UpdateStandingPins()
    {
        int standinPinsCount = CountStandingPins();
        pinsCountText.text = standinPinsCount.ToString();

        if (standinPinsCount != lastStandingCount)
        {
            lastStandingCount = standinPinsCount;
            lastChangeTime = Time.time;
        }
        else
        {
            if (Time.time > lastChangeTime + settleTime)
            {
                PinsHaveSettled();
            }
        }
    }

    int CountStandingPins()
    {
        pins = FindObjectsOfType<Pin>();
        int standingPins = 0;
        foreach (Pin pin in pins)
        {
            if (pin.IsStanding()) standingPins++;
        }
        return standingPins;
    }

    void PinsHaveSettled()
    {
        fallenPins = 10 - lastStandingCount - fallenPins;

        gameManager.Bowl(fallenPins);

        int standinPinsCount = CountStandingPins();
        pinsCountText.text = standinPinsCount.ToString();
        pinsCountText.color = Color.green;
        lastStandingCount = -1;
        ballLefTheBox = false;
    }

    public void ResetCount()
    {
        fallenPins = 0;
        pinsCountText.text = "10";
    }

    void OnTriggerExit(Collider other)
    {
        ballLefTheBox |= other.GetComponent<Ball>();
    }

}
