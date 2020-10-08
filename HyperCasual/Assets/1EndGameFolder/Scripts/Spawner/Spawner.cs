using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Описание объектов для спавна")]
    [SerializeField] string description;
    [Tooltip("Список настроек для врагов")]
    [SerializeField] private List<EnemyData> enemySettings;
    public static Dictionary<GameObject, SpawnSpecifications> Spawners;

    [Tooltip("Количество объектов для вызова")]
    [SerializeField] private int poolCount;
    [Tooltip("Ссылка на базовый префаб")]
    [SerializeField] private GameObject basePrefab;
    [Tooltip("Время между спауном врагов")]
    [SerializeField] private float spawnTime;
    [Tooltip("Расстояние по х от персонажа, для спавна врага")]
    [SerializeField] private float scatterSpawnXPosition = 20f;
    [Tooltip("Расстояние по y от персонажа, для спавна врага")]
    [SerializeField] private float scatterSpawnYPosition = 20f;


    

    //Oueue - очередь
    private Queue<GameObject> currentSpawners;

    private void Start()
    {
        Spawners = new Dictionary<GameObject, SpawnSpecifications>();
        currentSpawners = new Queue<GameObject>();

        for (int i = 0; i < poolCount; ++i)
        {
            var prefab = Instantiate(basePrefab);
            var script = prefab.GetComponent<SpawnSpecifications>();
            prefab.SetActive(false);
            Spawners.Add(prefab, script);
            currentSpawners.Enqueue(prefab);
        }
        SpawnSpecifications.OnOverFly += ReturnObject;
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        if (spawnTime == 0)
        {
            Debug.Log("Не выставленно время спауна в EnemySpawner, задано стандартное значение - 1сек");
            spawnTime = 1f;
        }
        while (true){ 
        yield return new WaitForSeconds(spawnTime);
            if (currentSpawners.Count > 0)
            {
                //получение компонентов и активанция врага
                var enemy = currentSpawners.Dequeue();
                var script = Spawners[enemy];
                enemy.SetActive(true);

                //генерация случайного врага и инициализация его
                int random = Random.Range(0, enemySettings.Count);
                script.Init(enemySettings[random]);

                var characterPosition = Character.Instance.transform.position;
                float xPosition = Random.Range(characterPosition.x - scatterSpawnXPosition, characterPosition.x + scatterSpawnXPosition);
                float yPosition = characterPosition.y + scatterSpawnYPosition;
                enemy.transform.position = new Vector2(xPosition, yPosition);
            }
        }
    }
    private void ReturnObject(GameObject returnObject)
    {
        returnObject.transform.position = transform.position;
        returnObject.SetActive(false);
        currentSpawners.Enqueue(returnObject);
    }
}
