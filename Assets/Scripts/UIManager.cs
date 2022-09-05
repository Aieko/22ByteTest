using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject menu;

    public float progressBarFillSpeed = 10f;

    private float currentProgress;
    private float secondsCount;

    private int minuteCount;

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        if(menu.activeInHierarchy) menu.SetActive(false);

        currentProgress = 0;
        UpdateCoinsNumber();
        progressBar.maxValue = GameManager.Instance.progressToWin;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        var currentTime = GameManager.Instance.targetTime;

        if (currentTime < 0) currentTime = 0;

        minuteCount = Mathf.FloorToInt(currentTime / 60);
        secondsCount = Mathf.FloorToInt(currentTime % 60);

        timer.text = minuteCount + " : "+ secondsCount;
       
    }

    public void UpdateProgressBar(float newProgress)
    {
        currentProgress = newProgress;

        StartCoroutine(ChangeProgressCoroutine());
    }

    public void UpdateCoinsNumber()
    {
        coinsText.text = GameManager.Instance.GetCoins().ToString();
    }

    private IEnumerator ChangeProgressCoroutine()
    {
        if(currentProgress > progressBar.value)
        {
            while (currentProgress > progressBar.value)
            {
                progressBar.value += progressBarFillSpeed * Time.deltaTime;

                yield return null;
            }

            yield break;
        }
        else if(currentProgress < progressBar.value)
        {
            while (currentProgress < progressBar.value)
            {
                progressBar.value -= progressBarFillSpeed * Time.deltaTime;

                yield return null;
            }

            yield break;
        }
        
    }

    public void GameOver(bool win)
    {
        if (win) gameOverText.text = "Congratulations! You are a winner!";
        else gameOverText.text = "You lose... But next time you'll do better!";

        menu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void BackInMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
