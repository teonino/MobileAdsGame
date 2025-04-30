using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DictionaryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty keys = property.FindPropertyRelative("keys");
        SerializedProperty values = property.FindPropertyRelative("values");

        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, 20), keys);
        EditorGUI.PropertyField(new Rect(position.x, position.y + 25, position.width, 20), values);

        if (keys.arraySize != values.arraySize)
        {
            EditorGUI.HelpBox(new Rect(position.x, position.y + 50, position.width, 20),
                "Keys and Values do not match in size", MessageType.Warning);
        }
    }
}
