using System;
using UnityEngine;

namespace ASHFramework.Mono.Diagnostics
{
    public class MANoticeTimer : MADisposeEX
    {
        public enum EType
        {
            Play = 0,
            Pause,
            Stop
        }

        protected EType _eType = EType.Stop;
        public virtual EType Type
        {
            get { return _eType; }
            protected set
            {
                if (_eType == value)
                    return;

                _eType = value;
                switch (_eType)
                {
                    case EType.Play:
                        _curTime = 0f;
                        break;

                    case EType.Pause:
                        break;

                    case EType.Stop:
                        _curTime = _totalTime;
                        break;
                }
            }
        }

        protected bool _loop = false;
        public virtual bool Loop
        {
            get { return _loop; }
            set
            {
                if (_loop == value)
                    return;
                _loop = value;
            }
        }

        protected Action<float> _fnPercent = null;
        public Action<float> FnPercent
        {
            get { return _fnPercent; }
            set { _fnPercent = value; }
        }

        protected float _curTime = 0f;
        protected float _totalTime = 1f;
        public float TotalTime
        {
            get { return _totalTime; }
            set
            {
                _totalTime = value;
                switch (_eType)
                {
                    case EType.Play:
                    case EType.Pause:
                        _curTime = Mathf.Clamp(_curTime, 0f, _totalTime);
                        if (_curTime == _totalTime)
                            Type = EType.Stop;
                        break;

                    case EType.Stop:
                        _curTime = _totalTime;
                        break;
                }
            }
        }

        protected float _speed = 1f;
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float Percent
        {
            get { return (_curTime / _totalTime); }
        }

        protected override void OnDispose()
        {
            DoStop();
            _fnPercent = null;
        }

        public void DoPlay()
        {
            Type = EType.Play;
        }

        public void DoStop()
        {
            Type = EType.Stop;
        }

        public void DoPause()
        {
            Type = EType.Pause;
        }

        public void Update()
        {
            if (_eType != EType.Play)
                return;
            _curTime = Mathf.Min(_totalTime, Time.deltaTime * _speed + _curTime);
            if (_fnPercent != null)
                _fnPercent(Percent);
            if (_curTime == _totalTime)
            {
                if (_loop == true)
                    _curTime = 0f;
                else
                    Type = EType.Stop;
            }
        }
    }
}
