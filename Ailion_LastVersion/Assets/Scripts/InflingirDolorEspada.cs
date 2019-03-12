using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflingirDolorEspada : MonoBehaviour {

    public float periodoAtk;
    private CharacterController2 script;
    private float lastTime = 0;
    private bool ataque = false;
    private AudioSource sonido;

    void Awake()
    {
        script = GetComponent<CharacterController2>();
        sonido = GetComponent<AudioSource>();
    }

    void Update()
    {
        Ataque();
    }

    void Ataque()
    {
        if (Input.GetKeyDown("mouse 1"))
        {
            sonido.Play();
            ataque = true;
            lastTime = Time.time;
        }
        if (ataque && Time.time - lastTime > periodoAtk)
        {
            ataque = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (ataque)
        {
            if (collision.tag == "Enemy")
            {
                EnemyLife life = collision.GetComponent<EnemyLife>();
                life.recibirDaño(100);
                ataque = false;
            }

            if (collision.tag == "Breakable")
            {
                Destruction destr = collision.GetComponent<Destruction>();
                destr.Destruir();
                ataque = false;
            }
        }
    }

}
