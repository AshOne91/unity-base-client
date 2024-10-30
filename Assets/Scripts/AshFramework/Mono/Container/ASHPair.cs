namespace ASHFramework.Mono.Container
{
    //제네릭
    // 컴파일타입에 타입을 안전하게 정의
    public class ASHPair<T1, T2> : ASHObject
    {
        public T1 _First { get; set; }
        public T2 _Second { get; set; }
        public ASHPair()
        {

        }
        public ASHPair(T1 first, T2 second)
        {
            this._First = first;
            this._Second = second;
        }
    }

    public class ASHPairEx<T1, T2> : ASHDisposeEX
    {
        public virtual T1 _First { get; set; }
        public virtual T2 _Second { get; set; }

        public ASHPairEx()
        {

        }

        public ASHPairEx(T1 first, T2 second)
        {
            this._First = first;
            this._Second = second;
        }

        protected override void OnDispose()
        {
        }
    }
}
