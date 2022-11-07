using System;
using UnityEditor;
using UnityEngine;
    
    [CustomEditor(typeof(Garde))]
    public class GardeEditor : Editor
    {
        private bool _foldoutFOV;
        private bool _foldSO;
        private Garde myTarget;
        private Editor _editorFOV;
     
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            return;
            myTarget = (Garde) target;
            var so = new SerializedObject(target);
            _foldoutFOV = EditorGUILayout.BeginFoldoutHeaderGroup(_foldoutFOV, "Field Of View");
            if (_foldoutFOV)
            {
                // Draw ScriptableObject in the inspector
                var _serializedProperty = serializedObject.FindProperty("_fieldOfView");
                
                //myTarget.gameObject.name = $"{_serializedProperty.name}";
                var sooof = new SerializedObject(_serializedProperty.objectReferenceValue);
                _foldSO = EditorGUILayout.InspectorTitlebar(_foldSO, sooof.targetObject);
                if (_foldSO)
                {
                    CreateCachedEditor(_serializedProperty.objectReferenceValue, null, ref _editorFOV);
                    EditorGUI.indentLevel++;
                    _editorFOV.OnInspectorGUI();
                }
            }
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
                if(serializedObject.FindProperty("_startWaypointIndex").intValue != -1)
                    myTarget.transform.position = myTarget.Waypoints[serializedObject.FindProperty("_startWaypointIndex").intValue].position;
            }
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var waypoints = serializedObject.FindProperty("_wayPoints");
                Vector3[] newPosition = new Vector3[myTarget.Waypoints.Length];
                for (int i = 0; i < myTarget.Waypoints.Length; i++)
                {
                    newPosition[i] = Handles.PositionHandle(myTarget.Waypoints[i].position, Quaternion.identity);
                }
                //Vector3 posCam = Handles.PositionHandle(cameraTransform.position, Quaternion.identity);
                //Vector3 posComponentBox = Handles.PositionHandle(boxColliderTransform.position , Quaternion.identity);
                if (check.changed)
                {
                    for (int i = 0; i < myTarget.Waypoints.Length; i++)
                    {
                        myTarget.Waypoints[i].position = newPosition[i];
                    }
                }
            }
            
            
            // Transform boxColliderTransform = _boxCollider.transform;
            // Transform cameraTransform = _camera.transform;
            // using (var check = new EditorGUI.ChangeCheckScope())
            // {
            //     Vector3 posCam = Handles.PositionHandle(cameraTransform.position, Quaternion.identity);
            //     Vector3 posComponentBox = Handles.PositionHandle(boxColliderTransform.position , Quaternion.identity);
            //     if (check.changed)
            //     {
            //         boxColliderTransform.position = posComponentBox;
            //         cameraTransform.position = posCam;
            //     }
            // }
            // Matrix4x4 matrix = Matrix4x4.TRS(boxColliderTransform.position,boxColliderTransform.rotation, boxColliderTransform.lossyScale);
            // using (new Handles.DrawingScope(Color.green,matrix))
            // {
            //     _boxCollider.center = _box.center;
            //     _boxCollider.size = _box.size;
            //     _box.DrawHandle();
            // }
        }
    }
