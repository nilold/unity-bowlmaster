using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schreder : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pin>())
        {
            Destroy(other.gameObject);
        }
    }
}
