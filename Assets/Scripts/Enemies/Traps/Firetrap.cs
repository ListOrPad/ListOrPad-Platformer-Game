using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Health playerHealth;
    private Animator animator;
    private SpriteRenderer spriteRend;

    private bool triggered; //when the trap gets triggered
    private bool active; //when the trap is active and can hurt the player

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if(!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red; //turn the sprite red to notify the player

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.Instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; //return sprite back to initial color
        active = true;
        animator.SetBool("activated", true);

        //Wait for X seconds, deactivate trap and reset all vars and aniamtor
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);

    }
}
