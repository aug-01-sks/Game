using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReactionController1 : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animation State Names")]
    [SerializeField] private string hitStateName = "GetHit01_SwordAndShield";
    [SerializeField] private string idleStateName = "Idle_Battle_SwordAndShield";
    [SerializeField] private string deathStateName = "Die01_SwordAndShield";

    [Header("Settings")]
    [SerializeField] private float hitDuration = 0.6f;
    [SerializeField] private bool returnToIdleAfterHit = true;

    private bool isHit = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }


    /// <summary>
    /// 受击动画
    /// </summary>
    public void TakeHit()
    {
        if (animator == null) return;
        if (isHit) return;


        StartCoroutine(PlayHitAnimation());
    }

    private IEnumerator PlayHitAnimation()
    {
        isHit = true;

        animator.Play(hitStateName, 0, 0f);

        yield return new WaitForSeconds(hitDuration);

        if (returnToIdleAfterHit)
        {
            animator.CrossFade(idleStateName, 0.1f);
        }

        isHit = false;
    }


    /// <summary>
    /// 死亡动画
    /// </summary>
    public void DieAnimator()
    {

        Collider collider = GetComponent<Collider>();

        if (collider != null)
        {
            collider.enabled = false;
        }
        Rigidbody rig = GetComponent<Rigidbody>();
        if (rig != null)
        {
            rig.useGravity = false;
        }

        if (animator != null)
        {
            animator.CrossFade(deathStateName, .1f);
        }
    }
}
