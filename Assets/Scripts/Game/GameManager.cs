using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image life1, life2, life3;
    public Sprite lifeOn, lifeOff;

    [SerializeField] private Button startButton, quitButton, retryButton;
    [SerializeField] private GameObject gameOverObj;

    private S_PlayerController playerController;
    private Vector3 playerStartPos;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<S_PlayerController>();
        playerStartPos = playerController.transform.position;
        
        //Assign events
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        retryButton.onClick.AddListener(RestartGame);
    }

    public void UpdateLives()
    {
        //lives display
        switch(playerController.lives)
        {
            case 3:
                life1.sprite = lifeOn;
                life2.sprite = lifeOn;
                life3.sprite = lifeOn;
                break;
            case 2:
                life1.sprite = lifeOff;
                life2.sprite = lifeOn;
                life3.sprite = lifeOn;
                break;
            case 1:
                life1.sprite = lifeOff;
                life2.sprite = lifeOff;
                life3.sprite = lifeOn;
                break;
            case 0:
                life1.sprite = lifeOff;
                life2.sprite = lifeOff;
                life3.sprite = lifeOff;
                break;
        }
    }

    public void ShowEndScreen()
    {
        gameOverObj.SetActive(true);
    }

    public void StartGame()
    {
        playerController.canMove = true;
        startButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        gameOverObj.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
