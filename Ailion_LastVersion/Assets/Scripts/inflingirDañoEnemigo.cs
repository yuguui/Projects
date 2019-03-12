using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inflingirDañoEnemigo : MonoBehaviour {

    private float size;
    private CharacterController2 script;
    private AudioSource sonido;

    public float maxSize = 5;

    int tiempoCargando = 0;
    int maxCarga = 0;

    void Awake()
    {
        script = GetComponent<CharacterController2>();
        sonido = GetComponent<AudioSource>();
        sonido.Play();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Life life = collision.GetComponent<Life>();
            life.recibirDaño(25);
        }
    }

}
