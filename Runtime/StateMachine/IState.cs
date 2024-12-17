namespace VanceUtility.StateMachine {
    public interface IState {
        void OnStateExecution();
        void OnStateEnter ();
        void OnStateExit ();
        void OnStateChange ();
    }
}