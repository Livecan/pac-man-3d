using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    public GameObject introPanel;
    public GameObject gameBar;
    public GameObject gameOverWindow;
    public GameObject victoryWindow;
    public List<Button> restartButtons;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.onGameLost.AddListener(DisplayGameLost);
        gameManager.onGameWon.AddListener(DisplayGameWon);
        restartButtons.ForEach(button => button.onClick.AddListener(gameManager.StartNew));
        if (gameManager.gameStarted)
        {
            UpdateFoodCount();
            introPanel.SetActive(false);
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

    private void DisplayGameLost()
    {
        gameOverWindow.SetActive(true);
    }

    private void DisplayGameWon()
    {
        victoryWindow.SetActive(true);
    }
}
