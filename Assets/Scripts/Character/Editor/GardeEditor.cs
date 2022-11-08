using UnityEditor;
using UnityEngine;

    [CustomEditor(typeof(Garde))]
    public class GardeEditor : UnityEditor.Editor
    {
        private bool _foldoutFOV;
        private bool _foldSO;
        private Garde myTarget;
        private UnityEditor.Editor _editorFOV;


        [MenuItem("GameObject/Gameplay Element/Garde", false, 1)]
        static void CreateGarde(MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject go = new GameObject("Garde");
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
            go.AddComponent<Garde>();
            var viewModel = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            viewModel.name = "viewModel";
            viewModel.transform.parent = go.transform;
       
        }
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();
            myTarget = (Garde) target;
            
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            myTarget ??= (Garde)target;
       
            myTarget.GetComponent<FieldOfView>().hideFlags = HideFlags.None;
            myTarget.GetComponent<MeshFilter>().hideFlags = HideFlags.HideInInspector;
            
            
        }
        private void OnSceneGUI()
        {
            myTarget ??= (Garde)target;
            // //https://docs.unity3d.com/ScriptReference/EditorGUI.ChangeCheckScope.html
            if (!Application.isPlaying)
            {
                if (myTarget.Waypoints != null && myTarget.Waypoints.Length > 0)
                {
                    if(serializedObject.FindProperty("_startWaypointIndex").intValue != -1)
                        myTarget.transform.position = myTarget.Waypoints[serializedObject.FindProperty("_startWaypointIndex").intValue];
                }
               
            }
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var waypoints = serializedObject.FindProperty("_wayPoints");
                if (waypoints.arraySize == 0) return;
                Vector3[] newPosition = new Vector3[myTarget.Waypoints.Length];
                for (int i = 0; i < myTarget.Waypoints.Length; i++)
                {
                    newPosition[i] = Handles.PositionHandle(myTarget.Waypoints[i], Quaternion.identity);
                }
                if (check.changed)
                {
                    for (int i = 0; i < myTarget.Waypoints.Length; i++)
                    {
                        myTarget.Waypoints[i] = newPosition[i];
                    }
                }
            }
            
            
        
        }
    }

