using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inflingirDaño : MonoBehaviour {

    private float size;
    private CharacterController2 script;

    public float maxSize = 5;

    int tiempoCargando;
    int minCarga;
    int maxCarga;

    void Awake()
    {
        script = GetComponent<CharacterController2>();
        tiempoCargando = script.tiempoCargando;
        minCarga = script.minTiempoCargando;
        maxCarga = script.maxCarga;

        EscalarCarga();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy") {
            Life life = collision.GetComponent<Life>();
            life.recibirDaño(25);
        }
    }

    void EscalarCarga()
    {
        float porcent;
        porcent = tiempoCargando / (maxCarga - minCarga) * 100;
        size = porcent * maxSize / 100;

        transform.localScale = new Vector3(size, size, size);
    }



}
  