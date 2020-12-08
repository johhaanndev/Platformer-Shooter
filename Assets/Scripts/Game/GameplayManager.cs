using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public float currentTime = 0f;
    public int currentTimeInt = 0;
    [SerializeField] private float startingTime = 400f; // Original Mario level 1 - 1 timer was 400 seconds

    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Text gameOverText;

    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private Text pointsText;
    [SerializeField] private GameObject winningUI;
    [SerializeField] private Text finalPoints;

    [SerializeField] private AudioSource theme;
    [SerializeField] private GameObject gameOverClip;
    [SerializeField] private GameObject winClip;

    public ParticleSystem playerDeathParticles;

    private PlayerController playerController;
    
    private GameObject player;

    private bool isOver = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();
        theme.Play();
        gameOverClip.SetActive(false);
        winClip.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameplayUI.SetActive(true);
        gameOverMenu.SetActive(false);
        winningUI.SetActive(false);
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        // substract 1 each second
        currentTime -= 1 * Time.deltaTime;
        currentTimeInt = (int)currentTime;

        //Debug.Log(playerController.getDead());
        if (playerController.getDead())
        {
            playerDead();
            gameOverClip.SetActive(true);
        }
        else if (playerController.getWin())
        {
            playerWin();
        }
        else if (isOver)
        {
            timeOut();
        }
        else
        {
            if (currentTime < 10)
            {
                countdownText.text = "00" + currentTimeInt.ToString();
            }
            if (currentTime >= 10 && currentTime < 100)
            {
                countdownText.text = "0" + currentTimeInt.ToString();
            }
            else if (currentTime >= 100)
            {
                countdownText.text = currentTimeInt.ToString();
            }
            else if (currentTime <= 0)
            {
                isOver = true;
            }
        }

        pointsCounter(player.gameObject.GetComponent<PlayerController>().getPoints());
    }

    private void pointsCounter(int points)
    {
        if (points == 0)
        {
            pointsText.text = "000000";
        }
        else if (points < 100)
        {
            pointsText.text = "0000" + points.ToString();
        }
        else if (points >= 100 && points < 1000)
        {
            pointsText.text = "000" + points.ToString();
        }
        else
        {
            pointsText.text = "00" + points.ToString();
        }
    }

    private void playerDead()
    {
        theme.Stop();
        gameOverClip.SetActive(true);
        gameOverText.text = "YOU DIED";
        gameOverMenu.SetActive(true);
    }

    private void timeOut()
    {
        theme.Stop();
        gameOverClip.SetActive(true);
        currentTime = 0;
        gameOverText.text = "TIME OUT";
        gameOverMenu.SetActive(true);
    }

    private void playerWin()
    {
        theme.Stop();
        winClip.SetActive(true);
        finalPoints.text = "POINTS: " + playerController.getPoints();
        winningUI.SetActive(true);
        gameplayUI.SetActive(false);
    }
}
