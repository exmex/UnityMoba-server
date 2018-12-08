using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MobaServer
{
    public class StateObject
    {
        public Socket WorkSocket = null;
        public byte[] Buffer;
        public TGNetService.Header Header;
    }

    internal class Program
    {
        private static List<Client> _clients = new List<Client>();
        public static TGNetService.RawSerializer<TGNetService.Header> RawSerializer;

        public static void Main(string[] args)
        {
            //25001
            RawSerializer = new TGNetService.RawSerializer<TGNetService.Header>();

            AddAllMethodsFromType(Assembly.GetEntryAssembly().GetTypes());
            //i += AddAllMethodsFromType(Assembly.GetExecutingAssembly().GetTypes());

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, 25001));
            listener.Listen(100);
            listener.BeginAccept(AcceptCallback, listener);

            while (true)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Contains all parsers and their function
        /// </summary>
        public static readonly Dictionary<string, Action<Packet>> Parsers = new Dictionary<string, Action<Packet>>();

        private static void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.  
            var listener = (Socket) ar.AsyncState;
            var handler = listener.EndAccept(ar);

            Console.WriteLine($"Accepted client");
            _clients.Add(new Client(handler));

            listener.BeginAccept(AcceptCallback, listener);
        }

        private static void SetParser(string id, Action<Packet> parser)
        {
            Parsers[id] = parser;
            Console.WriteLine($"Added parser for packet {id}");
        }

        private static int AddAllMethodsFromType(IEnumerable<Type> types)
        {
            var i = 0;
            foreach (var type in types)
            foreach (var method in type.GetMethods())
            foreach (var boxedAttrib in method.GetCustomAttributes(typeof(PacketAttribute), false))
            {
                if (!(boxedAttrib is PacketAttribute attrib)) continue;

                var id = attrib.Id;
                var parser = (Action<Packet>) Delegate.CreateDelegate(typeof(Action<Packet>), method);

                SetParser(id, parser);

#if DEBUG
                i++;
#endif
            }

            return i;
        }
    }
}