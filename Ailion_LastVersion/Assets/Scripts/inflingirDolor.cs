using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inflingirDolor : MonoBehaviour {

    private float size;
    private int porcent;
    private int daño;
    private GameObject player;
    private CharacterController2 script;
    private AudioSource sonido;

    public float maxSize = 5;

    int tiempoCargando = 0;
    int maxCarga = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        script = player.GetComponent<CharacterController2>();
        sonido = GetComponent<AudioSource>();
        sonido.Play();
    }
    
    void Update ()
    {
        tiempoCargando = script.tiempoCargando;
        maxCarga = script.maxCarga;

        EscalarCarga();
    }

    void OnTriggerEnter(Collider collision)
    {
        new WaitForSeconds(0.1f);
        if (collision.tag == "Enemy") {
            EnemyLife life = collision.GetComponent<EnemyLife>();
            life.recibirDaño(daño);
            Destroy(this.gameObject);
        }

        if (collision.tag == "Breakable") {
            Destruction destr = collision.GetComponent<Destruction>();
            destr.Destruir();
            Destroy(this.gameObject);
        }
    }

    void EscalarCarga()
    {
        porcent = Mathf.CeilToInt(tiempoCargando * 100 / maxCarga)+25;
        size = porcent * maxSize / 100;

        daño = (porcent < 25)? 25 : porcent;

        //transform.localScale = new Vector3(size, size, size);
    }



}
  