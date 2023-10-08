using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldownFireball;
    [SerializeField] private float attackCooldownSword;
    private float cooldownTimerFireball = Mathf.Infinity;
    private float cooldownTimerSword = Mathf.Infinity;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private AudioClip swordSound;

    private Animator animator;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && cooldownTimerFireball > attackCooldownFireball && playerMovement.CanAttack)
        {
            FireballAttack();
        }
        else if (Input.GetMouseButton(0) && cooldownTimerSword > attackCooldownSword && playerMovement.CanAttack)
        {
            SwordAttack();
        }
        cooldownTimerFireball += Time.deltaTime;
        cooldownTimerSword += Time.deltaTime;
    }

    private void SwordAttack()
    {
        SoundManager.Instance.PlaySound(swordSound);
        animator.SetTrigger("swordAttack");
        cooldownTimerSword = 0;
    }

    private void FireballAttack()
    {
        SoundManager.Instance.PlaySound(fireballSound);
        animator.SetTrigger("fireballAttack");
        cooldownTimerFireball = 0;

        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
