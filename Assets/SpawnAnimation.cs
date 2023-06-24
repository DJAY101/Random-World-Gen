using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnAnimation : MonoBehaviour
{

    System.Random rnd = new System.Random();
    float randomDelay;
    // Start is called before the first frame update
    void Start()
    {
        randomDelay = (float) rnd.NextDouble();
        transform.localScale = new Vector3(0, 0, 0);
    }


    float timeCounter = 0;
    float startTimer = 0;
    // Update is called once per frame
    void Update()
    {
        if (startTimer > randomDelay)
        {
            var scale = Mathf.Clamp(Mathf.Pow(timeCounter, 2) * 10, 0, 1);
            transform.localScale = new Vector3(scale, scale, scale);
            timeCounter += Time.deltaTime;
        }
        startTimer += Time.deltaTime;
    }
}
