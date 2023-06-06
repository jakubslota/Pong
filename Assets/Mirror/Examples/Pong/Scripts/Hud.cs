using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;
using Mirror;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    NetworkManager manager;
    public InputField ip_inputField;
    public GameObject HostConnect1;
    public GameObject HostConnect2;
    public GameObject HostConnect3;
    public GameObject HostConnect4;
    public GameObject DiscHost;
    public GameObject DiscClient;
    public Text text;
        public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
    void HideB()
    {
        text.text = GetLocalIPAddress();
        HostConnect1.SetActive(false);
        HostConnect2.SetActive(false);
        HostConnect3.SetActive(false);
        HostConnect4.SetActive(false);
    }
    void ShowB()
    {
        text.text = " ";
        HostConnect1.SetActive(true);
        HostConnect2.SetActive(true);
        HostConnect3.SetActive(true);
        HostConnect4.SetActive(true);
    }
    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    public void HostFunction()
    {
        manager.StartHost();
        HideB();
        DiscHost.SetActive(true);
    }
    public void ConnectFuction()
    {
        manager.networkAddress = ip_inputField.text;
        manager.StartClient();
        HideB();
        DiscClient.SetActive(true);
    }
    public void HostDisconnect()
    {
        manager.StopHost();
        DiscHost.SetActive(false);
        ShowB();
    }
    public void ClientDisconnect()
    {
        manager.StopClient();
        DiscClient.SetActive(false);
        ShowB();
        Text p1 = GameObject.Find("Canvas/Player Score").GetComponent<Text>();
        p1.text = "0";
        Text p2 = GameObject.Find("Canvas/Player Score2").GetComponent<Text>();
        p2.text = "0";
        Text EndP1 = GameObject.Find("Canvas/EndP1").GetComponent<Text>();
        EndP1.text = "";
        Text EndP2 = GameObject.Find("Canvas/EndP2").GetComponent<Text>();
        EndP2.text = "";
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
