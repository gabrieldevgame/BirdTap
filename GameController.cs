using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject menu;
    public int score;
    public Text scoreText;
    public Text bestScoreText;
    public int scoreSaved;

    public AudioSource button;

    public static GameController Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Time.timeScale = 1;

        button = GetComponent<AudioSource>();

        scoreSaved = PlayerPrefs.GetInt("ScoreSaved");
        bestScoreText.text = scoreSaved.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGameOver(){
        gameOver.SetActive(true);
    }

    public void ShowMenu(){
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    public void CloseMenu(){
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit(){
        SceneManager.LoadScene(0);
    }

    public void RestartScene(){
        SceneManager.LoadScene(1);
    }

    public void InitGame(){
        SceneManager.LoadScene(1);
    }

    public void ButtonSound(){
        button.Play();
    }
}
