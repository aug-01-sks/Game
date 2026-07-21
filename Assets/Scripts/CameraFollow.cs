using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 4f, -6f);
    [SerializeField] private float followSpeed = 8f;
    [SerializeField] private float rotateSpeed = 8f;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
