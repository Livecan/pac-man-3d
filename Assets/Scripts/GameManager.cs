using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalFood;
    private static GameManager instance;

    public bool gameStarted = false;

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
            instance.GetComponentInChildren<Camera>().gameObject.SetActive(false);
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
}
