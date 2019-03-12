using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByeBye : MonoBehaviour {

    public float minSecs = 5;
    public float maxSecs = 6;

    void Start () {
        Destroy(gameObject, Random.Range(minSecs, maxSecs));
    }
	
}
