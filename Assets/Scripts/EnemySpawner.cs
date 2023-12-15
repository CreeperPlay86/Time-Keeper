using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float interactionRadius = 2f;

    public float Timer = 30f;
    public float TimerRestart = 30f;

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        if(Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }

        if(Timer <= 0)
        {
            SpawnEnemy();
            Timer = TimerRestart;
        }

        // Проверка расстояния между игроком и объектом
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        // Если игрок находится в пределах радиуса взаимодействия
        if (distanceToPlayer <= interactionRadius)
        {
            // Обработка взаимодействия (например, отображение подсказки, нажатие клавиши и т. д.)
            if (Input.GetKeyDown(KeyCode.E))
            {
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Instantiate(enemyPrefab, position, rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
