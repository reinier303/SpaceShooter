using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class SaveEditor : EditorWindow
{
    [MenuItem("Tools/Save Editor")]
    private static void Init()
    {
        var window = (SaveEditor)GetWindow(typeof(SaveEditor));
        window.minSize = new Vector2(400, 300);
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Delete Save"))
        {
            string path = Application.persistentDataPath + "/Player.Sav";
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("File at: " + path + " deleted.");
            }
            else 
            {
                Debug.Log("No file found at: " + path);                
            }
        }
    }
}
