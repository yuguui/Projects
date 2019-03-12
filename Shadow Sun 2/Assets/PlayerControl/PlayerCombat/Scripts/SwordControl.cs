using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour {

    Transform playerTransform;
    Transform swordTransform;
    Transform aimTransform;

    Vector3 initialUp;

    public float _RotationSpeed = 100;
    public float _SelfRotationSpeed = 5;
    public float _AttackSpeed = 5;
    public float _Test;

    bool isAttacking = false;
    bool isReached = false;

    // Use this for initialization
    void Awake () {
        playerTransform = GetComponentsInParent<Transform>()[1];
        swordTransform = GetComponent<Transform>();
        aimTransform = GameObject.FindGameObjectWithTag("Aim").GetComponent<Transform>();
        print(playerTransform);
    }
	
	// Update is called once per frame
	void Update () {
        RotateSword();
        SwordAttack();
        print("Up: " + swordTransform.up);
    }

    void RotateSword()
    {
        if (!isAttacking)
        {
            print("Rotating");
            swordTransform.RotateAround(playerTransform.position, Vector3.up, _RotationSpeed * Time.deltaTime);
            //Mathf.PerlinNoise(Time.time,0);
            swordTransform.Rotate(new Vector3(_SelfRotationSpeed * 2 * Time.deltaTime, Mathf.PerlinNoise(Time.time, 0), Mathf.PerlinNoise(0, Time.time)) * _SelfRotationSpeed);
        }
        
    }

    void SwordAttack()
    {
        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            isAttacking = true;
            initialUp = swordTransform.position;

            
        }
        if (isAttacking)
        {
            GoToPoint(aimTransform.position);
            if (isReached)
            {
                GoToPoint(playerTransform.position);
                if(_Test > (swordTransform.position-playerTransform.position).magnitude)
                {
                    isReached = false;
                    isAttacking = false;
                }
            }
        }

    }

    void GoToPoint(Vector3 point)
    {
        PointObjective(point);
        Vector3 dir = (point - swordTransform.position);
        Debug.DrawRay(swordTransform.position, dir);
        swordTransform.position += dir * _AttackSpeed * Time.deltaTime;
        //swordTransform.Translate(dir.normalized * _AttackSpeed*Time.deltaTime);
    }

    void PointObjective(Vector3 point)
    {
        //swordTransform.forward = Vector3.up;
        //swordTransform.LookAt(point, -swordTransform.up);
        Vector3 pointDirection = Vector3.Slerp(swordTransform.up, point - swordTransform.position.normalized, 0.1f).normalized;
        swordTransform.up = pointDirection;
        //swordTransform.rotation = Quaternion.Euler(Vector3.Angle(swordTransform.up, point-swordTransform.position)*Time.deltaTime,0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Aim")
        {
            isReached = true;
        }
    }

    
}
