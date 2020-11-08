using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /* Serialized Private Fields */
    [SerializeField] private Player player = null;
    [SerializeField] private GameObject startInstructions = null;
    [SerializeField] private GameObject restartInstructions = null;
    
    /* Private Fields */
    private bool _levelStarted = false;

    /* Make sure correct instructions are displayed upon Start */
    void Start()
    {
        startInstructions.SetActive(true);
        restartInstructions.SetActive(false);
    }

    /* Checks for input each frame */
    void Update()
    {
        HandleInput();
    }

    /* Reads and handles appropriate action for each input */
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_levelStarted) ReloadLevel();
            else StartLevel();
        }
    }

    /* Starts the level by disabling instructions and activating the player object */
    private void StartLevel()
    {
        _levelStarted = true;
        
        /* Disable Instructions */
        startInstructions.SetActive(false);
        
        /* Enable player */
        player.enabled = true;
    }
    
    /* Reload the level */
    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void EndLevel()
    {
        restartInstructions.SetActive(true);
    }
}
