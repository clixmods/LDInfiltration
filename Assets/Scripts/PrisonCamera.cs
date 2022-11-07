using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
[SelectionBase]
//[ExecuteAlways]
public class PrisonCamera : MonoBehaviour
{ 
    [Range(0,90)]
    [SerializeField] private float angleRotate = 20;
    [SerializeField] private float _speed = 30;
    [SerializeField] private bool goB;

    private Quaternion ogRota;
    private Quaternion AngleAToTarget;
    private Quaternion AngleBToTarget;

    private int _targetAngleA;
    private int _targetAngleB;

    private float timeCount;

    private FieldOfView _FOV;

    #region MonoBehaviour

    // Start is called before the first frame update
    void Start()
    {
        ogRota = transform.rotation ;
        AngleAToTarget = ogRota;
        AngleBToTarget = ogRota;
        AngleAToTarget.y -= angleRotate;
        AngleBToTarget.y += angleRotate;
        //_targetAngleA = (int)(angleRotate + ogRota.y);
        // _targetAngleB = (int)(angleRotate/-2 + ogRota.y);
        GenerateTargetAngle();

    }

   

    private void OnValidate()
    {
        _FOV ??= GetComponent<FieldOfView>();
        GenerateTargetAngle();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = transform.rotation; 
        if (Math.Abs(transform.rotation.eulerAngles.y - AngleAToTarget.eulerAngles.y) < 0.05f)
        {
            goB = true;
        }
        else if(Math.Abs(transform.rotation.eulerAngles.y - AngleBToTarget.eulerAngles.y ) < 0.05f)
        {
            goB = false;
        }

        rotation = Quaternion.RotateTowards(transform.rotation, goB ? AngleBToTarget : AngleAToTarget, Time.deltaTime*_speed);
        transform.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
    }

    #endregion
    
    void GenerateTargetAngle()
    {
        AngleAToTarget = ogRota;
        AngleBToTarget = ogRota;

        AngleBToTarget = Quaternion.Euler(AngleBToTarget.eulerAngles.x, AngleBToTarget.eulerAngles.y + angleRotate,
            AngleBToTarget.eulerAngles.z);
        AngleAToTarget = Quaternion.Euler(AngleAToTarget.eulerAngles.x, AngleAToTarget.eulerAngles.y - angleRotate,
            AngleAToTarget.eulerAngles.z);
    }
}
