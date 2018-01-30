namespace Utils.EventManager
{
	public class Publisher
	{
		public static int Publish(string topic) => Deliver.Instance.Publish(topic, new object[] { });

		public static int Publish<T1>(string topic, T1 arg1) => Deliver.Instance.Publish(topic, new object[] {arg1});

		public static int Publish<T1, T2>(string topic, T1 arg1, T2 arg2) => Deliver.Instance.Publish(topic, new object[] {arg1, arg2});

		public static int Publish<T1, T2, T3>(string topic, T1 arg1, T2 arg2, T3 arg3) => Deliver.Instance.Publish(topic, new object[] {arg1, arg2, arg3});
	}
}
