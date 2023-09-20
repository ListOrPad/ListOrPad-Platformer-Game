using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;

    private bool attacking;

    private Vector3[] directions = new Vector3[4];

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //Move spikehead to destination only if attacking
        if(attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay )
            {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //check if spike sees the player in all 4 dimensions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null & !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlaySound(impactSound);
        base.OnTriggerEnter2D (collision);
        Stop(); //Stop spike once he hits something
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //right direction
        directions[1] = -transform.right * range; //left direction
        directions[2] = transform.up * range; //up direction
        directions[3] = -transform.up * range; //down direction
    }


}
