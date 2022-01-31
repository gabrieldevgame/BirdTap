using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject coin;
    public float height;
    public float maxTime;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newCoin = Instantiate(coin);
        newCoin.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
        Destroy(newCoin, 9f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > maxTime){
            GameObject newCoin = Instantiate(coin);
            newCoin.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(newCoin, 9f);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
