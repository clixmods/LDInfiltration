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
        if (Application.isPlaying) return;
        _self ??= (PrisonCamera)target;
        
        CreatePreviewFov();
    }
   
    private void OnDisable()
    {
        DestroyPreviewFov();
    }

    private void OnValidate()
    {
        if (Application.isPlaying) return;
        CreatePreviewFov();
        UpdatePreview();
    }

    private void OnDestroy()
    {
        DestroyPreviewFov();
    }

    private void UpdatePreview()
    {
        var component = _previewFOV.GetComponent<FieldOfView>();
        float angleOfFOV = _self.GetComponent<FieldOfView>().Angle; 
        float angleRotate = serializedObject.FindProperty("angleRotate").floatValue;
        component.Angle = angleRotate * Mathf.PI + ( angleOfFOV - angleRotate) ;
        component.Radius = _self.GetComponent<FieldOfView>().Radius;
    }
    private void CreatePreviewFov()
    {
        if (_previewFOV != null) return;
        _previewFOV = new GameObject();
        _previewFOV.name = "Preview Field Of View";
        _previewFOV.transform.position = _self.transform.position;
        _previewFOV.transform.rotation = _self.transform.rotation;
        _previewFOV.layer = LayerMask.GetMask("TransparentFX");
        //_previewFOV.hideFlags = HideFlags.HideInHierarchy;
        var oof = _previewFOV.AddComponent<FieldOfView>();
        oof.SetMaterial(mtlPreviewFOV);
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
        if (Application.isPlaying) return;
        UpdatePreview();
    }
}
