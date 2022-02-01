using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController controller;

    public float speed;
    private float speedDying = 1f;
    private bool dead = false;
    public AudioSource gameOverSound;
    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
        gameOverSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rig.gravityScale = 0;
        Invoke("ActiveRigidbody2D", 1.5f);
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead){
            Invoke("SpeedDying", 0f);
        }
    }

    void FixedUpdate() {
        Invoke("TapActive", 1.5f);
    }

    void ActiveRigidbody2D(){
        rig.gravityScale = 0.6f;
    }

    void TapActive(){
        if(Input.GetMouseButtonDown(0)){
            rig.velocity = Vector2.up * speed;
        }
    }

    void SpeedDying(){
        transform.position += Vector3.left * speedDying * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        anim.SetTrigger("Dead");

        if(!dead){
            if(other.gameObject.tag == "GameOver"){
                gameOverSound.Play();
            }
        }

        dead = true;

        if(controller.score > controller.scoreSaved){
            controller.scoreSaved = controller.score;
            PlayerPrefs.SetInt("ScoreSaved", controller.scoreSaved);
        }
        GameController.Instance.ShowGameOver();
        Destroy(gameObject, 2f);
    }
}
