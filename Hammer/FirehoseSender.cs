using System;
using System.Configuration;
using System.IO;
using System.Text;
using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Newtonsoft.Json;

namespace Hammer
{
	public class FirehoseSender : IMessageSender
	{
		public class Data
		{
			public string Field { get; set; }
		}

		AmazonKinesisFirehoseClient _client;

		public FirehoseSender()
		{

			var config = new AmazonKinesisFirehoseConfig();
			config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(ConfigurationSettings.AppSettings[ConfigurationKeys.Region]);
			_client = new AmazonKinesisFirehoseClient(ConfigurationSettings.AppSettings[ConfigurationKeys.AccessKeyId], 
			                                          ConfigurationSettings.AppSettings[ConfigurationKeys.AccessKeySecret], 
			                                          config);
		}

		public void SendMessage(uint messageCount)
		{
			var messageType = messageCount % 2 == 0 ? "test-event-1" : "test-event-2";
			var message = new Message<Data>(messageType, new Data() { Field = DateTime.Now.ToString() + " - " + messageCount.ToString() }, messageCount % 10 == 0);

			PutRecordRequest record = new PutRecordRequest();
			record.Record = new Record();
			record.Record.Data = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
			record.DeliveryStreamName = ConfigurationSettings.AppSettings[ConfigurationKeys.FirehoseStreamName];

			var res = _client.PutRecord(record);
			Console.WriteLine("{0} {1}", messageCount, res.RecordId);
		}
	}
}
