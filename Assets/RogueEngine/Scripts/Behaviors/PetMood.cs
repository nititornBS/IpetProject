using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;



namespace RogueEngine
{
[CreateAssetMenu(fileName = "mood", menuName = "TcgEngine/Behaviors/Mood")]
public class Mood : ScriptableObject
{
    private bool isAngry;
    private bool isHappy;
    private bool isSad;
    private bool isTired;
    private bool isHungry;
    private bool isSick;
    private bool isDead;
    private bool isAsleep;
        // Getter and Setter for isAngry
        public bool IsAngry
        {
            get { return isAngry; }
            set { isAngry = value; }
        }

        // Getter and Setter for isHappy
        public bool IsHappy
        {
            get { return isHappy; }
            set { isHappy = value; }
        }

        // Getter and Setter for isSad
        public bool IsSad
        {
            get { return isSad; }
            set { isSad = value; }
        }

        // Getter and Setter for isTired
        public bool IsTired
        {
            get { return isTired; }
            set { isTired = value; }
        }

        // Getter and Setter for isHungry
        public bool IsHungry
        {
            get { return isHungry; }
            set { isHungry = value; }
        }

        // Getter and Setter for isSick
        public bool IsSick
        {
            get { return isSick; }
            set { isSick = value; }
        }

        // Getter and Setter for isDead
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        // Getter and Setter for isAsleep
        public bool IsAsleep
        {
            get { return isAsleep; }
            set { isAsleep = value; }
        }
    }
}