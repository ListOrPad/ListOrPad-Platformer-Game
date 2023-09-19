using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;

    private HealthPickup healthPickup;

    private bool hit;

    private BoxCollider2D collider;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        collider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        if(gameObject.tag == "Enemy Fireball")
        {
            transform.Translate(movementSpeed, 0, 0);
        }
        else
        {
            transform.Translate(-movementSpeed, 0, 0);

        }

        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        
        base.OnTriggerEnter2D(collision);
        collider.enabled = false;

        if (animator != null)
        {
            animator.SetTrigger("explode"); //when the object is a fireball explode it
        }
        else
        {
            gameObject.SetActive(false); //when this hits any object deactivates projectile
        }
        //if (collision.gameObject.tag == "Pickup")
        //{
        //    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), healthPickup.GetComponent<Collider2D>());
        //}
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
