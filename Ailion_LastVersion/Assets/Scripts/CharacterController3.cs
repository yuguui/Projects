using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3 : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float distToGrounded = 0.1f;
        public LayerMask ground;
        public bool jumping = false;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }

    public MoveSettings moveSettings = new MoveSettings();
    public InputSettings inputSettings = new InputSettings();
    public PhysSettings physSettings = new PhysSettings();

    Vector3 velocity = Vector3.zero;
    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput, turnInput;
    bool jumpInput, planningInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSettings.distToGrounded, moveSettings.ground);
    }

    void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("Falta el rigibody");

        forwardInput = turnInput = 0;
        jumpInput = false;
    }
    
    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS); //Interpolated
        turnInput = Input.GetAxis(inputSettings.TURN_AXIS); //Interpolated
        jumpInput = Input.GetButtonDown(inputSettings.JUMP_AXIS); // Not Interpolated
        planningInput = Input.GetButton(inputSettings.JUMP_AXIS);

    }

    void Update()
    {
        GetInput();
        Turn();
		Debug.Log ("x =" + velocity.x);
		Debug.Log ("y =" + velocity.y);
		Debug.Log ("z =" + velocity.z);

    }
    void FixedUpdate()
    {
        Run();
        Jump();


        rBody.velocity = transform.TransformDirection(velocity);

    }
    void Run()
    {
		if (Mathf.Abs (forwardInput) > inputSettings.inputDelay) {
			//move
			velocity.z = moveSettings.forwardVel * forwardInput;
		} else {
			//zero velocity
			velocity.z = 0;
			velocity.x = 0;
		}
		
    } 
    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSettings.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }
    void Jump()
    {
        print(jumpInput);
         if(jumpInput == true && Grounded() && !moveSettings.jumping)
        {
            print("1");
            moveSettings.jumping = true;
            velocity.y = moveSettings.jumpVel;
        }

        else if(moveSettings.jumping && !Grounded() && jumpInput == true)
        {
            moveSettings.jumping = false;
            velocity.y = moveSettings.jumpVel;
        }

        else if (!jumpInput && Grounded())
        {
            //zero out our velocity.y
            print(Grounded());
            velocity.y = 0;
            moveSettings.jumping = false;
        }

        else if (planningInput && velocity.y < 0)
        {
            velocity.y = -physSettings.downAccel*4;
        }

        else //Falling
        {
            //decrease velocity.y
            velocity.y -= physSettings.downAccel;
        }
    }
}
