using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class EnemyData2
{
    public float Health;
    public float UnitsGiven;
    public Sprite sprite;


    public EnemyData2(float health, float unitsGiven)
    {
        Health = health;
        UnitsGiven = unitsGiven;
    }
}

public class EnemyEditor2 : EditorWindow
{
    private static List<GameObject> prefabs = new List<GameObject>();

    private static Object[] prefabsToLoad;
    private static Dictionary<string, EnemyData2> enemyDatas = new Dictionary<string, EnemyData2>();

    private GameObject currentPrefab;
    private static Sprite currentSprite;
    private static Sprite draggedSprite;


    private bool buttonPressed;

    private Vector2 scrollPos;

    private static Rect windowRect;

    [MenuItem("Tools/Enemy Editor 2")]    
    private static void Init()
    {
        var window = (EnemyEditor2)GetWindow(typeof(EnemyEditor2));
        windowRect = window.position;
        window.minSize = new Vector2(600,500);
    }

    private void OnEnable()
    {
        prefabsToLoad = Resources.LoadAll("Enemies", typeof(GameObject));
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(windowRect.width/3), GUILayout.Height(100));

        //Enemy Buttons.

        for (int i = 0; i<prefabsToLoad.Length; i++)
        {
            GameObject prefab = (GameObject)prefabsToLoad[i];
            BaseEnemy enemy = prefab.GetComponent<BaseEnemy>();
            if (!enemyDatas.ContainsKey(prefab.name))
            {
                enemyDatas.Add(prefab.name, new EnemyData2(enemy.MaxHealth, enemy.UnitsGiven));
            }
            EnemyButton(prefab);          
        }

        EditorGUILayout.EndScrollView();

        //Show enemy selected name
        if(currentPrefab != null)
        {
            EditorGUILayout.LabelField("Current sprite for: " + currentPrefab.name + "         Replacement Sprite", EditorStyles.boldLabel);
        }

        //Draw Sprite of enemy selected.
        if (currentSprite != null)
        {
            GUI.DrawTexture(new Rect(10, 125, 150, 150), ConvertToTexture(currentSprite), ScaleMode.ScaleToFit);
        }

        //Draw dragged in sprite
        if (draggedSprite != null)
        {
            GUI.DrawTexture(new Rect(215, 125, 150, 150), ConvertToTexture(draggedSprite), ScaleMode.ScaleToFit);
        }

        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");
        EditorGUILayout.LabelField("\n");

        //Draw fields if enemy selected.
        if (currentPrefab != null)
        {
            enemyDatas[currentPrefab.name].Health = EditorGUILayout.FloatField("Health", enemyDatas[currentPrefab.name].Health);
            enemyDatas[currentPrefab.name].UnitsGiven = EditorGUILayout.FloatField("Units Given", enemyDatas[currentPrefab.name].UnitsGiven);

            var fieldNames = typeof(BaseEnemy).GetFields().Where(x => x.FieldType.Equals(typeof(float))).Select(field => field.Name).ToList();

            //Poging tot ophalen variables met reflectie.
            /*
            for (int i = 0; i < fieldNames.Count; i++)
            {
                enemyDatas[currentPrefab.name].GetType()
                    .GetField(fieldNames[i])
                    .SetValue(enemyDatas[currentPrefab.name], 
                    EditorGUILayout.FloatField(fieldNames[i], ));
            }
            */
        }

        SaveButton();
        ResetButton();

        InfoBoxes();

        DragDropGUI();
    }

    private void EnemyButton(GameObject prefab)
    {
        if (GUILayout.Button(prefab.name))
        {
            buttonPressed = true;
            currentSprite = prefab.GetComponent<SpriteRenderer>().sprite;
            currentPrefab = prefab;
        }
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
                if(enemyDatas[prefab.name].sprite != null)
                {
                    currentPrefab.GetComponent<SpriteRenderer>().sprite = enemyDatas[currentPrefab.name].sprite;
                }
            }
            draggedSprite = null;
            currentSprite = currentPrefab.GetComponent<SpriteRenderer>().sprite;
            GUI.FocusControl(null);
        }
    }

    private void ResetButton()
    {
        if (GUILayout.Button("Reset"))
        {
            draggedSprite = null;
            foreach (GameObject prefab in prefabsToLoad)
            {
                BaseEnemy enemy = prefab.GetComponent<BaseEnemy>();
                enemyDatas[prefab.name].Health = EditorGUILayout.FloatField("Health", enemy.MaxHealth);
                enemyDatas[prefab.name].UnitsGiven = EditorGUILayout.FloatField("Units Given", enemy.UnitsGiven);
            }
            GUI.FocusControl(null);
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

    private void DragDropGUI()
    {
        var e = Event.current.type;

        if(e == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
        }
        else if(e == EventType.DragPerform)
        {
            DragAndDrop.AcceptDrag();
            Object draggedObject = DragAndDrop.objectReferences[0];
            if(draggedObject is Texture2D)
            {
                Sprite sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetOrScenePath(draggedObject), typeof(Sprite));
                enemyDatas[currentPrefab.name].sprite = sprite;
                draggedSprite = sprite;
            }
        }
    }

    private void InfoBoxes()
    {
        EditorGUILayout.HelpBox("Drag a sprite into the window to replace the current sprite", MessageType.Info);
        float currentHealth = currentPrefab.GetComponent<BaseEnemy>().MaxHealth;
        float currentUnits = currentPrefab.GetComponent<BaseEnemy>().UnitsGiven;

        if (draggedSprite != null || enemyDatas[currentPrefab.name].Health != currentHealth || enemyDatas[currentPrefab.name].UnitsGiven != currentUnits)
        {
            EditorGUILayout.HelpBox("Press Save to save the changed properties, and press reset to return them to the prefabs values.", MessageType.Info);
        }
    }
}
