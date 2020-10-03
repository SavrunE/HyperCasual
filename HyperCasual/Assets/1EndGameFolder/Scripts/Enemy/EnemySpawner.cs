using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Список настроек для врагов")]
    [SerializeField] private List<EnemyData> enemySettings;
    [Tooltip("Количество объектов для вызова")]
    [SerializeField] private int poolCount;
    [Tooltip("Ссылка на базовый префаб")]
    [SerializeField] private GameObject basePrefab;
    [Tooltip("Время между спауном врагов")]
    [SerializeField] private float spawnTime;
    [Tooltip("Расстояние по х от персонажа, для спавна врага")]
    [SerializeField] private float spawnXPosition = 20f;
    [Tooltip("Расстояние по y от персонажа, для спавна врага")]
    [SerializeField] private float spawnYPosition = 20f;


    public static Dictionary<GameObject, Enemy> Enemies;

    //Oueue - очередь
    private Queue<GameObject> currentEnemies;

    private void Start()
    {
        Enemies = new Dictionary<GameObject, Enemy>();
        currentEnemies = new Queue<GameObject>();

        for (int i = 0; i < poolCount; ++i)
        {
            var prefab = Instantiate(basePrefab);
            var script = prefab.GetComponent<Enemy>();
            prefab.SetActive(false);
            Enemies.Add(prefab, script);
            currentEnemies.Enqueue(prefab);
        }
        Enemy.OnEnemyOverFly += ReturnEnemy;
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
            if (currentEnemies.Count > 0)
            {
                //получение компонентов и активанция врага
                var enemy = currentEnemies.Dequeue();
                var script = Enemies[enemy];
                enemy.SetActive(true);

                //генерация случайного врага и инициализация его
                int random = Random.Range(0, enemySettings.Count);
                script.Init(enemySettings[random]);

                var characterPosition = Character.Instance.transform.position;
                float xPosition = Random.Range(characterPosition.x - spawnXPosition, characterPosition.x + spawnXPosition);
                float yPosition = characterPosition.y + spawnYPosition;
                enemy.transform.position = new Vector2(xPosition, yPosition);
            }
        }
    }
    private void ReturnEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        enemy.SetActive(false);
        currentEnemies.Enqueue(enemy);
    }
}
