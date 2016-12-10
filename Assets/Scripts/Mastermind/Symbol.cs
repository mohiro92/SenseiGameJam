﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Mastermind
{
    public class Symbol : MonoBehaviour
    {
        // Randomization component
        private static System.Random randomGenerator = new System.Random();
        // Available cipher symbols GameObjects
        public List<GameObject> availableSymbols;
        // Actual value of symbol
        public int value;

        // Use this for initialization
        void Start()
        {
            foreach(GameObject symbol in availableSymbols)
            {
                GameObject child = Instantiate(symbol);
                child.transform.SetParent(transform, false);
                child.SetActive(false);
            }
            RandomizeValue();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision col)
        {
            print("Collision");
            //if (col.gameObject.tag.Equals(Constants.BulletTag))
            //{
            print(col.transform.position.z + " " + transform.position.z);
                if (col.transform.position.z > transform.position.z)
                {
                    Next();
                } else
                {
                    Previous();
                }
            //}
        }

        public void RandomizeValue()
        {
            value = randomGenerator.Next(0, availableSymbols.Count);
            transform.GetChild(value).gameObject.SetActive(true);
        }

        public static bool operator ==(Symbol emp1, Symbol emp2)
        {
            return emp1.value == emp2.value;
        }

        public static bool operator !=(Symbol emp1, Symbol emp2)
        {
            return emp1.value != emp2.value;
        }

        public int TypeCount()
        {
            return availableSymbols.Count;
        }

        private void Next()
        {
            int newValue = (value + 1) % availableSymbols.Count;
            print("Next " + newValue);
            Activate(newValue);
        }

        private void Previous()
        {
            int newValue = value-1;
            if(newValue < 0)
            {
                newValue = availableSymbols.Count - 1;
            }
            print("Previous " + newValue);
            Activate(newValue);
        }
        
        private void Activate(int index)
        {
            if (value < transform.childCount && value >= 0)
            {
                transform.GetChild(value).gameObject.SetActive(false);
            }
            value = index;
            if (value < transform.childCount && value >= 0)
            {
                transform.GetChild(value).gameObject.SetActive(true);
            }
        }
    }
}