//  This file is part of WindowsMemoryAccessTraps.
//  Copyright (C) Google LLC 2021
//
//  WindowsMemoryAccessTraps is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  WindowsMemoryAccessTraps is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with WindowsMemoryAccessTraps.  If not, see <http://www.gnu.org/licenses/>.

using SMBLibrary;
using SMBLibrary.Adapters;
using SMBLibrary.Authentication.GSSAPI;
using SMBLibrary.Authentication.NTLM;
using SMBLibrary.Server;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace SMBServerTrap;

class Program
{
    public static AutoResetEvent ContinueEvent = new(false);

    static void ListIpAddresses()
    {
        foreach (var intf in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (intf.OperationalStatus != OperationalStatus.Up)
                continue;
            foreach (var addr in intf.GetIPProperties().UnicastAddresses)
            {
                Console.WriteLine(addr.Address);
            }
        }
    }

    class FakeSMBServer : SMBServer
    {
        public FakeSMBServer(SMBShareCollection shares, GSSProvider securityProvider) 
            : base(shares, securityProvider)
        {
        }

        public void Start(int port)
        {
            Start(IPAddress.Any, SMBTransportType.DirectTCPTransport, port, true, true, true, null);
        }
    }

    static void Main(string[] args)
    {
        try
        {
            int port = args.Length > 0 ? int.Parse(args[0]) : SMBServer.DirectTCPPort;

            ListIpAddresses();
            var shares = new SMBShareCollection
            {
                new("root", new NTFileSystemAdapter(new FakeFileSystem()), CachingPolicy.NoCaching)
            };
            var auth = new IndependentNTLMAuthenticationProvider(u => u == "guest" ? "password" : null);
            GSSProvider securityProvider = new(auth);
            var server = new FakeSMBServer(shares, securityProvider);
            server.LogEntryAdded += (s, e) => Console.WriteLine(e.Message);

            try
            {
                server.Start(port);
                Console.WriteLine("Server started.");
                string line = Console.ReadLine()?.Trim();
                while (line != null)
                {
                    line = line.ToLower();
                    if (line == "x")
                        break;
                    if (line == "c")
                        ContinueEvent.Set();
                    line = Console.ReadLine()?.Trim();
                }
            }
            finally
            {
                server.Stop();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
