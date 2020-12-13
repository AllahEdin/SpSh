using UnityEngine;

namespace Assets.Scripts
{
	public class ClientMonoBehaviour : MonoBehaviour
	{
		private void Start()
		{
			AsynchronousClient.StartClient();
		}
	}
}
