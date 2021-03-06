﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            byte[] data = new byte[1024];
            string input, stringData;
            string ipstring = "127.0.0.1";

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ipstring), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);

            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");
                Console.WriteLine(e.ToString());
                return;
            }

            int recv = server.Receive(data);

            stringData = Encoding.UTF8.GetString(data, 0, recv);

            Console.WriteLine(stringData);

            while (true)
            {
                input = Console.ReadLine();

                if (input == "exit")

                    break;

                Console.WriteLine("You: " + input);
                server.Send(Encoding.UTF8.GetBytes(input));
                data = new byte[1024];
                recv = server.Receive(data);
                stringData = Encoding.UTF8.GetString(data, 0, recv);
                byte[] utf8string = System.Text.Encoding.UTF8.GetBytes(stringData);
                Console.WriteLine("Server: " + stringData);
            }

            Console.WriteLine("Disconnecting from server...");
            server.Close();
            Console.WriteLine("Disconnected!");
            Console.ReadLine();
        }
    }
}