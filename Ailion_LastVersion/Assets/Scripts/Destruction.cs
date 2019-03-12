using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour {

    public int ScoreValue = 100;

    public GameObject fractured;

    public void Destruir()
    {
        ScoreManager.score += ScoreValue;
        //new WaitForEndOfFrame();
        Instantiate(fractured, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
