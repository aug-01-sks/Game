using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animation State Names")]
    [SerializeField] private string attackStateName = "Attack01_SwordAndShiled";

    [Header("Settings")]
    [SerializeField] private float crossFadeTime = 0.05f;
    [SerializeField] private float attackDuration = 0.08f;

    private bool isAttacking = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        if (animator == null) return;

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(PlayAttackOnce());
        }
    }

    private IEnumerator PlayAttackOnce()
    {
        isAttacking = true;

        animator.Play(attackStateName, 0, 0f);

        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }
}
