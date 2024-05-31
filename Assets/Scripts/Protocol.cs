using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Protocol : MonoBehaviour
{
    public static Protocol instance;
    public string ip, port, data;

    private UdpClient client;
    private IPEndPoint serverEndPoint;

    private void Awake() 
    {
        instance = this;
        ConnectToServer();
    }

    private void Start() 
    {
        
    }

    [ContextMenu("ConnectServer")]
    public void ConnectToServer()
    {
        try
        {
            client = new UdpClient();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));

            Debug.Log("Connected to the server!");
        }
        catch (Exception ex)
        {
            Debug.Log("Error while connecting to the server: " + ex.Message);
        }
    }

    [ContextMenu("Send")]
    public void SendDataToServer()
    {
        try
        {
            if (client == null)
            {
                Debug.Log("Not connected to the server. Call ConnectToServer() first.");
                return;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(data);
            client.Send(bytes, bytes.Length, serverEndPoint);
            Debug.Log("Sent data to the server: " + data);
        }
        catch (Exception ex)
        {
            Debug.Log("Error while sending data to the server: " + ex.Message);
        }
    }

    public void SendDataToServer(string msg)
    {
        try
        {
            if (client == null)
            {
                Debug.Log("Not connected to the server. Call ConnectToServer() first.");
                return;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(msg);
            client.Send(bytes, bytes.Length, serverEndPoint);
            Debug.Log("Sent data to the server: " + msg);
        }
        catch (Exception ex)
        {
            Debug.Log("Error while sending data to the server: " + ex.Message);
        }
    }

    public void CloseConnection()
    {
        if (client != null)
            client.Close();

        Debug.Log("Connection closed.");
    }
}
