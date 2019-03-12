using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaSound : MonoBehaviour {

    private AudioSource sonido;

    // Use this for initialization
    void Start()
    {
        sonido = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("mouse 0")) sonido.Play(); 
        
        if (Input.GetKeyUp("mouse 0")) sonido.Stop(); ;

    }
}
