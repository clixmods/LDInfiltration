using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleAmovible))]
public class ObstacleAmovibleEditor : Editor
{
    [MenuItem("GameObject/Gameplay Element/Obstacle Amovible", false, 1)]
    static void CreateObject(MenuCommand menuCommand)
    {
        var obstacleAmovible = FindObjectOfType<ObstacleAmovible>();
        if (obstacleAmovible != null)
        {
            Debug.LogWarning("Warning, only 1 endgameManager is allowed");
            return;
        }
            
        // Create a custom game object
        GameObject go = new GameObject("EndgameManager Trigger");
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
        var gameObject = go.AddComponent<ObstacleAmovible>();

          
    }
}
