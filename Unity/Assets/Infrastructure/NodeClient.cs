using UnityEngine;
using SocketIOClient;
using SocketIOClient.Messages;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class NodeClient : MonoBehaviour 
{
    public string IpAddress;
    public int Port = 5000;
    public bool Connect;
    public Client client;
    private bool _isConnected;

	void Awake ()
    {
        var ip = string.IsNullOrEmpty(IpAddress) ? LocalIPAddress() : IpAddress;
        var url = "http://" + ip + ":" + Port;
        
        client = new Client(url);


        client.Opened += client_Opened;
        client.Message += client_Message;
        client.SocketConnectionClosed += client_SocketConnectionClosed;
        client.Error += client_Error;

        Debug.Log("Client ready to connect to " + url);
	}

    void OnDestroy()
    {
        if (client.IsConnected)
        {
            client.Close();
            Debug.Log("Client turned Off");
        }
    }

    void client_Error(object sender, ErrorEventArgs e)
    {
        Debug.Log("Client Error: " + e.Message);
    }

    void client_SocketConnectionClosed(object sender, System.EventArgs e)
    {
        Debug.Log("Client Closed");
    }

    void client_Message(object sender, MessageEventArgs e)
    {
        //Debug.Log("Message Received: " + e.Message.Event);
        if (e != null && e.Message.Event == "GetDistance")
        {
            string msg = e.Message.MessageText;
            Debug.Log(msg);
        }

    }

    void client_Opened(object sender, System.EventArgs e)
    {
        Debug.Log("Client Opened");
    }
	
	void Update () 
    {
	    if (_isConnected && !Connect)
        {
            _isConnected = false;
            
            if (client.IsConnected)
            {
                client.Close();
                Debug.Log("Client Off");
            }
        }
        else if (!_isConnected && Connect)
        {
            _isConnected = true;
            client.Connect();
            Debug.Log("Client On");
        }
	}

    private string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
