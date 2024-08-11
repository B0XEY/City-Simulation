using System;
using Systems;
using Systems.Attributes;
using UnityEngine;

namespace Citizen.Data {
    public struct CitizenData {
        private readonly CityController _city;
        private readonly AnimationCurve _healthCurve;
        private readonly float _expectedAge;
        private float _currentHealth;
        
        private readonly string _fullName;
        private readonly int _dayOfBirth;
        
        private string _address;
        private int _age;

        public CitizenData(CityController city, string fullName, string address, int age, int dayOfBirth) {
            _city = city;
            _fullName = fullName;
            _address = address;
            _age = age;
            _dayOfBirth = dayOfBirth;

            _healthCurve = _city.HealthOverTime;
            _expectedAge = _city.ExpectedAge;

            _currentHealth = _healthCurve.Evaluate(0);
        }
        
        
        public void OnNewDay(int dayOfMonth, int currentDayOfYear) {
            //Death
            if (_currentHealth <= 0.1f) {
                Debug.Log(_fullName + " Died at age " + _age);
                _city.RemoveCitizen(this);
                return;
            }
            
            //Birthday Check
            if (currentDayOfYear == _dayOfBirth) {
                _age++;
                _currentHealth = _healthCurve.Evaluate(_age / _expectedAge);
                Debug.Log(_fullName + " just had there Birth Day. They are now " + _age);
            }
        }
    }
}
