using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2 : MonoBehaviour
{
    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 100;
        public float jumpVel = 25;
        public float latVel = 12;
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
        public string LATERAL_AXIS = "HorizontalMov";
        public KeyCode disparar;
    }

    public MoveSettings moveSettings = new MoveSettings();
    public InputSettings inputSettings = new InputSettings();
    public PhysSettings physSettings = new PhysSettings();

    public GameObject bala;
    public Vector3 velocity;
    public bool disparar;
    public bool planear;
    public bool aire;
    public bool dobleSalto;
    public bool charging;
    public int tiempoCargando;
    public int minTiempoCargando = 30;
    public int maxCarga = 2000;

    private AnimationsTransition script;
    private bool cargar;
    private AudioSource sonido;
    //private bool disparar;

    Quaternion targetRotation;
    Rigidbody rBody;
    GameObject dust;
    GameObject lugarDisparo;
    float forwardInput, turnInput, lateralInput;
    bool jumpInput, planningInput;
    bool cargando = false;
    bool disparando = false;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    bool Grounded()
    {
        // Devuelve true si está tocando el suelo
        return Physics.Raycast(transform.position, Vector3.down, moveSettings.distToGrounded, moveSettings.ground);
    }

    void Start()
    {
        script = GetComponent<AnimationsTransition>();

        sonido = GetComponent<AudioSource>();
        velocity = Vector3.zero;
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("Falta el rigibody");

        forwardInput = turnInput = 0;
        jumpInput = false;
        planear = false;
        aire = false;
        dobleSalto = false;
        charging = false;
        dust = GameObject.Find("DustTrail");
        lugarDisparo = GameObject.Find("CanonShoot");

    }

    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS); //Interpolated
        turnInput = Input.GetAxis(inputSettings.TURN_AXIS); //Interpolated
        jumpInput = Input.GetButtonDown(inputSettings.JUMP_AXIS); // Not Interpolated
        planningInput = Input.GetButton(inputSettings.JUMP_AXIS);
        //if (Input.GetKeyDown(inputSettings.disparar)) charging = true;
        //if (Input.GetKeyUp(inputSettings.disparar)) charging = false;   
        //lateralInput = Input.GetAxis(inputSettings.LATERAL_AXIS);

    }

    void Update()
    {
        GetInput();
        Turn();
        
    }

    void FixedUpdate()
    {
        Run();
        Jump();
        //LateralMovement();
        rBody.velocity = transform.TransformDirection(velocity);
        Shoot();
    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputSettings.inputDelay)
        {
            //move
            velocity.z = moveSettings.forwardVel * forwardInput;
        }
        else
            //zero velocity
            velocity.z = 0;
    }
    void LateralMovement()
    {
        if (Mathf.Abs(lateralInput) > inputSettings.inputDelay)
        {
            velocity.x = moveSettings.latVel * lateralInput;
        }
        else
            velocity.x = 0;
    }
    void Turn()
    {
		Mathf.Abs (turnInput);
        if (Mathf.Abs(turnInput) > inputSettings.inputDelay)
        {
			//Debug.Log ("rotating");
			targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }
    void Jump()
    {
        // Primer Salto: Despegarse del suelo (solo se ejecuta la una vez por salto)
        if (jumpInput == true && Grounded() && !moveSettings.jumping)
        {
            //print("1");
            moveSettings.jumping = true;
            velocity.y = moveSettings.jumpVel;
            if (dust.activeSelf) dust.SetActive(false);
            sonido.Play();
        }

        // Segundo Salto (solo se ejecuta una vez por salto)
        else if (moveSettings.jumping && !Grounded() && jumpInput == true) 
        {
            //print("2");
            moveSettings.jumping = false;
            velocity.y = moveSettings.jumpVel;
            dobleSalto = true;
            sonido.Play();
        }

        // Tocando suelo: No está en el aire
        else if (!jumpInput && Grounded())
        {
            //print("3");
            //zero out our velocity.y
            velocity.y = 0;
            moveSettings.jumping = false;
            aire = false;
            planear = false;
            dobleSalto = false;
            if (!dust.activeSelf) dust.SetActive(true);
        }

        // Planear
        else if (planningInput && velocity.y < 0)
        {
            //print("4");
            velocity.y = -physSettings.downAccel * 4;
            planear = true;
            
        }

        // En el aire sin planear (Mientras salta o cae)
        else
        {
            //print("5");
            //decrease velocity.y
            velocity.y -= physSettings.downAccel;
            aire = true;
            planear = false;
        }
    }

    void Shoot()
    {
        cargar = script.cargar;
        disparar = script.bala;

        if (cargar) tiempoCargando++;
        
        if (disparar && tiempoCargando >= minTiempoCargando)
        {
            Rigidbody disparo = Instantiate(bala, lugarDisparo.transform.position, lugarDisparo.transform.rotation).GetComponent<Rigidbody>();
            disparo.velocity = -50 * new Vector3(lugarDisparo.transform.forward.x, 0, lugarDisparo.transform.forward.z) +3 * (disparo.transform.up);
            tiempoCargando = 0;
            script.bala = false;
        }

        else disparar = false;     
    }

}