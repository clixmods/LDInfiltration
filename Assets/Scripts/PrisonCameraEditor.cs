using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PrisonCamera))]
public class PrisonCameraEditor : Editor
{
    [SerializeField] private Material mtlPreviewFOV;
    
    private GameObject _previewFOV;
    private SerializedObject _serializedObjectPreviewFOV;
    private PrisonCamera _self;
    private void OnEnable()
    {
        _self ??= (PrisonCamera)target;
        CreatePreviewFov();
    }

   

    private void OnDisable()
    {
        DestroyPreviewFov();
    }

    private void OnValidate()
    {
        CreatePreviewFov();
        UpdatePreview();

        //_serializedObjectPreviewFOV.Update();
    }

    private void OnDestroy()
    {
        DestroyPreviewFov();
    }

    private void UpdatePreview()
    {
       // _previewFOV.transform.localPosition = Vector3.zero;
       // _previewFOV.transform.localRotation = Quaternion.identity;
       
       //_previewFOV.transform.position = _self.transform.position;
      // _previewFOV.transform.rotation = _self.transform.rotation;
        var component = _previewFOV.GetComponent<FieldOfView>();
        component.Angle = serializedObject.FindProperty("angleRotate").floatValue*3.14f + (_self.GetComponent<FieldOfView>().Angle - serializedObject.FindProperty("angleRotate").floatValue) ;
            
            
        component.Radius = _self.GetComponent<FieldOfView>().Radius;
    }
    private void CreatePreviewFov()
    {
        
        if (_previewFOV == null)
        {
            _previewFOV = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, null);
            _previewFOV.transform.position = _self.transform.position;
            _previewFOV.transform.rotation = _self.transform.rotation;


            _previewFOV.layer = LayerMask.GetMask("TransparentFX");
            
            //_previewFOV.hideFlags = HideFlags.HideInHierarchy;
            var oof = _previewFOV.AddComponent<FieldOfView>();
            
            // Bounds carBounds = oof.viewMesh.bounds;
            // Vector3 whereYouWantMe = _self.transform.position;
            // Vector3 offset = _previewFOV.transform.position - _previewFOV.transform.TransformPoint(carBounds.center);
            // _previewFOV.transform.position = whereYouWantMe + offset;
            
            oof.SetMaterial(mtlPreviewFOV);
        }
    }

    private void DestroyPreviewFov()
    {
        if (_previewFOV != null)
        {
            DestroyImmediate(_previewFOV);
        }
    }

    private void OnSceneGUI()
    {
        UpdatePreview();
    }
}
