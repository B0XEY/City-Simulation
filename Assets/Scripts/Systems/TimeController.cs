using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Systems {
    public class TimeController : MonoBehaviour{
        private readonly GUIStyle m_customStyle = new GUIStyle();
        private float m_minTimer;
        private float m_timer;
        
        [Header("Time")] 
        public UnityEvent<int> onNewHour = new UnityEvent<int>();
        public UnityEvent<int> onNewDay = new UnityEvent<int>();
        public UnityEvent<int> onNewMonth = new UnityEvent<int>();
        public UnityEvent<int> onNewYear = new UnityEvent<int>();
        [SerializeField] private int startYear = 0;
        [Space]
        [SerializeField, Range(0f, 5f)] private float hoursToSeconds = 3f;
        [Space]
        [SerializeField] private string currentTime;
        
        public static int CurrentMinute { get; private set; }
        public static int CurrentHour { get; private set; }
        public static int CurrentDay { get; private set; }
        public static int CurrentMonth { get; private set; }
        public static int CurrentYear { get; private set; }

        private void Start() {
            CurrentMinute = 0;
            CurrentHour = 0;
            CurrentDay = 0;
            CurrentMonth = 0;
            CurrentYear = startYear;
            m_timer = hoursToSeconds;
            m_minTimer = hoursToSeconds / 60;
        }
        private void Update(){
            m_minTimer -= Time.deltaTime;
            //Min
            if (m_minTimer < 0){
                CurrentMinute++;
                m_minTimer = hoursToSeconds / 60;
            }
            m_timer -= Time.deltaTime;
            //Hours
            if (m_timer <= 0){
                CurrentHour++;
                CurrentMinute = 0;
                onNewHour?.Invoke(CurrentHour);
                //Days
                if (CurrentHour > 23){
                    CurrentDay++;
                    CurrentHour = 0;
                    onNewDay?.Invoke(CurrentDay);
                    //Months
                    if (CurrentDay > 29){
                        CurrentMonth++;
                        CurrentDay = 0;
                        onNewMonth?.Invoke(CurrentMonth);
                        //Years
                        if (CurrentMonth > 11){
                            CurrentYear++;
                            CurrentMonth = 0;
                            onNewYear?.Invoke(CurrentYear);
                        }
                    }
                }
                m_timer = hoursToSeconds;
            }
            //Set Current Time Data
            currentTime = $"{CurrentYear:0000} : {CurrentMonth:00} : {CurrentDay:00} : {CurrentHour:00} : {CurrentMinute:00}";
        }

        private void OnDisable(){
            onNewHour.RemoveAllListeners();
            onNewDay.RemoveAllListeners();
            onNewMonth.RemoveAllListeners();
            onNewYear.RemoveAllListeners();
        }
        private void OnGUI() {
            m_customStyle.fontSize = 30;
            m_customStyle.normal.textColor = Color.white;

            GUI.Label(new Rect(10, 10, 100, 20), currentTime, m_customStyle);
        }
    }
}
