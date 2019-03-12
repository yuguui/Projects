﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform playerCam, character, centerPoint;

    private float mouseX, mouseY;
    public float mouseSensitivity = 10f;
    public float mouseYPosition = 1f;

    private float moveFB, moveLR;
    public float moveSpeed = 2f;

    private float zoom;
    public float zoomSpeed = 10;

    public float zoomMin = 0f;
    public float zoomMax = 70f;

    public float rotationSpeed = 5f;



    // Use this for initialization
    void Start()
    {

        zoom = -3;

    }

    // Update is called once per frame
    void Update()
    {

        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (zoom > zoomMin)
            zoom = zoomMin;

        if (zoom < zoomMax)
            zoom = zoomMax;

        playerCam.transform.localPosition = new Vector3(0, 10*((-zoom)/10), zoom-30);

//        if (Input.GetMouseButton(1))
//        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");
//        }

        mouseY = Mathf.Clamp(mouseY, -55f, 50f);
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = character.rotation * movement;
        character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition +2*(-zoom/30), character.position.z);

        if (Input.GetAxis("Vertical") > 0 | Input.GetAxis("Vertical") < 0 | Input.GetAxis("Horizontal") > 0 | Input.GetAxis("Horizontal")< 0)
        {

            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);

            character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);

        }

    }
}