using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class EndgameManager : MonoBehaviour
{
    private BoxCollider _collider;
    public static bool IsTriggerable { get; set; }
    [SerializeField,Scene] private int nextScene;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _collider ??= gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;
        IsTriggerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsTriggerable) return;
        if (other.TryGetComponent<Player>(out var player))
        {
            Debug.Log("Win");
            SceneManager.LoadScene(nextScene);

        }
    }
}
