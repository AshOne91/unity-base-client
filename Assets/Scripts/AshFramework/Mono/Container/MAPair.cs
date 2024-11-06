namespace ASHFramework.Mono.Container
{
    //제네릭
    // 컴파일타입에 타입을 안전하게 정의
    public class MAPair<T1, T2> : MAObject
    {
        public T1 _First { get; set; }
        public T2 _Second { get; set; }
        public MAPair()
        {

        }
        public MAPair(T1 first, T2 second)
        {
            this._First = first;
            this._Second = second;
        }
    }

    public class MAPairEx<T1, T2> : MADisposeEX
    {
        public virtual T1 _First { get; set; }
        public virtual T2 _Second { get; set; }

        public MAPairEx()
        {

        }

        public MAPairEx(T1 first, T2 second)
        {
            this._First = first;
            this._Second = second;
        }

        protected override void OnDispose()
        {
        }
    }
}
