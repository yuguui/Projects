﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour {

    
    public Transform target;

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 0.75f, 0);
        public float lookSmooth = 100f;
        public float distanceFromTarget = -8;
        public float zoomSmooth = 100;
        public float maxZoom = -1;
        public float minZoom = -15;
        public bool smoothFollow = true;
        public float smooth = 0.05f;

        [HideInInspector]
        public float newDistance = -8; //set by zoom input
        [HideInInspector]
        public float adjustmentDistance = -8;

    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20 ;
        public float yRotation = -180 ;
        public float maxRotation = 25;
        public float minRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
        public string ORBIT_HORIZONTAL = "OrbitHorizontal";
        public string ORBIT_VERTICAL = "OrbitVertical";
        public string ZOOM = "Mouse ScrollWheel";
    }

    [System.Serializable]
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
    }



    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();
    public DebugSettings debug = new DebugSettings();
    public CollisionHandler collision = new CollisionHandler();
    

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    Vector3 adjustedDestination = Vector3.zero;
    Vector3 camVel = Vector3.zero;
    CharacterController3 charController;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;


    void Start()
    {
        SetCameraTarget(target);

        vOrbitInput = hOrbitInput = zoomInput = hOrbitSnapInput = 0;

        MoveToTarget();
        collision.Initialize(Camera.main);
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

    }

    void SetCameraTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<CharacterController3>())
            {
                charController = target.GetComponent<CharacterController3>();
            }
            else
                Debug.LogError("El target necesita un Controller");
        }
        else
            Debug.LogError("La cámara necesita un target");
    }

    void GetInput()
    {
        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    } 

    void Update()
    {
        GetInput();
        ZoomInOnTarget();

    }

    void FixedUpdate()
    {
        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();
        //player orbit input
        OrbitTarget();

        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

        //draw debug lines
        for (int i = 0; i < 5; i++)
        {
            if (debug.drawDesiredCollisionLines)
            {
                Debug.DrawLine(targetPos, collision.desiredCameraClipPoints[i], Color.white);
            }
            if (debug.drawAdjustedCollisionLines)
            {
                Debug.DrawLine(targetPos, collision.adjustedCameraClipPoints[i], Color.green);
            }
        }

        collision.CheckColliding(targetPos); //using raycast here
        position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(targetPos);
    

    }

    void MoveToTarget()
    {
        targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation+ target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;
        transform.position = destination; 

        if (collision.colliding)
        {
            adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * Vector3.forward * position.adjustmentDistance;
            adjustedDestination += targetPos;

            if (position.smoothFollow)
            {
                //use sooth damp function
                transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, position.smooth);
            }
            else
                transform.position = adjustedDestination;
        }
        else
        {
            if(position.smoothFollow)
            {
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, position.smooth);
                //use sooth damp function
            }
            else
                transform.position = destination;
        }
    }

    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime); 
    }

    void OrbitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            orbit.yRotation = -180;
        }
        orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;
        if (orbit.xRotation > orbit.maxRotation)
        {
            orbit.xRotation = orbit.maxRotation;
        }
        if (orbit.xRotation < orbit.minRotation)
        {
            orbit.xRotation = orbit.minRotation;
        }
    } 

    void ZoomInOnTarget()
    {
        position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;
        if (position.distanceFromTarget > position.maxZoom)
        {
            position.distanceFromTarget = position.maxZoom;
        }
        if (position.distanceFromTarget < position.minZoom)
        {
            position.distanceFromTarget = position.minZoom;
        }
    }

    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;

//        [HideInInspector]
        public bool colliding = false;
//        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
//        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera;

        public void Initialize (Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }

        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera)
                return;
            // clear the content of intoArray
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 3.41f)*z; // TEST THIS VALUE
            float y = x / camera.aspect;

            // Find 5 new points
            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; //added and rottated the point relative to camera
            //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition; //added and rottated the point relative to camera
            //bottom left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition; //added and rottated the point relative to camera
            //bottom right
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition; //added and rottated the point relative to camera
            //camera's position
            intoArray[4] = cameraPosition - camera.transform.forward;
        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray (fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i],fromPosition);
                if (Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }
            return false;
        }

        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;

            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                    else
                    {
                        if (hit.distance < distance)
                            distance = hit.distance;
                    }

                }
            }
            if (distance == -1)
                return 0;
            else
                return distance;
        }
        public void CheckColliding(Vector3 targetPosition)
        {
            if ( CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition))
            {
                colliding = true;
            }
            else
            {
                colliding = false;
            }
        }
    }
}
