using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Level.Editor
{
    [CustomPropertyDrawer(typeof(WaypointIndexAttribute))]
    public class LevelAttributeDrawer : PropertyDrawer
    {
        private List<string> nameLevel;
        private SerializedObject _serializedLevelManager;
        private const string PropertyDataLevelName = "dataLevels";
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var wpSerializedProperty = property.serializedObject.FindProperty("_wayPoints");
            nameLevel = new List<string>();
            int length = wpSerializedProperty.arraySize;
            nameLevel.Add("Manual Position");
            for (int i = 0; i < length; i++)
            {
                nameLevel.Add($" [{i}] : Position)");
            }
            EditorGUI.LabelField(position,"Start Waypoint");
            Rect nextPos = position;
            nextPos.x += 120;
            nextPos.width -= 120;
            property.intValue = EditorGUI.Popup(nextPos,property.intValue+1, nameLevel.ToArray()) - 1;
            
        }
    }
}