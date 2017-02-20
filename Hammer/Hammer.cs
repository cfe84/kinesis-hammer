using System.Collections.Generic;
using System.Threading;

namespace Hammer
{
	public class Hammer
	{
		public uint MessageCount { get; private set; } = 0;
		private bool _hammering = false;
		private List<Thread> _pool = new List<Thread>();
		private IMessageSender _sender;

		public Hammer(int threadCount, IMessageSender sender)
		{
			_sender = sender;
			for (int i = 0; i < threadCount; i++)
				_pool.Add(new Thread(send));
		}

		public void Start()
		{
			_hammering = true;
			foreach (Thread t in _pool)
				t.Start();
		}

		public void Stop()
		{
			_hammering = false;
		}

		private void send()
		{
			while (_hammering)
				_sender.SendMessage(MessageCount++);
		}
	}
}
