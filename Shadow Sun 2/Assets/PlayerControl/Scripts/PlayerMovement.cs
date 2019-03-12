using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Transform playerTransform;
    Rigidbody playerRG;

    //Properties
    public float _Speed = 0.35f;
    public float _JumpSpeed = 1f;
    public float _MaxHorizontalSpeed = 5;

    //Input
    float h, v;

    //Jump
    bool isGrounded = true;

	// Use this for initialization
	void Awake () {
        playerTransform = GetComponent<Transform>();
        playerRG = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        UpdateSpeed();
        Jump();
	}

    void GetInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (!isGrounded)
        {
            h /= 4;
            v /= 4;
        }
    }

    void UpdateSpeed()
    {
        if (Mathf.Abs(playerRG.velocity.z) > _MaxHorizontalSpeed) playerRG.velocity = new Vector3(playerRG.velocity.x, playerRG.velocity.y, _MaxHorizontalSpeed*h);
        else playerRG.AddForce(new Vector3(0, 0, h * _Speed), ForceMode.VelocityChange);
        print(playerRG.velocity.z);
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                isGrounded = false;
                playerRG.AddForce(new Vector3(0,_JumpSpeed,0), ForceMode.VelocityChange);
                //vel += new Vector3(0,_JumpSpeed,0);
            }
        }
        else
        {
            playerRG.AddForce(new Vector3(0,Physics.gravity.y*2, 0), ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckFloor(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        CheckTakeOff(collision);
    }

    void CheckFloor(Collision collision)
    {
        if(collision.collider.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    void CheckTakeOff(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        { 
            isGrounded = false;
        }
    }

}
