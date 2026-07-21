using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDamage : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animation")]
    [SerializeField] private string attackStateName = "Attack01_SwordAndShiled";
    [SerializeField] private float attackDuration = 0.8f;
    [SerializeField] private float damageTime = 0.3f;

    [Header("Damage")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int damage = 20;
    [SerializeField] private LayerMask enemyLayer;

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
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        if (animator != null)
        {
            animator.Play(attackStateName, 0, 0f);
        }

        yield return new WaitForSeconds(damageTime);

        DealDamage();

        yield return new WaitForSeconds(attackDuration - damageTime);

        isAttacking = false;
    }

    private void DealDamage()
    {
        Vector3 attackCenter = attackPoint != null
            ? attackPoint.position
            : transform.position + transform.forward;

        Collider[] hitEnemies = Physics.OverlapSphere(
            attackCenter,
            attackRange,
            enemyLayer
        );

        foreach (Collider collider in hitEnemies)
        {
            Enemy enemy = collider.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
          /*  HitReactionController1 hitReaction = enemy.GetComponentInParent<HitReactionController1>();
            if (hitReaction != null)
            {
                hitReaction.TakeHit();
            }*/
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 attackCenter = attackPoint != null
            ? attackPoint.position
            : transform.position + transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter, attackRange);
    }
}
