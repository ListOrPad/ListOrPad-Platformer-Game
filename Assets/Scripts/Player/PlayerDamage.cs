using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] protected float swordDamage;
    [SerializeField] private float range;
    private Health enemyHealth;
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D playerCollider;
    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;

    private bool EnemyInRange()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y, playerCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        if (raycastHit.collider != null)
        {
            enemyHealth = raycastHit.transform.GetComponent<Health>();
        }

        return raycastHit.collider != null; //"if hit.collider != null, return true"
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(playerCollider.bounds.size.x * range, playerCollider.bounds.size.y, playerCollider.bounds.size.z));
    }   

    /// <summary>
    /// used in Sword Attack animation event
    /// </summary>
    private void DamageEnemy()
    {
        //if enemy still in range damage him
        if (EnemyInRange())
        {
            enemyHealth.TakeDamage(swordDamage);
        }
    }
}
