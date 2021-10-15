using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    public GameObject startButton;
    public GameObject gameBar;
    public GameObject gameOverWindow;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.onGameOver.AddListener(DisplayGameOver);
        if (gameManager.gameStarted)
        {
            UpdateFoodCount();
            startButton.SetActive(false);
        }
        else
        {
            gameBar.SetActive(false);
        }
    }

    public void UpdateFoodCount()
    {
        Invoke(nameof(UpdateFoodCountDelayed), 0);
    }

    private void UpdateFoodCountDelayed()
    {
        foodText.text = "Score: " + (gameManager.totalFood - gameManager.CountAllFood()) + "/" + gameManager.totalFood;
    }

    private void DisplayGameOver()
    {
        gameOverWindow.SetActive(true);

    }
}
