using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VanceUtility.StateMachine;

public class ExamplePlant : MonoBehaviour
{

    private StateMachine _plantStateMachine;
    // Start is called before the first frame update
    void Start()
    {
        IState initialPlantState = ExampleState.Instance;
        _plantStateMachine = new StateMachine();
        _plantStateMachine.SetState(initialPlantState);
    }

    // Update is called once per frame
    void Update()
    {
        _plantStateMachine.Update();
    }
}
