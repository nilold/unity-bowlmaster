using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    private Pin[] pins;
    [SerializeField] Text pinsCountText;

	// Use this for initialization
	void Start () {
        pins = FindObjectsOfType<Pin>();

	}
	
	// Update is called once per frame
	void Update () {
        pinsCountText.text = CountStandingPins().ToString();
    }

    private int CountStandingPins(){
        int standingPins = 0;
        foreach(Pin pin in pins){
            if (pin.IsStanding()) standingPins++;
        }
        return standingPins;
    }
}
