﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public GameObject startButton;
    public GameObject backgroundImage;

    public GameObject canvas;
    public GameObject events;

    public GameObject dialogBox;
    public GameObject dialogText;
    public TextMeshProUGUI titleText;

    private Coroutine dialogCo;

    private int enemyCount = 0;
    public TextMeshProUGUI enemyText;

    // Start is called before the first frame update
    void Start()
    {
        enemyText.text = "";
        titleText.text = "SNAIL REVOLT";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decEnemyCount()
    {
        enemyCount -= 1;
        if (enemyCount == 0)
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                StartCoroutine(LoadYourAsyncScene("Level2"));
                enemyCount = 8;
            } else if (SceneManager.GetActiveScene().name == "Level2")
            {
                StartCoroutine(LoadYourAsyncScene("Level3"));
                enemyCount = 12;
            } else
            {
                Win();
            }
            
        }
        if (enemyCount > 0)
        {
            enemyText.text = "Farmers Remaining: " + enemyCount;
        }
        
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ColorLerp(Color endvalue, float duration)
    {
        float time = 0;
        Image sprite = backgroundImage.GetComponent<Image>();
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endvalue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endvalue;
    }

    public void StartButton()
    {
        startButton.SetActive(false);
        titleText.text = "";
        StartCoroutine(LoadYourAsyncScene("Level1"));
        enemyCount = 5;
        enemyText.text = "Farmers Remaining: " + enemyCount;
        
    }

    public void restart()
    {
        StartCoroutine(ColorLerp(Color.white, 2));
        enemyCount = 0;
        enemyText.text = "";
        titleText.text = "GAME OVER!";
        startButton.SetActive(true);
    }

    public void Win()
    {
        StartCoroutine(ColorLerp(Color.white, 2));
        enemyCount = 0;
        enemyText.text = "";
        titleText.text = "Congratulations!";
        startButton.SetActive(true);
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartCoroutine(ColorLerp(new Color(1, 1, 1, 0), 2));
    }

    public void StartDialog(string text)
    {
        dialogBox.SetActive(true);
        dialogCo = StartCoroutine(TypeText(text));
    }

    public void HideDialog()
    {
        dialogBox.SetActive(false);
        StopCoroutine(dialogCo);
    }

    IEnumerator TypeText(string text)
    {
        dialogText.GetComponent<TextMeshProUGUI>().text = "";
        foreach (char c in text.ToCharArray())
        {
            dialogText.GetComponent<TextMeshProUGUI>().text += c;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
