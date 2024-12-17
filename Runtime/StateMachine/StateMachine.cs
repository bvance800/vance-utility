using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VanceUtility.StateMachine {

    public class StateMachine : MonoBehaviour
    {
        IState _activeState;

        public void Input()
        {
            IState nextState = _activeState.Input();
            if (nextState != null && nextState != _activeState)
            {
                ChangeState(nextState);
            }
        }

        public void Update()
        {
            IState nextState = _activeState.Update();
            if (nextState != null && nextState != _activeState)
            {
                ChangeState(nextState);
            }
        }

        public void ChangeState(IState nextState) {
            _activeState.OnStateExit();
            _activeState = nextState;
            _activeState.OnStateEnter();
        }

        public void SetState(IState state) {
            _activeState = state;
        }
    }
}
