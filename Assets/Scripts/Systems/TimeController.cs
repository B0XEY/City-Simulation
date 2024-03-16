using System;
using QFSW.QC;
using Systems.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Systems {
    public class TimeController : MonoBehaviour{
        private readonly GUIStyle m_customStyle = new GUIStyle();
        private float m_minTimer;
        private float m_timer;
        
        private const float HoursToSeconds = 3f;
        private enum SpeedMultiplier {
            Normal = 1,
            Swift = 10,
            Rapid = 45,
            Fast = 100
        }
        
        //[Header("Events"), Line]
        [HideInInspector] public UnityEvent<int> onNewHour = new UnityEvent<int>();
        [HideInInspector] public UnityEvent<int, int> onNewDay = new UnityEvent<int, int>();
        [HideInInspector] public UnityEvent<int> onNewMonth = new UnityEvent<int>();
        [HideInInspector] public UnityEvent<int> onNewYear = new UnityEvent<int>();
#if UNITY_EDITOR
        [Header("Time Settings"), Line]
        [SerializeField, ToggleButtons] 
#endif
        private SpeedMultiplier speedMultiplier = SpeedMultiplier.Normal;
        [SerializeField] private int startYear = 0;
#if UNITY_EDITOR
        [ShowOnly, Label("Current Game Time: ")] 
#endif
        public string currentTime;
        
        public int CurrentMinute { get; private set; }
        public int CurrentHour { get; private set; }
        public int CurrentDayToMonth { get; private set; }
        public int CurrentDayToYear { get; private set; }
        public int CurrentMonth { get; private set; }
        public int CurrentYear { get; private set; }

        private void Start() {
            CurrentMinute = 0;
            CurrentHour = 0;
            CurrentDayToMonth = 0;
            CurrentDayToYear = 0;
            CurrentMonth = 0;
            CurrentYear = startYear;
            m_timer = HoursToSeconds;
            m_minTimer = HoursToSeconds / 60;
        }
        private void Update(){
            m_minTimer -= Time.deltaTime * (int)speedMultiplier;
            //Min
            if (m_minTimer < 0){
                CurrentMinute++;
                m_minTimer = HoursToSeconds / 60;
            }
            m_timer -= Time.deltaTime * (int)speedMultiplier;
            //Hours
            if (m_timer <= 0){
                CurrentHour++;
                CurrentMinute = 0;
                onNewHour?.Invoke(CurrentHour);
                //Days
                if (CurrentHour > 23){
                    CurrentDayToMonth++;
                    CurrentDayToYear++;
                    CurrentHour = 0;
                    onNewDay?.Invoke(CurrentDayToMonth, CurrentDayToYear);
                    //Months
                    if (CurrentDayToMonth > 29){
                        CurrentMonth++;
                        CurrentDayToMonth = 0;
                        onNewMonth?.Invoke(CurrentMonth);
                        //Years
                        if (CurrentMonth > 11){
                            CurrentYear++;
                            CurrentMonth = 0;
                            CurrentDayToYear = 0;
                            onNewYear?.Invoke(CurrentYear);
                        }
                    }
                }
                m_timer = HoursToSeconds;
            }
            //Set Current Time Data
            currentTime = $"{CurrentYear:0000}:{CurrentMonth:00}:{CurrentDayToMonth:00}:{CurrentHour:00}:{CurrentMinute:00} -  {CurrentDayToYear:000} ";
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


        [Command]
        private void ChangeSpeed(SpeedMultiplier newValue) => speedMultiplier = newValue;
        
    }
}
