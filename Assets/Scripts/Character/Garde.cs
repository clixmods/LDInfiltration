using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public enum PatternBehavior
    {
        /// <summary>
        /// Simple :  une fois qu’il a atteint le dernier waypoint, le prochain sera le premier de la liste
        /// </summary>
        Simple,
        /// <summary>
        /// AllerRetour : Une fois qu’il a atteint le dernier waypoint, il refait le chemin à l’inverse 
        /// </summary>
        AllerRetour
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(FieldOfView))]
    public class Garde : Character
    {
        [SerializeField] private PatternBehavior _patternBehavior;
        [SerializeField] private Vector3[] _wayPoints;

        public Vector3[] Waypoints => _wayPoints;
    
        [SerializeField, WaypointIndex] private int _startWaypointIndex = -1;
        private int _currentIndexWaypoint;
        private FieldOfView _fieldOfView;
 
    


        public override void Start()
        {
            base.Start();
            transform.position = _wayPoints[_startWaypointIndex];
            _currentIndexWaypoint = _startWaypointIndex;
            _collider.enabled = false;
        
        }

        private void OnValidate()
        {
            _fieldOfView ??= GetComponent<FieldOfView>();
            if (!Application.isPlaying)
            {
                if (_startWaypointIndex != -1)
                {
                    transform.position = _wayPoints[_startWaypointIndex];
                    if (_wayPoints.Length > _startWaypointIndex + 1)
                    {
                        transform.LookAt(_wayPoints[_startWaypointIndex+1]);
                    }
                    else if (_startWaypointIndex - 1 >= 0)
                    {
                        transform.LookAt(_wayPoints[_startWaypointIndex-1]);
                    }
                }
                    
            }
        
        }

        float GetDistancePlane(Vector3 vectorA, Vector3 vectorB)
        {
            Vector2 a = new Vector2(vectorA.x, vectorA.z);
            Vector2 b = new Vector2(vectorB.x, vectorB.z);
            return Vector2.Distance(a, b);
        }
        // Update is called once per frame
        void Update()
        {
            Vector3 direction = _wayPoints[_currentIndexWaypoint] - transform.position;
        
            transform.LookAt(new Vector3(_wayPoints[_currentIndexWaypoint].x,transform.position.y ,_wayPoints[_currentIndexWaypoint].z));
        
            _rb.velocity = direction.normalized * _speed;
            if (GetDistancePlane(transform.position,_wayPoints[_currentIndexWaypoint]) < 0.5f)
            {
                if (_currentIndexWaypoint == _wayPoints.Length-1)
                {
                    _currentIndexWaypoint = -1;
                }
                _currentIndexWaypoint++;
            }
        }

   
    }
