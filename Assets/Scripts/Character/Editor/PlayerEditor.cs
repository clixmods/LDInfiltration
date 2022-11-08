using UnityEditor;
using UnityEngine;


    [CustomEditor(typeof(Player))]
    public class PlayerEditor : UnityEditor.Editor
    {
        
        [MenuItem("GameObject/Gameplay Element/Player", false, 1)]
        static void CreatePlayer(MenuCommand menuCommand)
        {
            var Player = FindObjectOfType<Player>();
            if (Player != null)
            {
                Debug.LogWarning("Warning, only 1 player is allowed");
                return;
            }
            
            // Create a custom game object
            GameObject go = new GameObject("Player");
            go.layer = LayerMask.NameToLayer("Player");
            go.tag = "Player";
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
            var player = go.AddComponent<Player>();

            var so = new SerializedObject(player);
            so.FindProperty("_speed").floatValue = 10;

            so.ApplyModifiedProperties();
            
            var viewModel = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            viewModel.name = "viewModel";
            viewModel.transform.parent = go.transform;
            if(viewModel.TryGetComponent<CapsuleCollider>(out var collider))
                DestroyImmediate(collider);
       
        }
    }
