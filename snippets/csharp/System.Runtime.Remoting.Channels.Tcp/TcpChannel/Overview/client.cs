﻿//<snippet10>
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Security.Permissions;

public class Client
{
[SecurityPermission(SecurityAction.Demand)]
    public static void Main(string[] args)
    {
        //<snippet11>
        // Create the channel.
        TcpChannel clientChannel = new TcpChannel();
        //</snippet11>

        // Register the channel.
        ChannelServices.RegisterChannel(clientChannel, false);

        // Register as client for remote object.
        WellKnownClientTypeEntry remoteType = new WellKnownClientTypeEntry(
            typeof(RemoteObject),"tcp://localhost:9090/RemoteObject.rem");
        RemotingConfiguration.RegisterWellKnownClientType(remoteType);

        //<snippet12>
        // Create a message sink.
        string objectUri;
        System.Runtime.Remoting.Messaging.IMessageSink messageSink =
            clientChannel.CreateMessageSink(
                "tcp://localhost:9090/RemoteObject.rem", null,
                out objectUri);
        Console.WriteLine("The URI of the message sink is {0}.",
            objectUri);
        if (messageSink != null)
        {
            Console.WriteLine("The type of the message sink is {0}.",
                messageSink.GetType().ToString());
        }
        //</snippet12>

        // Create an instance of the remote object.
        RemoteObject service = new RemoteObject();

        // Invoke a method on the remote object.
        Console.WriteLine("The client is invoking the remote object.");
        Console.WriteLine("The remote object has been called {0} times.",
            service.GetCount());
    }
}
//</snippet10>
