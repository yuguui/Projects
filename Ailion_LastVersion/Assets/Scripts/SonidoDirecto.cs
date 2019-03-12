using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoDirecto : MonoBehaviour {

    private AudioSource sonido;

    // Use this for initialization
    void Awake () {
        sonido = GetComponent<AudioSource>();
        sonido.Play();
    }
}
