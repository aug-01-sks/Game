using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这个是血条item，挂在血条上，想find到enemy，然后在update时，一直跟随enemy，以至于能一直在enemy头上，可能还要面向摄像机
/// </summary>
public class EnemyBarItem : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform enemy;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 2.2f, 0f);

    private void Start()
    {
        if (enemy == null)
        {
            Enemy enemyComponent = GetComponentInParent<Enemy>();

            if (enemyComponent != null)
            {
                enemy = enemyComponent.transform;
            }
        }
    }

    private void LateUpdate()
    {
        if (enemy == null) return;

        transform.position = enemy.position + offset;
    }

    public void SetEnemy(Transform target)
    {
        enemy = target;
    }
}
