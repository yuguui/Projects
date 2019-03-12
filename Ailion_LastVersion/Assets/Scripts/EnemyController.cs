using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject bala;
    private GameObject player;
    private int TimeBetweenShoots;
    private Transform tPlayer;
    public LayerMask shootingMask;
    private int TimeWaitingShoot;

    private UnityEngine.AI.NavMeshAgent nav;
    private EnemyAnimation scriptAnim;



    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("A-IlionManager");
        tPlayer = player.transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.Warp(GetComponent<Transform>().position);
        scriptAnim = GetComponent<EnemyAnimation>();
        TimeWaitingShoot = UnityEngine.Random.Range(120, 240);
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.SetDestination(tPlayer.position))
        {
            this.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));

            if (nav.stoppingDistance > 5 && nav.remainingDistance <= nav.stoppingDistance)

                disparar();
        };
    }

    private void disparar()
    {
        //Llama al método del script de animación para atacar
        Ray shootRay = new Ray();
        RaycastHit shootHit; 
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        TimeBetweenShoots++;
        
        if (Physics.Raycast(shootRay, out shootHit, Mathf.Infinity, shootingMask) && TimeBetweenShoots > 120)
        {
            TimeBetweenShoots = 0;
            scriptAnim.Atacar();
            Rigidbody disparo = Instantiate(bala, new Vector3 (transform.position.x,transform.position.y+2,transform.position.z),transform.rotation).GetComponent<Rigidbody>();
            disparo.velocity = 40 * disparo.transform.forward + 3*(disparo.transform.up) ;
            TimeWaitingShoot = UnityEngine.Random.Range(120, 240);  
        }

    }
}
