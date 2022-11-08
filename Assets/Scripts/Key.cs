using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Key : MonoBehaviour
{
    private BoxCollider _box;
    // Start is called before the first frame update
    void Start()
    {
        _box = GetComponent<BoxCollider>();
        _box.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            EndgameManager.IsTriggerable = true;
            Destroy(gameObject);
        }
    }
}
