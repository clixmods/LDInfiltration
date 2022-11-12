using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KeyEditor : Editor
{
    [MenuItem("GameObject/Gameplay Element/Key", false, 1)]
    static void CreateObject(MenuCommand menuCommand)
    {
        var myKey = FindObjectOfType<Key>();
        if (myKey != null)
        {
            Debug.LogWarning("Warning, only 1 Key is allowed");
            return;
        }
            
        // Create a custom game object
        GameObject go = new GameObject("Key Endgame");
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
        var key = go.AddComponent<Key>();

          
    }
}
