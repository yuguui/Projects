using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private GameObject Player;
    private Random Random;
    private int oleadaEnemigos = 1;
    public GameObject enemigo;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 1, 30);
        Player = GameObject.Find("A-IlionManager");

	}
	
	// Update is called once per frame
	void Spawn () {
        {
            for (int i = 0; i < oleadaEnemigos * 3; i++)
            {
                float x = (Random.value * 100) + Player.transform.position.x;
                float z = (Random.value * 100) + Player.transform.position.z;
                Instantiate(enemigo,new Vector3(x,0,z),Quaternion.Euler(0,0,0));
            }




            oleadaEnemigos++;
            return;
        }
    }
}
