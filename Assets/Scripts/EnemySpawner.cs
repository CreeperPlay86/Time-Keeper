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

        // �������� ���������� ����� ������� � ��������
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        // ���� ����� ��������� � �������� ������� ��������������
        if (distanceToPlayer <= interactionRadius)
        {
            // ��������� �������������� (��������, ����������� ���������, ������� ������� � �. �.)
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
