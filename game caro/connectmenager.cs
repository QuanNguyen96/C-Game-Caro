using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace game_caro
{
    public class connectmenager
    {
        #region Client
        Socket client;
        public bool connectServer()
        {
            IPEndPoint iop = new IPEndPoint(IPAddress.Parse(IP), PORT);
            client = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            try
            {
                client.Connect(iop);
                return true;
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Sever
        Socket sever;
        public void CreateServer()
        {
            IPEndPoint iop = new IPEndPoint(IPAddress.Parse(IP), PORT);
            sever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sever.Bind(iop);
            sever.Listen(10);
            Thread acceptClient = new Thread(() =>
              {
                  client = sever.Accept();
              });
            acceptClient.IsBackground = true;
            acceptClient.Start();
        }
        #endregion
        #region Both
        
        public string IP = "192.168.1.83";
        public int PORT = 9999;
        public const int BUFFER = 1024;
        public bool isServer = true;
        public bool Send(object data)
        {
            byte[] senData = SerializeData(data);
            return SenData(client, senData);
        }
        public object Receive()
        {
            byte[] receiveData = new byte[BUFFER];
            bool isOk = ReceiveData(client, receiveData);
            return DeserializeData(receiveData);
        }
        private bool SenData(Socket target, byte[] data)
        {
            return target.Send(data) == 1 ? true : false;
        }
        private bool ReceiveData(Socket target,byte[] data)
        {
            return target.Receive(data) == 1 ? true : false;
        }
        public byte[] SerializeData(object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }
        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach(UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if(ip.Address.AddressFamily==AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        #endregion
    }
}
