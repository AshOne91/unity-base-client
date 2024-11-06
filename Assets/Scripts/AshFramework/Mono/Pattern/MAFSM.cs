using System.Collections.Generic;

using ASHFramework.Mono.Diagnostics;

namespace ASHFramework.Mono.Pattern
{
    public interface MAStateInterface<TStateID> : MAUpdatableInterface
    {
        void OnEnter(TStateID prevStateID);
        void OnLeave(TStateID nestStateID);
    }
    public class MAStateFunctor<TStateID> : MAStateInterface<TStateID>
    {
        public delegate void FNStateCallback(TStateID stateID);
        public delegate void FNStateHandler();

        private FNStateCallback _onEnter = null;
        private FNStateCallback _onLeave = null;
        private FNStateHandler _doUpdate = null;

        public MAStateFunctor(FNStateCallback onEnter, FNStateCallback onLeave, FNStateHandler doUpdate)
        {
            Set(onEnter, onLeave, doUpdate);
        }

        public void Set(FNStateCallback onEnter, FNStateCallback onLeave, FNStateHandler doUpdate)
        {
            _onEnter = onEnter;
            _onLeave = onLeave;
            _doUpdate = doUpdate;
        }

        public void OnEnter(TStateID prevStateID)
        {
            if (_onEnter != null)
            {
                _onEnter(prevStateID);
            }
        }

        public void OnLeave(TStateID nextStateID)
        {
            if (_onLeave != null)
            {
                _onLeave(nextStateID);
            }
        }

        public void DoUpdate()
        {
            if (_doUpdate != null)
            {
                _doUpdate();
            }
        }
    }

    public class MAFSM<TStateID> : MAObject
    {
        protected class StatePair : MAObject
        {
            public TStateID StateID;
            public MAStateInterface<TStateID> State;

            public StatePair(TStateID stateID, MAStateInterface<TStateID> state)
            {
                StateID = stateID;
                State = state;
            }
        }

        protected Dictionary<TStateID, StatePair> _statePairsDic = new Dictionary<TStateID, StatePair>();

        protected StatePair _currentStatePair = null;
        protected StatePair _nextStatePair = null;

        protected bool _needDefaultStateOnEnter = false;
        protected bool _initialized = false;

        public bool Initialized
        {
            get { return _initialized; }
        }

        public void Init()
        {
            _initialized = true;
            _needDefaultStateOnEnter = false;
        }

        public void DeInit()
        {
            if (_initialized == false)
            {
                return;
            }

            _currentStatePair.State.OnLeave(_currentStatePair.StateID);
            _currentStatePair = null;
            _nextStatePair = null;

            _statePairsDic.Clear();

            _needDefaultStateOnEnter = false;
            _initialized = false;
        }

        public bool RegisterState(TStateID stateID, MAStateInterface<TStateID> state)
        {
            if (_initialized == false)
            {
                return false;
            }

            if (_statePairsDic.ContainsKey(stateID) == true) 
            {
                return false;
            }

            StatePair statePair = new StatePair(stateID, state);
            _statePairsDic.Add(stateID, statePair);

            if (_statePairsDic.Count == 1)
            {
                _currentStatePair = statePair;
                _needDefaultStateOnEnter = true;
            }

            return true;
        }

        public MAStateInterface<TStateID> GetState(TStateID stateID)
        {
            StatePair statePair = null;
            if (_statePairsDic.TryGetValue(stateID, out statePair) == false)
            {
                return null;
            }
            return statePair.State;
        }

        public TStateID GetCurrentStateID()
        {
            MADebug.Assert(_currentStatePair != null, "_currentStatePair == null");
            return _currentStatePair.StateID;
        }

        public MAStateInterface<TStateID> GetCurrentState()
        {
            MADebug.Assert(_currentStatePair != null, "_currentStatePair == null");
            return _currentStatePair.State;
        }

        public void ChangeState(TStateID nextStateID)
        {
            if (nextStateID.Equals(GetCurrentStateID()) == true)
                return;
            _nextStatePair = null;
            _statePairsDic.TryGetValue(nextStateID, out _nextStatePair);
        }

        public void DoUpdate()
        {
            if (_needDefaultStateOnEnter == true)
            {
                _needDefaultStateOnEnter = false;
                // 기본 상태의 OnEnter에는 자기 자신의 StateID를 넘겨준다.
                _currentStatePair.State.OnEnter(_currentStatePair.StateID);
            }

            if (_nextStatePair != null)
            {
                StatePair nextStatePair = _nextStatePair;
                _nextStatePair = null;

                if (nextStatePair != _currentStatePair)
                {
                    _currentStatePair.State.OnLeave(nextStatePair.StateID);
                    nextStatePair.State.OnEnter(_currentStatePair.StateID);

                    _currentStatePair = nextStatePair;
                }
                else
                {
                    MADebug.Write("Can't switch to duplicate state: " + nextStatePair._stateID);
                }
            }

            if (_currentStatePair != null)
                _currentStatePair.State.DoUpdate();
        }
    }
}
