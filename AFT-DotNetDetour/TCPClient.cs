using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AFT_DotNetDetour
{
	internal class TcpClient
	{
		private Socket socket;

		public Socket getSocket()
		{
			connect();
			return socket;
		}

		private bool connect()
		{
			try
			{
				if (socket == null)
				{
					socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				}
				int num = 0;
				while (!socket.Connected)
				{
					try
					{
						socket.Connect("127.0.0.1", 51787 + num);
					}
					catch (Exception)
					{
					}
					if (socket != null)
					{
						_ = socket.Connected;
					}
					num++;
				}
			}
			catch
			{
				return false;
			}
			return socket.Connected;
		}

		public bool send(byte[] bytes)
		{
			try
			{
				if (connect())
				{
					socket.Blocking = false;
					socket.SendTimeout = 5000;
					try
					{
						bytes = Tool.Base64Encrypt(bytes);
						socket.Send(bytes, bytes.Length, SocketFlags.None);
					}
					catch (Exception)
					{
						try
						{
							socket.Send(bytes, bytes.Length, SocketFlags.None);
						}
						catch (Exception)
						{
							return false;
						}
					}
					socket.Shutdown(SocketShutdown.Send);
					socket.Disconnect(reuseSocket: false);
					socket.Close();
					socket = null;
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		public bool send(string str)
		{
			try
			{
				return str == null || str.Length == 0 || send(Encoding.Default.GetBytes(str));
			}
			catch
			{
				return false;
			}
		}

		public bool send(string a, string b)
		{
			if ((a != null && a.Length != 0) || (b != null && b.Length != 0))
			{
				return send("|" + a + "|" + b);
			}
			return false;
		}

		public string send_Ret(string a, string b)
		{
			try
			{
				if (a != null && a.Length != 0)
				{
					send("RETN " + a, b);
					string path = Tool.getMainPath() + "\\" + a + ".cach";
					if (File.Exists(path))
					{
						File.Delete(path);
					}
					return recv(path, 0);
				}
				return "";
			}
			catch
			{
				return "";
			}
		}

		public string recv(string path, int outTime = 15)
		{
			int num = 0;
			int num2 = 100;
			send("needRecvFile:", path);
			while (!File.Exists(path))
			{
				if (outTime > 0)
				{
					num += num2;
				}
				Thread.Sleep(num2);
				if (outTime * 1000 < num)
				{
					send("RecvTimeOut", path);
					break;
				}
			}
			try
			{
				StreamReader streamReader = new StreamReader(path, Encoding.UTF8);
				string text = "";
				string text2;
				while ((text2 = streamReader.ReadLine()) != null)
				{
					text += text2;
				}
				streamReader.Close();
				send("Recv", text);
				File.Delete(path);
				return text;
			}
			catch (Exception)
			{
				return "";
			}
		}
	}
}
