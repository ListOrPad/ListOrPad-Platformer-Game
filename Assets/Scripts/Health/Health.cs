using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("health")]
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if(CurrentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("died");

                //deactivate all attached component classes 
                foreach(Behaviour component in components)
                {
                    component.enabled = false;
                }

                dead = true;
            }
        }
    }
    public void AddHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, startingHealth);
    }
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0;i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
