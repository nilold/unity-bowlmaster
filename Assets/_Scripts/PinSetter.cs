using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{

    private Pin[] pins;
    private Ball ball;
    private bool ballEnteredBox = false;
    private float lastChangeTime;
    private int fallenPins = 0;

    public float settleTime = 3f;
    public int lastStandingCount = -1;
    [SerializeField] Text pinsCountText;
    [SerializeField] GameObject pinsSet;

    ActionMaster actionMaster = new ActionMaster();
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    Animator animator;

    void Start()
    {
        pins = FindObjectsOfType<Pin>();
        ball = FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ballEnteredBox)
        {
            UpdateStandingPins();
        }

    }

    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
                pin.Raise();
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            //if (pin.IsStanding())
                pin.Lower();
        }
        int standinPinsCount = CountStandingPins();
        pinsCountText.text = standinPinsCount.ToString();
    }

    public void RenewPins()
    {
        fallenPins = 0;
        pinsCountText.text = "10";
        Instantiate(pinsSet, new Vector3(0, 40, 1908), Quaternion.identity);

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

    private void PinsHaveSettled()
    {
        fallenPins = 10 - lastStandingCount - fallenPins;

        ActionMaster.Action action =  actionMaster.Bowl(fallenPins);
        if(action == tidy){
            Debug.Log("tidyTrigger");
            animator.SetTrigger("tidyTrigger");
        } else{
            Debug.Log("resetTrigger");
            animator.SetTrigger("resetTrigger");
        }

        Debug.Log("fallen pins: " + fallenPins.ToString());

        pinsCountText.color = Color.green;
        ballEnteredBox = false;
        lastStandingCount = -1;
        ball.Reset();
    }

    private int CountStandingPins()
    {
        pins = FindObjectsOfType<Pin>();
        int standingPins = 0;
        foreach (Pin pin in pins)
        {
            if (pin.IsStanding()) standingPins++;
        }
        return standingPins;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>())
        {
            HandleBallEntered();
        }
    }

    private void HandleBallEntered()
    {
        ballEnteredBox = true;
        pinsCountText.color = Color.red;
    }

}
