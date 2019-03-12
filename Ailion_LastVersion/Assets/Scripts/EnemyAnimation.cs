using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour {

    private UnityEngine.AI.NavMeshAgent nav;
    //private Vector3 vel;

    private AnimatorOverrideController animTronco;
    private AnimatorOverrideController animPiernaDer;
    private AnimatorOverrideController animPiernaIzq;
    private Animator animBrazoDer;
    private Animator animBrazoIzq;

    public bool correr = false;
    public bool salto = false;
    public bool ataque = false;

    private float ultimaVez = 0;

    void Start () {

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //vel = Vector3.zero;

        animTronco = GetComponentInChildren<AnimatorOverrideController>();
        animBrazoDer = GameObject.FindWithTag("EnemyBrazoDer").GetComponent<Animator>();
        animBrazoIzq = GameObject.FindWithTag("EnemyBrazoIzq").GetComponent<Animator>();
        animPiernaIzq = GameObject.FindWithTag("EnemyPiernaIzq").GetComponent<AnimatorOverrideController>();
        animPiernaDer = GameObject.FindWithTag("EnemyPiernaDer").GetComponent<AnimatorOverrideController>();
    }
	
	void Update () {
        //vel = nav.velocity;
        //correr = (nav.velocity != Vector3.zero);
        print(nav.velocity);
	}
}
