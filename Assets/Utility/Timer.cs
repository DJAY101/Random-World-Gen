using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utility
{
    public class Timer : MonoBehaviour
    {
        // Start is called before the first frame update

        public float timerValue
        { get; set; }

        private bool timerRunning = false;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timerRunning) { timerValue += Time.deltaTime; }
        }

        //checks if the timer has passed x amount of seconds after starting
        public bool hasPassed(float seconds)
        {
            return timerValue > seconds;
        }

        public void StartTimer()
        {
            timerRunning = true;
        }

        public void StopTimer()
        {
            timerRunning = false;
        }

        public void ResetTimer()
        {
            timerValue = 0f;
        }



    }
}
