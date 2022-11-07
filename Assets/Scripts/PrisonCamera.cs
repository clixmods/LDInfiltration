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
    private FieldOfView _previewFOV;
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
        if(_previewFOV != null)
            Destroy(_previewFOV.transform);
        
    }

    private void OnValidate()
    {
        _FOV ??= GetComponent<FieldOfView>();
        AngleAToTarget = ogRota;
        AngleBToTarget = ogRota;

        AngleBToTarget = Quaternion.Euler(AngleBToTarget.eulerAngles.x, AngleBToTarget.eulerAngles.y + angleRotate,
            AngleBToTarget.eulerAngles.z);
        AngleAToTarget = Quaternion.Euler(AngleAToTarget.eulerAngles.x, AngleAToTarget.eulerAngles.y - angleRotate,
            AngleAToTarget.eulerAngles.z);

        _previewFOV = GetComponentInChildren<FieldOfView>();
        if (_previewFOV != _FOV)
        {
            
            _previewFOV.transform.localPosition = Vector3.zero;
            _previewFOV.transform.localRotation = Quaternion.identity;
            _previewFOV.Angle = angleRotate;
            _previewFOV.Radius = GetComponent<FieldOfView>().Radius;
            
        }
        _previewFOV ??= Instantiate(new GameObject(), Vector3.zero, quaternion.identity, transform).AddComponent<FieldOfView>();
        
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
        Debug.DrawLine(transform.position, transform.position + AngleBToTarget.eulerAngles , Color.green);
        Debug.DrawRay(transform.position,AngleBToTarget.eulerAngles.normalized,Color.red);
    }
}
