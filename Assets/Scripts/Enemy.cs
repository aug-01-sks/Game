using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [Header("Settings")]
    [SerializeField] private float crossFadeTime = 0.1f;
    [SerializeField] private bool disableColliderOnDeath = true;

    private int currentHealth;
    private HitReactionController1 controller;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        controller = gameObject.AddComponent<HitReactionController1>();
    }

    /// <summary>
    /// 被打
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log(gameObject.name + " took damage: " + damage + ", current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }

        controller.TakeHit();
    }


    /// <summary>
    /// 死掉
    /// </summary>
    private void Die()
    {
        if (isDead) return;

        isDead = true;
        controller.DieAnimator();
    }

}
