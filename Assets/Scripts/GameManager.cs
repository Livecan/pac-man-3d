using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int totalFood;
    public UnityEvent onGameLost;
    public UnityEvent onGameWon;

    private static GameManager instance;

    public bool gameStarted = false;

    public Camera camera3;

    public static GameManager Instance { get => instance; }

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(this);
            GameObject.Find("Player").SetActive(false);
        } else
        {
            instance.gameStarted = true;
            instance.camera3.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalFood = CountAllFood();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(0);
    }

    public int CountAllFood()
    {
        return GameObject.FindGameObjectsWithTag("Food").Length;
    }

    private void GameOver()
    {
        camera3.gameObject.SetActive(true);
        GameObject.Find("Player").SetActive(false);
    }

    public void LoseGame()
    {
        GameOver();
        onGameLost.Invoke();
    }

    public void WinGame()
    {
        GameOver();
        onGameWon.Invoke();
    }
}
