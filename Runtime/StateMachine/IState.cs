namespace VanceUtility.StateMachine {
    public interface IState {
        IState Update();
        IState Input();
        void OnStateEnter ();
        void OnStateExit ();
        void OnStateChange ();
    }
}