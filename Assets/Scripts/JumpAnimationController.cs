using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animation State Names")]
    [SerializeField] private string idleStateName = "Idle_Battle_SwordAndShield";
    [SerializeField] private string jumpStateName = "JumpFull_Normal_RM_SwordAndShield";

    [Header("Ground Check")]
    [SerializeField] private string groundTag = "Ground";

    [Header("Settings")]
    [SerializeField] private float crossFadeTime = 0.1f;

    private bool isGrounded = false;
    private bool hasPlayedIdleOnGround = false;
    private int currentStateHash;

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            hasPlayedIdleOnGround = false;
            PlayAnimation(jumpStateName);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            SetGrounded();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            SetGrounded();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
            hasPlayedIdleOnGround = false;
        }
    }

    private void SetGrounded()
    {
        isGrounded = true;

        if (!hasPlayedIdleOnGround)
        {
            PlayAnimation(idleStateName);
            hasPlayedIdleOnGround = true;
        }
    }

    private void PlayAnimation(string stateName)
    {
        int stateHash = Animator.StringToHash(stateName);

        if (currentStateHash == stateHash) return;

        animator.CrossFade(stateName, crossFadeTime);
        currentStateHash = stateHash;
    }
}
