using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveEditor : EditorWindow
{
    public static Dictionary<string, float> Stats = new Dictionary<string, float>();
    public static Dictionary<string, bool> Unlockables = new Dictionary<string, bool>();


    [MenuItem("Tools/Save Editor")]
    private static void Init()
    {
        var window = (SaveEditor)GetWindow(typeof(SaveEditor));
        window.minSize = new Vector2(400, 300);

        InitializeDictionaries();
    }

    private static void InitializeDictionaries()
    {
        if (!Stats.ContainsKey("Damage")) { Stats.Add("Damage", 1); }
        if (!Stats.ContainsKey("FireRate")) { Stats.Add("FireRate", 1); }
        if (!Stats.ContainsKey("MaxHealth")) { Stats.Add("MaxHealth", 3); }
        if (!Stats.ContainsKey("ProjectileCount")) { Stats.Add("ProjectileCount", 1); }
        if (!Stats.ContainsKey("Speed")) { Stats.Add("Speed", 3); }

        if (!Unlockables.ContainsKey("BoostUnlocked")) { Unlockables.Add("BoostUnlocked", false); }
        if (!Unlockables.ContainsKey("MultipleProjectiles")) { Unlockables.Add("MultipleProjectiles", false); }
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Delete Save"))
        {
            EditorGUILayout.LabelField("Save Editor", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("\n");
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

        DrawFields();

        if (GUILayout.Button("Create Save"))
        {
            SavePlayerInEditor();
        }
    }

    private void DrawFields()
    {
        Stats["Damage"] = EditorGUILayout.FloatField("Damage", Stats["Damage"]);
        Stats["FireRate"] = EditorGUILayout.FloatField("FireRate", Stats["FireRate"]);
        Stats["MaxHealth"] = EditorGUILayout.FloatField("MaxHealth", Stats["MaxHealth"]);
        Stats["ProjectileCount"] = EditorGUILayout.FloatField("ProjectileCount", Stats["ProjectileCount"]);
        Stats["Speed"] = EditorGUILayout.FloatField("Speed", Stats["Speed"]);

        Unlockables["BoostUnlocked"] = EditorGUILayout.Toggle("BoostUnlocked", Unlockables["BoostUnlocked"]);
        Unlockables["MultipleProjectiles"] = EditorGUILayout.Toggle("MultipleProjectiles", Unlockables["MultipleProjectiles"]);
    }

    public void SavePlayerInEditor()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player.Sav";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(null, Stats, Unlockables);

        formatter.Serialize(stream, data);
        stream.Close();
    }
}
