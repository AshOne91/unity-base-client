using System;

using ASHFramework.Mono.Pattern;

namespace ASHFramework.Mono.Diagnostics
{
    public abstract class MAActionParamBase : MADispose, MAActionInterface
    {
        protected MAActionParamBase() { }
        protected override void Construct() { }
        public abstract void DoAction();
    }

    public sealed class MAActionParam : MAActionParamBase
    {
        private Action _action = null;

        public MAActionParam(Action action)
        {
            _action = action;
        }

        protected override void Destruct()
        {
            _action = null;
        }

        public override void DoAction()
        {
            _action();
        }
    }

    public sealed class MTActionParam<Param1> : MAActionParamBase
    {
        private Action<Param1> _action = null;
        private Param1 _param1;

        public MTActionParam(Action<Param1> action, Param1 param1)
        {
            _action = action;
            _param1 = param1;
        }

        protected override void Destruct()
        {
            _action = null;
        }

        public override void DoAction()
        {
            _action(_param1);
        }
    }

    public sealed class MTActionParam<Param1, Param2> : MAActionParamBase
    {
        private Action<Param1, Param2> _action = null;
        private Param1 _param1;
        private Param2 _param2;

        public MTActionParam(System.Action<Param1, Param2> action, Param1 param1, Param2 param2)
        {
            _action = action;
            _param1 = param1;
            _param2 = param2;
        }

        protected override void Destruct()
        {
            _action = null;
        }

        public override void DoAction()
        {
            _action(_param1, _param2);
        }
    }

    public sealed class MTActionParam<Param1, Param2, Param3> : MAActionParamBase
    {
        private System.Action<Param1, Param2, Param3> _action = null;
        private Param1 _param1;
        private Param2 _param2;
        private Param3 _param3;

        public MTActionParam(System.Action<Param1, Param2, Param3> action, Param1 param1, Param2 param2, Param3 param3)
        {
            _action = action;
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
        }

        protected override void Destruct()
        {
            _action = null;
        }

        public override void DoAction()
        {
            _action(_param1, _param2, _param3);
        }
    }

    public sealed class MTActionParam<Param1, Param2, Param3, Param4> : MAActionParamBase
    {
        private System.Action<Param1, Param2, Param3, Param4> _action = null;
        private Param1 _param1;
        private Param2 _param2;
        private Param3 _param3;
        private Param4 _param4;

        public MTActionParam(System.Action<Param1, Param2, Param3, Param4> action, Param1 param1, Param2 param2, Param3 param3, Param4 param4)
        {
            _action = action;
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;
        }

        protected override void Destruct()
        {
            _action = null;
        }

        public override void DoAction()
        {
            _action(_param1, _param2, _param3, _param4);
        }
    }
}