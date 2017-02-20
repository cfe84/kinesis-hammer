using System;

namespace Hammer
{
	[Serializable]
	public class Message<T>
	{
		[Serializable]
		public class PropertyBag
		{
			public string EventType { get; set; }
			public bool ForwardToBus { get; set; }
		}
		public PropertyBag Properties { get; private set; }
		public T Data { get; set; }

		public Message(string eventType, T data, bool forwardToBus = false)
		{
			Properties = new PropertyBag() { EventType = eventType, ForwardToBus = forwardToBus };
			Data = data;
		}
	}
}
