using System.Collections;
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

    private int enemyCount = 0;
    public TextMeshProUGUI enemyText;
    // Start is called before the first frame update
    void Start()
    {
        enemyText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decEnemyCount()
    {
        enemyCount -= 1;
        enemyText.text = "Farmers Remaining: " + enemyCount;
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

    public void StartButton()
    {
        startButton.SetActive(false);
        enemyCount = 5;
        enemyText.text = "Farmers Remaining: " + enemyCount;

        StartCoroutine(LoadYourAsyncScene("Level1"));

    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void StartDialog(string text)
    {
        dialogBox.SetActive(true);
        dialogText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void HideDialog()
    {
        dialogBox.SetActive(false);
    }
}
