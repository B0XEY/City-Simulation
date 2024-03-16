using System;
using Systems.Attributes;
using UnityEngine;

namespace Citizen.Data {
    public class CitizenData {
        [Header("Base Data"), Line] 
        public string FullName;
        public string Address;
        public int Age;
        public int DayOfBirth;

        public CitizenData(string fullName, string address, int age, int dayOfBirth) {
            FullName = fullName;
            Address = address;
            Age = age;
            DayOfBirth = dayOfBirth;
        }
        
        
        public void OnNewDay(int dayOfMonth, int currentDayOfYear) {
            //Birthday Check
            if (currentDayOfYear == DayOfBirth) {
                Age++;
                Debug.Log(FullName + " Just had there Birth Day. They are now " + Age);
            }
        }
    }
}
