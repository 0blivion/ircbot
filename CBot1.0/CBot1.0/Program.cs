//Oblivion sei gay.

﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;
namespace IRC
{
    public class Client
    {
        private TcpClient client;
        StreamWriter writer;
        StreamReader reader;
        private string server;
        private ushort port;

        public Client(string server = "irc.azzurra.org", ushort port = 6667)
        {
            this.port = port;
            this.server = server;
            client = new TcpClient();
            var stream = client.GetStream();
            writer = new StreamWriter(stream, System.Text.Encoding.GetEncoding("UTF-8"));
            reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("UTF-8"));
        }

        private void connect()
        {
            client.Connect(server, port);
        }

        private void send(string Msg)
        {
            writer.WriteLine(Msg);
            writer.Flush();
        }

        public string read()
        {
            return reader.ReadLine();
        }
    }

}