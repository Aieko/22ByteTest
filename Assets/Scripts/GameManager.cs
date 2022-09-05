using UnityEngine;
using System.Collections;

public enum Sounds
{
    CoinPickup,
    BallWasCatched,
    BallWasMissed,
    BombWasCatched,
}

public class GameManager : MonoBehaviour
{
  

    public static GameManager Instance;
    public float targetTime = 240;
    public int progressToWin;

    [SerializeField] private int progressForCatch = 10;
    [SerializeField] private Coins coins;
    [Space]
    public bool gameOver;
    [Space]
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip bombSound;
    [SerializeField] private AudioClip ballCatchedSound;
    [SerializeField] private AudioClip ballMissedSound;
    [SerializeField] private AudioClip timeIsOverSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private float currentProgress;
    private AudioSource audioSource;

    private void Awake()
    {
        if (!Instance)
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
        coins.count = PlayerPrefs.GetInt("Coins");
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameOver = false;
        currentProgress = 0;
       
    }

    private void Update()
    {
        if (targetTime > 0)
        {
            targetTime -= Time.deltaTime;
        }
        else if(!gameOver)
        {
            TimerEnded();
        }

    }

    private void TimerEnded()
    {
        targetTime = 0;
        gameOver = true;
        audioSource.PlayOneShot(timeIsOverSound);
        PlayerPrefs.SetInt("Coins", coins.count);
        StartCoroutine(GameOver());
    }
    
    public void AddToProgress(int value = 0)
    {
        //if method is calling without parameters then just add progress for catch the ball
        if (value == 0)
        {
            value = progressForCatch;
        }

        if (currentProgress + value < 0) currentProgress = 0;
        else if (currentProgress + value > progressToWin) currentProgress = progressToWin;
        else currentProgress += value;

        UIManager.Instance.UpdateProgressBar(currentProgress);
    }

    public int GetCoins()
    {
        return coins.count;
    }

    public void AddCoin()
    {
        coins.count++;
        UIManager.Instance.UpdateCoinsNumber();
    }

    public void PlaySound(Sounds soundType)
    {
        AudioClip sound = null;

        switch (soundType)
        {
            case Sounds.CoinPickup:
                sound = coinSound;
                break;
            case Sounds.BallWasCatched:
                sound = ballCatchedSound;
                break;
            case Sounds.BallWasMissed:
                sound = ballMissedSound;
                break;
            case Sounds.BombWasCatched:
                sound = bombSound;
                break;
            default:
                break;
        }
        
        if(sound) audioSource.PlayOneShot(sound);
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);

        var win = currentProgress >= progressToWin;
        var sound = win ? winSound : loseSound;

        audioSource.PlayOneShot(sound);

        UIManager.Instance.GameOver(win);
    }
}
