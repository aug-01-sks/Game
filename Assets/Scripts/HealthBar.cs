using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Canvas healthCanvas;
    [SerializeField] private bool lookAtCamera = true;
    [SerializeField] private bool hideWhenDead = true;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (healthSlider == null)
        {
            healthSlider = GetComponentInChildren<Slider>();
        }

        if (healthCanvas == null)
        {
            healthCanvas = GetComponentInChildren<Canvas>();
        }
    }

    private void LateUpdate()
    {
        if (!lookAtCamera) return;
        if (mainCamera == null) return;

        transform.LookAt(transform.position + mainCamera.transform.forward);
    }

    public void SetMaxHealth(int maxHealth)
    {
        if (healthSlider == null) return;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(int currentHealth)
    {
        if (healthSlider == null) return;

        healthSlider.value = currentHealth;

        if (hideWhenDead && currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
