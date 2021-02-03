using System.Collections.Concurrent;

namespace ClientApp.States
{
	public delegate void StateCompletedDelegate();


	//public class StateManager
	//{
	//	private bool _initialized = false;

	//	private StateBase _stateBase;

	//	private ConcurrentQueue<StateBase> _queue =
	//		new ConcurrentQueue<StateBase>();


	//	private void StateCompleted()
	//	{
	//		_stateBase.StateCompletedEvent -= StateCompleted;

	//		if (_queue.TryDequeue(out var nextState))
	//		{
	//			_stateBase =
	//				nextState;

	//			_stateBase.Start();
	//			_stateBase.StateCompletedEvent += StateCompleted;
	//		}
	//	}

	//	public void AddToQueue(StateBase state)
	//	{
	//		_queue.Enqueue(state);

	//		if (!_initialized)
	//		{
	//			if (_queue.TryDequeue(out var nextState))
	//			{
	//				_initialized = true;
	//				_stateBase =
	//					nextState;

	//				_stateBase.StateCompletedEvent += StateCompleted;
	//				_stateBase.Start();
	//			}
	//		}
	//	}

	//}
}
