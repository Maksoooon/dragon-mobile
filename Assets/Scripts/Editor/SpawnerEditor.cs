using UnityEngine;
using UnityEditor;

public class SpawnerEditor : EditorWindow
{

    private GameObject playerHolder;

    private int treePrefabAmount = 0;
    private GameObject[] treePrefab;
    private int numberOfTrees = 1;

    private float minSpawnRadiusTrees;
    private float maxSpawnRadiusTrees;


    private int rockPrefabAmount = 0;
    private GameObject[] rockPrefab;
    private int numberOfRocks = 1;


    private float minSpawnRadiusRocks;
    private float maxSpawnRadiusRocks;


    private GameObject parentObject;

    [MenuItem("Tools/Spawner")]
    public static void ShowWindow()
    {
        GetWindow<SpawnerEditor>("Spawner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawner", EditorStyles.boldLabel);

        playerHolder = (GameObject)EditorGUILayout.ObjectField("Player Holder", playerHolder, typeof(GameObject), true);


        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();


        treePrefabAmount = EditorGUILayout.IntField("Rock collection size", treePrefabAmount);

        if (treePrefab != null && treePrefabAmount != treePrefab.Length)
            treePrefab = new GameObject[treePrefabAmount];
        for (int i = 0; i < treePrefabAmount; i++)
        {
            treePrefab[i] = EditorGUILayout.ObjectField("Rock " + i.ToString(), treePrefab[i], typeof(GameObject), false) as GameObject;
        }

        numberOfTrees = EditorGUILayout.IntField("Number of Trees", numberOfTrees);

        minSpawnRadiusTrees = EditorGUILayout.FloatField("Min Spawn Radius Tree", minSpawnRadiusTrees);
        maxSpawnRadiusTrees = EditorGUILayout.FloatField("Max Spawn Radius Tree", maxSpawnRadiusTrees);


        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();


        rockPrefabAmount = EditorGUILayout.IntField("Rock collection size", rockPrefabAmount);

        if (rockPrefab != null && rockPrefabAmount != rockPrefab.Length)
            rockPrefab = new GameObject[rockPrefabAmount];
        for (int i = 0; i < rockPrefabAmount; i++)
        {
            rockPrefab[i] = EditorGUILayout.ObjectField("Rock " + i.ToString(), rockPrefab[i], typeof(GameObject), false) as GameObject;
        }



        numberOfRocks = EditorGUILayout.IntField("Number of Rocks", numberOfRocks);


        minSpawnRadiusRocks = EditorGUILayout.FloatField("Min Spawn Radius Rock", minSpawnRadiusRocks);
        maxSpawnRadiusRocks = EditorGUILayout.FloatField("Max Spawn Radius Rock", maxSpawnRadiusRocks);


        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

        if (GUILayout.Button("Spawn"))
        {
            SpawnTrees();
        }

        if (GUILayout.Button("Regenerate"))
        {
            RegenerateTrees();
        }

        if (GUILayout.Button("Remove"))
        {
            RemoveTrees();
        }

    }

    private void SpawnTrees()
    {

        if (parentObject == null)
        {
            parentObject = new GameObject("SpawnedTrees");
            Undo.RegisterCreatedObjectUndo(parentObject, "Create SpawnedTrees Parent");
        }

        for (int i = 0; i < numberOfTrees; i++)
        {
            bool randomBoolX = Random.value > 0.5f;
            bool randomBoolZ = Random.value > 0.5f;
            Vector3 randomPosition = new Vector3(
                Random.Range(minSpawnRadiusTrees, maxSpawnRadiusTrees) * (randomBoolX ? 1 : -1) + playerHolder.transform.position.x,
                playerHolder.transform.position.y,
                Random.Range(minSpawnRadiusTrees, maxSpawnRadiusTrees) * (randomBoolZ ? 1 : -1) + playerHolder.transform.position.z
            );

            GameObject tree = (GameObject)PrefabUtility.InstantiatePrefab(treePrefab[Random.Range(0, treePrefab.Length)]);
            tree.transform.position = randomPosition;
            tree.transform.SetParent(parentObject.transform);
            Undo.RegisterCreatedObjectUndo(tree, "Spawn Tree");
        }


        for (int i = 0; i < numberOfRocks; i++)
        {
            bool randomBoolX = Random.value > 0.5f;
            bool randomBoolZ = Random.value > 0.5f;
            Vector3 randomPosition = new Vector3(
                Random.Range(minSpawnRadiusRocks, maxSpawnRadiusRocks) * (randomBoolX ? 1 : -1) + playerHolder.transform.position.x,
                playerHolder.transform.position.y,
                Random.Range(minSpawnRadiusRocks, maxSpawnRadiusRocks) * (randomBoolZ ? 1 : -1) + playerHolder.transform.position.z
            );

            GameObject rock = (GameObject)PrefabUtility.InstantiatePrefab(rockPrefab[Random.Range(0, rockPrefab.Length)]);
            rock.transform.position = randomPosition;
            rock.transform.SetParent(parentObject.transform);
            Undo.RegisterCreatedObjectUndo(rock, "Spawn Rock");
        }
    }

    private void RemoveTrees()
    {
        if (parentObject != null)
        {
            Undo.DestroyObjectImmediate(parentObject);
            parentObject = null;
        }
    }

    private void RegenerateTrees()
    {
        RemoveTrees();
        SpawnTrees();
    }
}
