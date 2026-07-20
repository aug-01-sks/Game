using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Animation State Names")]
    [SerializeField] private string idleStateName = "Idle_Battle_SwordAndShield";
    [SerializeField] private string runStateName = "MoveFWD_Normal_InPlace_SwordAndShield";

    [SerializeField] private float moveThreshold = 0.1f;
    [SerializeField] private float crossFadeTime = 0.1f;

    private int currentStateHash;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        PlayAnimation(idleStateName);
    }

    private void Update()
    {
        if (animator == null) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        bool isMoving = new Vector2(moveX, moveZ).magnitude > moveThreshold;

        if (isMoving)
        {
            PlayAnimation(runStateName);
        }
        else
        {
            PlayAnimation(idleStateName);
        }
    }

    private void PlayAnimation(string stateName)
    {
        int stateHash = Animator.StringToHash(stateName);

        if (currentStateHash == stateHash) return;

        animator.CrossFade(stateHash, crossFadeTime);
        currentStateHash = stateHash;
    }
}
