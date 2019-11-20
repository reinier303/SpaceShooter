using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyData
{
    public float Health;
    public float UnitsGiven;

    public EnemyData(float health, float unitsGiven)
    {
        Health = health;
        UnitsGiven = unitsGiven;
    }
}

public class EnemyEditor : EditorWindow
{
    private static List<GameObject> prefabs = new List<GameObject>();

    private static Object[] prefabsToLoad;
    private static Dictionary<string, EnemyData> enemyDatas = new Dictionary<string, EnemyData>();

    private Vector2 scrollPos;

    [MenuItem("Tools/Enemy Editor")]    
    private static void Init()
    {
        var window = (EnemyEditor)GetWindow(typeof(EnemyEditor));
        window.minSize = new Vector2(300,600);
    }

    private void OnEnable()
    {
        prefabsToLoad = Resources.LoadAll("Enemies", typeof(GameObject));
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(250), GUILayout.Height(650));

        for (int i = 0; i<prefabsToLoad.Length; i++)
        {
            GameObject prefab = (GameObject)prefabsToLoad[i];
            BaseEnemy enemy = prefab.GetComponent<BaseEnemy>();
            if (!enemyDatas.ContainsKey(prefab.name))
            {
                enemyDatas.Add(prefab.name, new EnemyData(enemy.MaxHealth, enemy.UnitsGiven));
            }
            EditorGUILayout.LabelField(prefab.name, EditorStyles.boldLabel);
            if(prefab.GetComponent<SpriteRenderer>() != null)
            {
                GUI.DrawTexture(new Rect(80, 5 + (126 * i), 64, 64), ConvertToTexture(prefab.GetComponent<SpriteRenderer>().sprite), ScaleMode.ScaleToFit);
            }

            EditorGUILayout.LabelField("\n");
            EditorGUILayout.LabelField("\n");
            EditorGUILayout.LabelField("\n");

            enemyDatas[prefab.name].Health = EditorGUILayout.FloatField("Health", enemyDatas[prefab.name].Health);
            enemyDatas[prefab.name].UnitsGiven = EditorGUILayout.FloatField("Units Given", enemyDatas[prefab.name].UnitsGiven);
            EditorGUILayout.LabelField("\n");

        }

        SaveButton();
        ResetButton();
        EditorGUILayout.EndScrollView();

    }

    private void SaveButton()
    {
        if (GUILayout.Button("Save"))
        {
            foreach (GameObject prefab in prefabsToLoad)
            {
                BaseEnemy enemy = prefab.GetComponent<BaseEnemy>();
                enemy.MaxHealth = enemyDatas[prefab.name].Health;
                enemy.UnitsGiven = enemyDatas[prefab.name].UnitsGiven;
            }
        }
    }

    private void ResetButton()
    {
        if (GUILayout.Button("Reset"))
        {
            foreach (GameObject prefab in prefabsToLoad)
            {
                BaseEnemy enemy = prefab.GetComponent<BaseEnemy>();
                enemyDatas[prefab.name].Health = EditorGUILayout.FloatField("Health", enemy.MaxHealth);
                enemyDatas[prefab.name].UnitsGiven = EditorGUILayout.FloatField("Units Given", enemy.UnitsGiven);
            }
        }
    }

    private Texture2D ConvertToTexture(Sprite sprite)
    {
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.rect.x,
                                                (int)sprite.rect.y,
                                                (int)sprite.rect.width,
                                                (int)sprite.rect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();
        return croppedTexture;
    }
}
