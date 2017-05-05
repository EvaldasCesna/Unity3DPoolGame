using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private float turnSpeed = 5.0f;

    public GameObject cue;
    public GameObject back;
    public Vector3 offset;
    public Transform cueBallTransform;
    public Camera cam2;
    public GameObject ControlImage;


    void Start()
    {
        offset = new Vector3(cueBallTransform.position.x - 0.35f, cueBallTransform.position.y - 0.35f, cueBallTransform.position.z);
    }
    void LateUpdate()
    {
        guideLine();

        //Only be able to move if the ball exists
        if (cueBallTransform != null)
        {
            moveCam();
            moveCue();
        }
        switchCamera();
    }

    public void switchCamera()
    {
        if (Input.GetButtonDown("Camera"))
        {
            if (cam2.enabled == true)
            {
                cam2.enabled = false;
            }
            else
            {
                cam2.enabled = true;
            }
        }
    }

    public void moveCam()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ControlImage.transform.rotation = Quaternion.Euler(Vector3.zero);
            //Locks Cursor to screen if not running in fullscreen
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            turnSpeed = 0;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ControlImage.transform.rotation = Quaternion.Euler(0, 0, -90);
            turnSpeed = 5.0f;
        }
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;

        transform.position = cueBallTransform.position + offset;

        transform.LookAt(cueBallTransform.position);
    }

    public void moveCue()
    {
        cue.transform.LookAt(new Vector3(cueBallTransform.position.x, cueBallTransform.transform.position.y, cueBallTransform.transform.position.z));
        cue.transform.Rotate(new Vector3(1.0f, 0, 0), 90);
        float mouseInput = Convert.ToSingle(Input.GetAxis("Mouse Y") * 0.02);

        if (cue.activeSelf)
        {
            if (Input.GetAxis("Mouse Y") < 0f && Input.GetButton("Fire1"))
            {
                //Maximum Cue pullback
                if (Vector3.Distance(cue.transform.position, cueBallTransform.position) < 2.25f)
                    cue.transform.position = Vector3.MoveTowards(cue.transform.position, cueBallTransform.position, mouseInput);
            }
            if (Input.GetAxis("Mouse Y") > 0f && Input.GetButton("Fire1"))
            {
                cue.transform.position = Vector3.MoveTowards(cue.transform.position, back.transform.position, mouseInput);
            }
        }
    }

    public void guideLine()
    {
        GetComponent<LineRenderer>().enabled = false;
        if (cue.activeSelf)
        {
            GetComponent<LineRenderer>().enabled = true;
            Ray ray = new Ray(cueBallTransform.position, new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GetComponent<LineRenderer>().SetPositions(new[] { cueBallTransform.position, hit.point });
            }
        }
    }
}
