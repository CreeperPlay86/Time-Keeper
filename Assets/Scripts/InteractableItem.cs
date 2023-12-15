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
    public bool interactActive = false;
    public GameObject mark;

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

    public void SetInteract(bool newValue)
    {
		if (totalInteractionCount >= maxInteractions) return;
		interactActive = newValue;
        mark.SetActive(newValue);
	}

    void Interact()
    {
        if (!interactActive) return;
        RestartTimer();
        TimerManager.instance.ActiveNext();
		totalInteractionCount++;
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