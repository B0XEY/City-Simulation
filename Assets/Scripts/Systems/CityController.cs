using System;
using System.Collections.Generic;
using Citizen.Data;
using Systems.Attributes;
using UnityEngine;

namespace Systems {
    public class CityController : MonoBehaviour {
        [Header("References"), Line]
        [SerializeField] private TimeController time;

        
        private List<CitizenData> m_currentCitizens = new List<CitizenData>();

        [Button]
        private void CreateCitizen() {
            var newCitizen = new CitizenData("John Doe", "1234 car St", 0, time.CurrentDayToYear);
            m_currentCitizens.Add(newCitizen);
            time.onNewDay.AddListener(newCitizen.OnNewDay);
        }
    }
}