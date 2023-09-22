using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position; //Move player to checkpoint position
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            //Store the checpoint that we just activated as the current one
            currentCheckpoint = collision.transform;
            SoundManager.Instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider so we won't interact with it again
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
