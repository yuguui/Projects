using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombIntensity : MonoBehaviour {

    public float vel;
    private Light luz;

	// Use this for initialization
	void Start () {
        luz = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (luz.intensity > 0) luz.intensity -= vel;
    }
}
