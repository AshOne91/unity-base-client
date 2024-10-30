namespace ASHFramework.Mono.Pattern
{
    public interface ASHStateInterface<TStateID> : ASHUpdatableInterface
    {
        void OnEnter(TStateID prevStateID);
        void OnLeave(TStateID nestStateID);
    }
    public class ASHFSM
    {

    }
}
