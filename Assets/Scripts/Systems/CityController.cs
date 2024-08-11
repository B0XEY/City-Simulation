using System;
using System.Collections.Generic;
using Citizen.Data;
using Systems.Attributes;
using UnityEngine;
using QFSW.QC;

namespace Systems {
    public class CityController : MonoBehaviour {
        [Header("References"), Line]
        [SerializeField] private TimeController time;
        [Header("Data"), Line]
        [SerializeField] private int expectedAge;
        [SerializeField] private AnimationCurve healthOverTime;

        [SerializeField, ShowOnly]
        private int citizenCount;

        
        private HashSet<CitizenData> _currentCitizens = new HashSet<CitizenData>();
        
        public int ExpectedAge => expectedAge;
        public AnimationCurve HealthOverTime => healthOverTime;
        
        [Command]
        private void CreateCitizen(int amount) {
            for (var i = 0; i < amount; i++) {
                var newCitizen = new CitizenData(this, "John Doe", "1234 car St", 0, time.CurrentDayToYear);
                _currentCitizens.Add(newCitizen);
                time.onNewDay.AddListener(newCitizen.OnNewDay);
                citizenCount = _currentCitizens.Count;
            }
        }
        [Command]
        private void ClearCitizen() {
            _currentCitizens.Clear();
        }
        [Command]
        private void CitizenAmount() {
            Debug.Log("There are currently " + _currentCitizens.Count + " Citizens in the world!");
        }


        public void RemoveCitizen(CitizenData data) {
            if (_currentCitizens.Contains(data)) _currentCitizens.Remove(data);
            else Debug.LogError("Citizen does not exist!");
        }
    }
}