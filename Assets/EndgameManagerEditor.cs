using System;
using UnityEngine;
using UnityEditor;
namespace DefaultNamespace
{
    
    public class EndgameManagerEditor : Editor
    {
        [MenuItem("GameObject/Gameplay Element/Trigger Endgame", false, 1)]
        static void CreateObject(MenuCommand menuCommand)
        {
            var endgameManager = FindObjectOfType<EndgameManager>();
            if (endgameManager != null)
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
            var player = go.AddComponent<EndgameManager>();

          
        }

        private void OnEnable()
        {
            var myObject = (EndgameManager)target;
            myObject.name = "Endgame Trigger";
        }
    }
}