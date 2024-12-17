using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VanceUtility.StateMachine {

    public class ExampleState : IState
    {

        private static ExampleState _instance;
        public static ExampleState Instance => _instance ??= new ExampleState();

        // Private constructor prevents external instantiation
         private ExampleState() { }
        public IState Update() {
            Debug.Log("Doing the example state update");
            return null;
        }
        public IState Input(){
            Debug.Log("Doing the example state input");
            return null;
        }
        public void OnStateEnter (){
            Debug.Log("Doing the example state OnStateEnter");
        }
        public void OnStateExit (){
            Debug.Log("Doing the example state OnStateExit");
        }
        public void OnStateChange (){
            Debug.Log("Doing the example state OnStateChanger");
        }
    }
}
