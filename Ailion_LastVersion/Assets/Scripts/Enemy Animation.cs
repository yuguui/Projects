using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

    private UnityEngine.AI.NavMeshAgent nav;

    private Animator animTronco;
    private Animator animPiernaDer;
    private Animator animPiernaIzq;
    private Animator animBrazoDer;
    private Animator animBrazoIzq;
    private Animator animTronco1;
    private Animator animPiernaDer1;
    private Animator animPiernaIzq1;
    private Animator animBrazoDer1;
    private Animator animBrazoIzq1;
    private Animator animTronco2;
    private Animator animPiernaDer2;
    private Animator animPiernaIzq2;
    private Animator animBrazoDer2;
    private Animator animBrazoIzq2;

    public bool correr = false;

    void Start () {

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        animTronco = GameObject.FindWithTag("EnemyTorso").GetComponent<Animator>();
        animBrazoDer = GameObject.FindWithTag("EnemyBrazoDer").GetComponent<Animator>();
        animBrazoIzq = GameObject.FindWithTag("EnemyBrazoIzq").GetComponent<Animator>();
        animPiernaIzq = GameObject.FindWithTag("EnemyPiernaIzq").GetComponent<Animator>();
        animPiernaDer = GameObject.FindWithTag("EnemyPiernaDer").GetComponent<Animator>();
        animTronco1 = GameObject.FindWithTag("EnemyTorso1").GetComponent<Animator>();
        animBrazoDer1 = GameObject.FindWithTag("EnemyBrazoDer1").GetComponent<Animator>();
        animBrazoIzq1 = GameObject.FindWithTag("EnemyBrazoIzq1").GetComponent<Animator>();
        animPiernaIzq1 = GameObject.FindWithTag("EnemyPiernaIzq1").GetComponent<Animator>();
        animPiernaDer1 = GameObject.FindWithTag("EnemyPiernaDer1").GetComponent<Animator>();
        animTronco2 = GameObject.FindWithTag("EnemyTorso2").GetComponent<Animator>();
        animBrazoDer2 = GameObject.FindWithTag("EnemyBrazoDer2").GetComponent<Animator>();
        animBrazoIzq2 = GameObject.FindWithTag("EnemyBrazoIzq2").GetComponent<Animator>();
        animPiernaIzq2 = GameObject.FindWithTag("EnemyPiernaIzq2").GetComponent<Animator>();
        animPiernaDer2 = GameObject.FindWithTag("EnemyPiernaDer2").GetComponent<Animator>();
    }
	
	void Update () {
        correr = (nav.velocity != Vector3.zero);

        animTronco.SetBool("correr", correr);
        animPiernaIzq.SetBool("correr", correr);
        animPiernaDer.SetBool("correr", correr);
        animTronco1.SetBool("correr", correr);
        animPiernaIzq1.SetBool("correr", correr);
        animPiernaDer1.SetBool("correr", correr);
        animTronco2.SetBool("correr", correr);
        animPiernaIzq2.SetBool("correr", correr);
        animPiernaDer2.SetBool("correr", correr);
    }

    public void Atacar()
    {
        animBrazoDer.SetTrigger("ataque");
        animBrazoIzq.SetTrigger("ataque");
        animBrazoDer1.SetTrigger("ataque");
        animBrazoIzq1.SetTrigger("ataque");
        animBrazoDer2.SetTrigger("ataque");
        animBrazoIzq2.SetTrigger("ataque");
    }


}
