using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{

    [SerializeField] GameObject pinsSet;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TidyPins(){
        animator.SetTrigger("tidyTrigger");
    }

    public void ResetPins()
    {
        animator.SetTrigger("resetTrigger");
    }


    // called by animator
    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                pin.Raise();
            }

        }
    }

    // called by animator
    public void LowerPins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    // called by animator
    public void RenewPins()
    {
        Instantiate(pinsSet, new Vector3(0, 40, 1908), Quaternion.identity);

    }
}
