using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

namespace Tank
{
    public class JoinNetworkUI : MonoBehaviour
    {
        [SerializeField] private Button m_hostLobby, m_joinLobby, m_exitLobby;
        [SerializeField] private TextMeshProUGUI m_networkStatus;

        private void Start()
        {
            SetText(Color.red, "Lobby Exited");

            HostLobby();
            JoinLobby();
            ExitLobby();
        }

        private void HostLobby()
        {
            m_hostLobby.onClick.AddListener(() =>
            {
                Debug.Log("<color=green>Hosting Lobby...</color>");
                SetText(Color.green, "Hosting Lobby");
                NetworkManager.Singleton.StartHost();
            });
        }

        private void JoinLobby()
        {
            m_joinLobby.onClick.AddListener(() =>
            {
                Debug.Log("<color=green>Joined Lobby...</color>");
                SetText(Color.green, "Joined Lobby");
                NetworkManager.Singleton.StartClient();
            });
        }

        private void ExitLobby()
        {
            m_exitLobby.onClick.AddListener(() =>
            {
                Debug.Log("<color=red>Exit Lobby......</color>");
                SetText(Color.red, "Lobby Exited");
                NetworkManager.Singleton.Shutdown();
            });
        }

        private void SetText(Color c, string status)
        {
            m_networkStatus.color = c;
            m_networkStatus.text = status;
        }
    }
}