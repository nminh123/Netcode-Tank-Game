using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

namespace Tank
{
    public class JoinNetworkUI : MonoBehaviour
    {
        private Button m_hostLobby, m_joinLobby, m_exitLobby;
        private TextMeshProUGUI m_networkStatus;

        private void Awake()
        {
            m_hostLobby = GetComponentInChildren<Button>();
            m_joinLobby = GetComponentInChildren<Button>();
            m_exitLobby = GetComponentInChildren<Button>();

            m_networkStatus = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            HostLobby();
            JoinLobby();
            ExitLobby();
        }

        private void HostLobby()
        {
            Debug.Log("<color=green>Hosting Lobby...</color>");
            SetText(Color.green, "Hosting Lobby");
            m_hostLobby.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        }

        private void JoinLobby()
        {
            Debug.Log("<color=green>Joined Lobby...</color>");
            SetText(Color.green, "Joined Lobby...");
            m_joinLobby.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        }

        private void ExitLobby()
        {
            Debug.Log("<color=red>Exit Lobby......</color>");
            SetText(Color.red, "Lobby Exited");
            m_exitLobby.onClick.AddListener(() => NetworkManager.Singleton.Shutdown());
        }

        private void SetText(Color c, string status)
        {
            m_networkStatus.color = c;
            m_networkStatus.text = status;
        }
    }
}