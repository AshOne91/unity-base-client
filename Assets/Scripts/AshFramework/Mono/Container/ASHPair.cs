namespace ASHFramework.Mono.Container
{
    //���׸�
    // ������Ÿ�Կ� Ÿ���� �����ϰ� ����
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
