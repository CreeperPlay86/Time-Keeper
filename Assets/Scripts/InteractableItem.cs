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
    public static int totalInteractionCount = 0; // Общий счетчик взаимодействий

    public Text interactionCountText; // Ссылка на текстовое поле

    public static int maxInteractions = 20; // Максимальное количество взаимодействий

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // Проверка расстояния между игроком и объектом
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        // Если игрок находится в пределах радиуса взаимодействия
        if (distanceToPlayer <= interactionRadius)
        {
            // Обработка взаимодействия (например, отображение подсказки, нажатие клавиши и т. д.)
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
        // Обновляем значение текстового поля в UI
        interactionCountText.text = totalInteractionCount + "/" + maxInteractions;
    }

    // Метод для отрисовки радиуса взаимодействия в сцене
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}