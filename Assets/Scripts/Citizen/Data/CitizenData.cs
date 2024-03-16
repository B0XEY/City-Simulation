using System;
using Systems;
using Systems.Attributes;
using UnityEngine;

namespace Citizen.Data {
    public class CitizenData {
        private CityController m_city;
        private AnimationCurve m_healthCurve;
        private float m_currentHealth;
        
        private readonly string m_fullName;
        private readonly int m_dayOfBirth;
        
        private string m_address;
        private int m_age;
        private float m_expectedAge;

        public CitizenData(CityController city, string fullName, string address, int age, int dayOfBirth, AnimationCurve healthCurve) {
            m_city = city;
            m_fullName = fullName;
            m_address = address;
            m_age = age;
            m_dayOfBirth = dayOfBirth;

            m_healthCurve = healthCurve;

            m_currentHealth = 1;
            m_expectedAge = 100;
        }
        
        
        public void OnNewDay(int dayOfMonth, int currentDayOfYear) {
            //Death
            if (m_currentHealth <= 0.1f) {
                Debug.Log(m_fullName + " Died at age " + m_age);
                m_city.RemoveCitizen(this);
            }
            
            //Birthday Check
            if (currentDayOfYear == m_dayOfBirth) {
                m_age++;
                m_currentHealth = m_healthCurve.Evaluate(m_age / m_expectedAge);
                Debug.Log(m_fullName + " Just had there Birth Day. They are now " + m_age);
            }
        }
    }
}
