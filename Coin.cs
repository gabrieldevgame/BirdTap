using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameController controller;

    public AudioSource getCoin;

    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        getCoin = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            getCoin.Play();
            controller.score += 10;
            controller.scoreText.text = controller.score.ToString();
            Destroy(gameObject, 0.2f);
        }
    }
}
