using System;
using System.Collections.Generic;
using Citizen.Data;
using Systems.Attributes;
using UnityEngine;
using QFSW.QC;

namespace Systems {
    public class CityController : MonoBehaviour {
#if UNITY_EDITOR
        [Header("References"), Line]
#endif
        [SerializeField] private TimeController time;
#if UNITY_EDITOR
        [Header("Data"), Line]
#endif
        [SerializeField] private int expectedAge;
        [SerializeField] private AnimationCurve healthOverTime;
#if UNITY_EDITOR
        [SerializeField, ShowOnly]
#endif
        private int citizenCount;

        
        private List<CitizenData> m_currentCitizens = new List<CitizenData>();
#if UNITY_EDITOR
        [Button]
#endif
        private void CreateCitizen() {
            var newCitizen = new CitizenData(this, "John Doe", "1234 car St", 0, time.CurrentDayToYear, healthOverTime);
            m_currentCitizens.Add(newCitizen);
            time.onNewDay.AddListener(newCitizen.OnNewDay);
            citizenCount = m_currentCitizens.Count;
        }
        
        [Command]
        private void CreateCitizen(int amount) {
            for (var i = 0; i < amount; i++) {
                var newCitizen = new CitizenData(this, "John Doe", "1234 car St", 0, time.CurrentDayToYear, healthOverTime);
                m_currentCitizens.Add(newCitizen);
                time.onNewDay.AddListener(newCitizen.OnNewDay);
                citizenCount = m_currentCitizens.Count;
            }
        }
        [Command]
        private void ClearCitizen() {
            m_currentCitizens.Clear();
        }
        [Command]
        private void CitizenAmount() {
            Debug.Log("There are currently " + m_currentCitizens.Count + " Citizens in the world!");
        }


        public void RemoveCitizen(CitizenData data) {
            if (m_currentCitizens.Contains(data)) m_currentCitizens.Remove(data);
            else Debug.LogError("Citizen does not exist!");
        }
    }
}