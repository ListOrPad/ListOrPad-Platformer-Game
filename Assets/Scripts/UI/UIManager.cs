using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.Instance.PlaySound(gameOverSound);
    }

    #region Game over functions

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion
}
