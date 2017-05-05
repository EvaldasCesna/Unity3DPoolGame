using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsBehaviour : MonoBehaviour
{
    public bool isSolid;
    public bool isStripe;

    // Use this for initialization
    void Start()
    {
        //Sets a different rotation for each ball on start doesnt effect gameplay but looks better
        transform.eulerAngles = new Vector3(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(0, 1.5f, 0);
        }
    }
}
