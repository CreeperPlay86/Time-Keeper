using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    public float timer = 30f;
    public float TimerRestart = 30f;
    public Text TimerText;

    public GameObject ObjectA;
    public GameObject subObjectA;
    public GameObject ObjectB;
    public GameObject subObjectB;

    public float interactionRadius = 3f;
    public static int totalInteractionCount = 0; // ����� ������� ��������������

    public Text interactionCountText; // ������ �� ��������� ����

    public static int maxInteractions = 20; // ������������ ���������� ��������������

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // �������� ���������� ����� ������� � ��������
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        // ���� ����� ��������� � �������� ������� ��������������
        if (distanceToPlayer <= interactionRadius)
        {
            // ��������� �������������� (��������, ����������� ���������, ������� ������� � �. �.)
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        Timer();
    }

    void Timer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            print("aaa");
        }
        
    }

    void RestartTimer()
    {
        timer = TimerRestart;
    }

    void Interact()
    {
        if (totalInteractionCount >= maxInteractions) return;

        if (ObjectA.activeSelf && timer <= 0)
        {
            RestartTimer();
            ObjectB.SetActive(true);
            subObjectB.SetActive(false);
            totalInteractionCount++;
            ObjectA.SetActive(false);
            subObjectA.SetActive(true);
        }
        else if (ObjectB.activeSelf && timer <= 0)
        {
            RestartTimer();
            ObjectB.SetActive(false);
            subObjectB.SetActive(true);
            totalInteractionCount++;
            ObjectA.SetActive(true);
            subObjectA.SetActive(false);
        }

        UpdateUI();
    }
    void UpdateUI()
    {
        // ��������� �������� ���������� ���� � UI
        interactionCountText.text = totalInteractionCount + "/" + maxInteractions;
    }

    // ����� ��� ��������� ������� �������������� � �����
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}