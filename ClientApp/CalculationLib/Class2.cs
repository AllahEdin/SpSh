namespace CalculationLib
{
	public class StateMachine
	{
		private SourseBase _sourse;
		private StateBase[] _states;
		private StateConversion[] _conversions;

		private StateBase _curState;
		private StateBase _lastState;

		public StateMachine(SourseBase sourse)
		{
			_sourse = sourse;
		}

		public void AddStatesRange(StateBase[] states, StateConversion[] conversions)
		{
			_states = states;
			_conversions = conversions;
		}

		public void Start(StateBase initialState)
		{
			_curState =
				initialState;

			_curState.OnNewConditionEvent += OnNewCondition;

			void OnNewCondition(ConditionBase condition)
			{
				_curState.OnNewConditionEvent -= OnNewCondition;
				
				_lastState = _curState;
				
				_curState =
					_curState.GetNextState(condition);
			}
		}

	}


	public abstract class StateBase
	{
		public delegate void ConditionDelgate(ConditionBase condition);

		public event ConditionDelgate OnNewConditionEvent;

		public StateBase()
		{
			
		}

		public abstract StateBase GetNextState( ConditionBase condition);
	}


	public abstract class StateConversion
	{
		
	}

	public abstract class ConditionBase
	{

	}

}
