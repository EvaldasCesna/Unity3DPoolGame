using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
    private Rigidbody rig;

    public bool isMoving;
    public Transform spawn;
    public float speed;
    public float maxSpeed;
    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if(transform.position.y < 0f)
        {
            resetPos();
        }
    }

    public void push()
    {
        float inputSpeed = (speed * Input.GetAxis("Mouse Y"));

        if  (inputSpeed > maxSpeed)
        {
            inputSpeed = maxSpeed;
        }

        rig.AddForceAtPosition(new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z) * inputSpeed * Time.deltaTime, transform.position);
    }

    public void resetPos()
    {
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;
        transform.position = new Vector3(spawn.position.x, spawn.position.y + 0.004f, spawn.position.z);
    }

}
