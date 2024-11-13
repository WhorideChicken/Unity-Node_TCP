using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUICanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField _ip;
    [SerializeField] private TMP_InputField _port;
    [SerializeField] private Button _connect;

    private async void Awake()
    {
        _connect.onClick.AddListener(OnClcikConnectButton);
    }

    private void OnClcikConnectButton()
    {
        string ip = _ip.text;
        string port = _port.text;
        NetworkManager.Instance.StartConnect(ip, port);
    }
}
