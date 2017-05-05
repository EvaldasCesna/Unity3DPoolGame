using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBehaviour : MonoBehaviour
{
    public GameObject back;
    public bool ballHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CueBall")
        {
            other.gameObject.GetComponent<BallBehaviour>().push();

            ballHit = true;
        }
    }

    public void resetPosition()
    {
        transform.position = back.transform.position;
    }

}
