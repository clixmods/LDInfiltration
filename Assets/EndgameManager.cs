using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class EndgameManager : MonoBehaviour
{
    private BoxCollider _collider;
    
   

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _collider ??= gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent<Player>(out var player))
        {
            Debug.Log("Win");
            Application.Quit();
            
        }
    }
}
