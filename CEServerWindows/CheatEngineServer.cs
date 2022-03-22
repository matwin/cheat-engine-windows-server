﻿using CEServerWindows.CheatEnginePackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CEServerWindows.CheatEnginePackets.C2S;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace CEServerWindows
{
    public class CheatEngineServer : IDisposable
    {
        private TcpListener _tcpListener;
        private PacketManager packetManager;
        private CancellationTokenSource _tokenSource;
        private bool _listening;
        private CancellationToken _token;

        public int Port { get; }


        public CheatEngineServer(ushort port = 52736) : this(port, new PacketManager())
        {
            this.RegisterDefaultHandlers();
        }

        public CheatEngineServer(PacketManager pm) : this(52736, pm)
        {

        }

        public CheatEngineServer(ushort port, PacketManager pm)
        {
            _tcpListener = new TcpListener(IPAddress.Any, port);
            this.packetManager = pm;
            Port = port;
        }

        private void HandleReceivedClient(TcpClient client)
        {
            var clientStream = client.GetStream();
            var reader = new BinaryReader(clientStream);
            var writer = new BinaryWriter(clientStream);
            while (true)
            {
                try
                {
                    if (clientStream.DataAvailable)
                    {
                        var command = this.packetManager.ReadNextCommand(reader);
                        var output = this.packetManager.ProcessAndGetBytes(command);
                        /* if(command.CommandType != CommandType.CMD_READPROCESSMEMORY)
                             Console.WriteLine(BitConverter.ToString(output).Replace("-", ""));*/
                      // Console.WriteLine("{0} returned {1} bytes", command.CommandType, output.Length);
                        writer.Write(output);
                        writer.Flush();
                        command.HandleAfterWrite(client);
                        //   Handle(stream, writer, cmd);
                    }
                }
                catch(EndOfStreamException)
                {
                    client.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + ": "+  e.Message);
                    Console.WriteLine(e.StackTrace);
                    client.Close();
                    break;
                }
            }
        }

        public async Task StartAsync(CancellationToken? token = null)
        {
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token ?? new CancellationToken());
            _token = _tokenSource.Token;
            _tcpListener.Start();
            _listening = true;

            try
            {
                while (!_token.IsCancellationRequested)
                {
                    var tcpClientTask = _tcpListener.AcceptTcpClientAsync();
                    var result = await tcpClientTask;
                    Console.WriteLine("New client");
                    _ = Task.Run(() =>
                      {
                          HandleReceivedClient(result);
                      }, _token);
                }
            }
            finally
            {
                _tcpListener.Stop();
                _listening = false;
            }
        }

        public void Stop()
        {
            _tokenSource?.Cancel();
        }

        public void Dispose()
        {
            Stop();
        }

        private void RegisterDefaultHandlers()
        {
            this.RegisterCommandHandler(new CreateToolHelp32SnapshotCommand());
            this.RegisterCommandHandler(new GetVersionCommand());
            this.RegisterCommandHandler(new Module32FirstCommand());
            this.RegisterCommandHandler(new Module32NextCommand());
            this.RegisterCommandHandler(new Process32FirstCommand());
            this.RegisterCommandHandler(new Process32NextCommand());
            this.RegisterCommandHandler(new CloseHandleCommand());
            this.RegisterCommandHandler(new OpenProcessCommand());
            this.RegisterCommandHandler(new GetArchitectureCommand());
            this.RegisterCommandHandler(new VirtualQueryExCommand());
            this.RegisterCommandHandler(new VirtualQueryExFullCommand());
            this.RegisterCommandHandler(new ReadProcessMemoryCommand());
            this.RegisterCommandHandler(new GetSymbolsFromFileCommand());
            this.RegisterCommandHandler(new AbiCommand());
        }

        public void RegisterCommandHandler(ICheatEngineCommand command)
        {
            this.packetManager.RegisterCommand(command);
        }

    }
}