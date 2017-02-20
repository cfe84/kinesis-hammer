using System;
using System.Configuration;
using System.IO;
using System.Text;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Newtonsoft.Json;

namespace Hammer
{
	public class KinesisSender: IMessageSender
	{
		public class Data
		{
			public string Field { get; set; }
		}

		AmazonKinesisClient _client;
		string _streamName = null;

		public AmazonKinesisClient GetClient()
		{
			var config = new AmazonKinesisConfig();
			config.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(ConfigurationSettings.AppSettings[ConfigurationKeys.Region]);
			_streamName = ConfigurationSettings.AppSettings[ConfigurationKeys.KinesisStreamName];
			return new AmazonKinesisClient(ConfigurationSettings.AppSettings[ConfigurationKeys.AccessKeyId],
										   ConfigurationSettings.AppSettings[ConfigurationKeys.AccessKeySecret],
										   config);
		}

		public KinesisSender()
		{
			_client = GetClient();
		}

		public void SendMessage(uint messageCount)
		{
			var messageType = messageCount % 2 == 0 ? "test-event-1" : "test-event-2";
			var message = new Message<Data>(messageType, new Data() { Field = DateTime.Now.ToString() + " - " +  messageCount.ToString() }, messageCount % 10 == 0);

			PutRecordRequest record = new PutRecordRequest();
			record.PartitionKey = messageCount.ToString();
			record.Data = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
			record.StreamName = _streamName;

 			var res = _client.PutRecord(record);
			Console.WriteLine("{0} {1}", messageCount, res.ShardId);
		}
	}
}
