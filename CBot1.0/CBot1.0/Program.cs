using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;
namespace irc
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

    public class IrcCommand
    {
        Client client = new Client();

        public string sender()
        {
            string raw = client.read();
            string[] rawsp = raw.Split(new char[':'], 2); // ':', "user!ident@host etc"
            string usermask = rawsp[1];
            string[] user = usermask.Split(new char['!'], 2);// "user" "!ident@host etc"
            string sender1 = user[0];
            return sender1;
        }

        public string command()
        {
            string raw = client.read();
            string[] rawsp = raw.Split(new char[':']); // ':', "user!ident@host" "command params"
            if (rawsp.Length < 2)
            {
                string msg = rawsp[2];
                string[] commandpar = msg.Split(new char[' '], 2); // "command" "par par par etc"
                string command1 = commandpar[0];
                return command1;
            }
            else
            {
                return null;
            }
        }

        public string[] parameters()
        {
            string raw = client.read();
            string[] rawsp = raw.Split(new char[':']);// ':', "user!ident@host" "cmd cmd cmd..."
            if (rawsp.Length < 2)
            {
                string msg = rawsp[2];
                string[] commandpar = msg.Split(new char[' '], 2); // "cmd" "params params params"
                string par = commandpar[1];
                string[] parameters1 = par.Split(new char[' ']); // "params" "params" "params" ...
                return parameters1;
            }
            else
            {
                return null;
            }
        }

        public string channel()
        {
            string raw = client.read();
            string[] rawsp = raw.Split(new char[':']);
            if (rawsp.Length < 2)
            {
                string ircraw = rawsp[1];
                string[] r1 = ircraw.Split(new char[' ']); //
                if (r1.Length < 2)
                {
                    string channel1 = r1[2];
                    channel1.Replace(':', ' ');
                    return channel1;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public string pong()
        {
            return client.read().Split(new char[':'])[1].Split(new char[' '], 2)[1];
        }
    }
}