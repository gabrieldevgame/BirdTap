using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController controller;

    public float speed;
    public AudioSource gameOverSound;

    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
        gameOverSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            rig.velocity = Vector2.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "GameOver"){
            gameOverSound.Play();
        }

        if(controller.score > controller.scoreSaved){
            controller.scoreSaved = controller.score;
            PlayerPrefs.SetInt("ScoreSaved", controller.scoreSaved);
        }
        GameController.Instance.ShowGameOver();
        Time.timeScale = 0;
    }
}
