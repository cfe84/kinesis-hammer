using System;
using System.Threading;

namespace Hammer
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var sender = new FirehoseSender();
			var hammer = new Hammer(16, sender);
			hammer.Start();
			Thread.Sleep(60000);
			hammer.Stop();
			Console.ReadLine();
		}
	}
}
