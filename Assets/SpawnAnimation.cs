using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnAnimation : MonoBehaviour
{
    // create the random generator
    System.Random rnd = new System.Random();
    
    // the random delay variable
    float randomDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        //set the randomdelay to a random value between 0 and 1
        randomDelay = (float) rnd.NextDouble();
        //set the current cubes scale to 0
        transform.localScale = new Vector3(0, 0, 0);
    
    }// start

    //timer variables
    float timeCounter = 0;
    float startTimer = 0;

    // Update is called once per frame
    void Update()
    {
        //if the timer has passed the randomDelay then perform the action
        if (startTimer > randomDelay)
        {
            // create a variable for the new scale
            var scale = Mathf.Clamp(Mathf.Pow(timeCounter, 2) * 10, 0, 1); //the scale is proportional to the quadratic of time passed since the animation started, and clamped between 0 and 1
            //apply the scale
            transform.localScale = new Vector3(scale, scale, scale);
            //increase the timer since the animation started
            timeCounter += Time.deltaTime;
        
        }
        //increase random delay timer
        startTimer += Time.deltaTime;
    }//update

}//spawn animation
