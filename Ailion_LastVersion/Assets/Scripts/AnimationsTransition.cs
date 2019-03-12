using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTransition : MonoBehaviour {

    private CharacterController2 script;

    // Acceder a variables PUBLIC del script CharacterController2 
    private Vector3 velocity; 
    private bool dobleSalto;
    private bool planear;
    private bool aire;

    private Animator animTronco;
    private Animator animBrazoDer;
    private Animator animBrazoIzq;
    private Animator animPiernaDer;
    private Animator animPiernaIzq;
    private Animator animCola;

    //private GameObject chargeCloud;
    private ParticleSystem chargeCloud;

    public KeyCode cannonAtk; // Botón para el cañón
    public KeyCode swordAtk;  // Botón para la espada

    public bool cargar = false;
    public bool disparar = false;
    public bool cortar = false;
    public bool bajando = false;
    public bool quieto = true;
    public bool bala = false;

    void Start () {
        // Se saca el script cuyas variables se necesitan para las transiciones de las animaciones
        script = GetComponent<CharacterController2>();
        velocity = Vector3.zero;
        aire = false;
        planear = false;
        dobleSalto = false;

        chargeCloud = GameObject.Find("ChargingCannon").GetComponent<ParticleSystem>();
        chargeCloud.Stop();

        // Se localizan las diversas partes del cuerpo y se obtiene su Animator para poder animarlas
        animTronco = GetComponentInChildren<Animator>();
        animBrazoDer = GameObject.FindWithTag("BrazoDer").GetComponent<Animator>();
        animBrazoIzq = GameObject.FindWithTag("BrazoIzq").GetComponent<Animator>();
        animPiernaIzq = GameObject.FindWithTag("PiernaIzq").GetComponent<Animator>();
        animPiernaDer = GameObject.FindWithTag("PiernaDer").GetComponent<Animator>();
        animCola = GameObject.Find("Cola").GetComponent<Animator>();

    }

	void Update () {

        // Se comprueba si jugador ha presionado algún botón de ataque
        if (Input.GetKeyDown(cannonAtk)) cargar = true;
        if (cargar && Input.GetKeyUp(cannonAtk)) disparar = true;
        if (Input.GetKeyDown(swordAtk)) cortar = true;   
        
        // Métodos que gestiona las animaciones de todas las partes del cuerpo
        Actualizar();
        Animar();

        // Se actualiza el valor de la variable velocity del script CharacterController2 
        velocity = script.velocity;
        aire = script.aire;
        planear = script.planear;
        dobleSalto = script.dobleSalto;

        quieto = (velocity == Vector3.zero);
        bajando = (velocity.y < 0);
    }


    void Animar() {

        // Si ataca
        if (cargar || cortar)
        {
            Atacar();
        }

        // Si no está en el aire
        if (!aire) {
            // Si no se mueve -> IDLE
            if (quieto) {
                // Si no ataca
                if (!cargar && !cortar)
                { 
                    animBrazoDer.SetBool("correr", false);
                    animBrazoIzq.SetBool("correr", false);
                }
                animTronco.SetBool("correr", false);
                animPiernaIzq.SetBool("correr", false);
                animPiernaDer.SetBool("correr", false);
                animCola.SetBool("correr", false);
            }

            // Si está en movimiento -> CORRER
            else {
                // Si no ataca
                if (!cargar && !cortar)
                {  
                    animBrazoDer.SetBool("correr", true);
                    animBrazoIzq.SetBool("correr", true);
                }
                animTronco.SetBool("correr", true);
                animPiernaIzq.SetBool("correr", true);
                animPiernaDer.SetBool("correr", true);
                animCola.SetBool("correr", true);               
            }
        }
        
        // Si está en el aire
        if (aire) {

            // Si no está cayendo, está saltando
            if (!bajando)
            {
                // Si se ejecuta doble salto -> SALTO
                if (dobleSalto) {
                    // Si no ataca
                    if (!cargar && !cortar)
                    {
                        animBrazoDer.SetBool("salto", true);
                        animBrazoIzq.SetBool("salto", true);
                    }
                    animTronco.SetBool("salto", true);
                    animPiernaIzq.SetBool("salto", true);
                    animPiernaDer.SetBool("salto", true);
                    animCola.SetBool("salto", true);
                }
            }
        }
    }


     void Atacar() {
        if (cargar) {
            animBrazoIzq.SetBool("disparar", true);
            chargeCloud.Play();

            if (disparar) {
                bala = true;
                cargar = false;
                animBrazoIzq.SetBool("disparar", false);
                chargeCloud.Stop();
                disparar = false;
            }         
        }
        else if (cortar) {
            animBrazoDer.SetTrigger("atacar");
            cortar = false;
        }
    }


    void Actualizar() {
        
        if (!aire) {
            animTronco.SetBool("salto", false);
            animPiernaIzq.SetBool("salto", false);
            animPiernaDer.SetBool("salto", false);
        }

        if (bajando) {
            animBrazoDer.SetBool("bajar", true);
            animBrazoIzq.SetBool("bajar", true);
            animCola.SetBool("bajar", true);

            animCola.SetBool("salto", false);
            animBrazoDer.SetBool("salto", false);
            animBrazoIzq.SetBool("salto", false);
        }
        else {
            animBrazoDer.SetBool("bajar", false);
            animBrazoIzq.SetBool("bajar", false);
            animCola.SetBool("bajar", false);
        }

        if (planear) {
            animBrazoDer.SetBool("planeo", true);
            animBrazoIzq.SetBool("planeo", true);
            animCola.SetBool("planeo", true);
        }

        else {
            animBrazoDer.SetBool("planeo", false);
            animBrazoIzq.SetBool("planeo", false);
            animCola.SetBool("planeo", false);
        }
    }

}
