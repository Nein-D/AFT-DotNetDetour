using System.Net.Sockets;
using System.Text;
using System.Threading;



namespace AFT_DotNetDetour
{
	internal class AFTClient
	{
		private static TcpClient client;

		private static byte[] buffer;

		static AFTClient()
		{
			client = new TcpClient();
			buffer = null;
			client.send(Tool.randomStr(64));
		}

		public static bool send(string a, string b)
		{
			return client.send(a, b);
		}

		public static void send(string a)
		{
			client.send(a);
		}

		public static string send_Ret(string a, string b)
		{
			return client.send_Ret(a, b);
		}

		public static string recv(string a, int b)
		{
			return client.recv(a, b);
		}

		public static string toString()
		{
			return "";
		}

		public static void send(byte[] bs)
		{
			client.send(bs);
		}

		public static void listen()
		{
			while (true)
			{
				try
				{
					buffer = new byte[1024];
					Socket socket = client.getSocket();
					while (socket.Receive(buffer, SocketFlags.None) > 0)
					{
						string @string = Encoding.UTF8.GetString(buffer);
						client.send(@string);
						Thread.Sleep(50);
					}
					Thread.Sleep(1000);
				}
				catch
				{
				}
			}
		}
	}
}

