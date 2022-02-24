using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController controller;

    public float speed;
    private float speedDying = 1f;
    private bool dead = false;
    private bool scoreAdmob = false;
    private bool canTap = false;
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
        Invoke("ActiveRigidbodyAndTap", 1.5f);
        dead = false;
        scoreAdmob = false;
        canTap = false;
        AdmobManager.instance.RequestBanner();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canTap == true && !dead){
            rig.velocity = Vector2.up * speed;
        }
    }

    void FixedUpdate() {
        if(dead){
            Invoke("SpeedDying", 0f);
        }
    }

    void ActiveRigidbodyAndTap(){
        rig.gravityScale = 0.6f;
        canTap = true;
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

        if(dead == false){
            AdmobManager.instance.deaths++;
        }

        if(GameController.Instance.score >= Random.Range(30,150) && dead == false){
            scoreAdmob = true;
        }

        dead = true;

        if(controller.score > controller.scoreSaved){
            controller.scoreSaved = controller.score;
            PlayerPrefs.SetInt("ScoreSaved", controller.scoreSaved);
        }
        GameController.Instance.ShowGameOver();

        if(AdmobManager.instance.deaths >= Random.Range(3,8)){
            AdmobManager.instance.deaths = 0;
            AdmobManager.instance.ShowInterstitial();
        }
        
        if(scoreAdmob == true){
            AdmobManager.instance.deaths = 0;
            AdmobManager.instance.ShowInterstitial();
            scoreAdmob = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Destroy"){
            Destroy(gameObject);
        }
    }
}
