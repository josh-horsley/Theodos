using UnityEngine;
using SocketIOClient;
using SocketIOClient.Messages;
using System.Collections;

public class NodeClient : MonoBehaviour 
{
    public string IpAddress = "192.168.1.4";
    public int Port = 5000;
    public bool Connect;
    public Client client;
    private bool _isConnected;

	void Awake ()
    {
        var url = "http://" + IpAddress + ":" + Port;
        
        client = new Client(url);

        client.Opened += client_Opened;
        client.Message += client_Message;
        client.SocketConnectionClosed += client_SocketConnectionClosed;
        client.Error += client_Error;

        Debug.Log("Client ready to connecting to " + url);
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

    public void Send(string message)
    {
        if (client.IsConnected)
        {
            Debug.Log("Sending message: " + message);
            client.Emit("message", message);
        }
    }
}
