using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    public GameObject startButton;
    public GameObject gameBar;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
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
        //foodText.text = "Food to eat still: " + CountAllFood() + "/" + totalFood;
    }

    private void UpdateFoodCountDelayed()
    {
        foodText.text = "Score: " + (gameManager.totalFood - gameManager.CountAllFood()) + "/" + gameManager.totalFood;
    }
}
