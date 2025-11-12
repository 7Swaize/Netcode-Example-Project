using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace VS.NetcodeExampleProject.Networking {
    public enum NetworkConnectionType {
        Relay,
        Direct
    }
    
    [CreateAssetMenu(fileName = "SessionConfig", menuName = "Netcode/Session Config", order = 0)]
    public class SessionConfig : ScriptableObject {
        [Header("Session Settings")]
        public int maxPlayers = 8;
        public bool isLocked;
        public bool isPrivate;
        public string sessionName;

        [Header("Network Settings")]
        public NetworkConnectionType networkConnectionType = NetworkConnectionType.Relay;
        public string listenIp = "0.0.0.0";
        public int port = 0; // may need to set it to 7777? 

        [HideInInspector] public string publishIp;
        
        private void OnEnable() {
            publishIp = GetLocalIPAddress();
        }
        
        // https://stackoverflow.com/questions/3253701/get-public-external-ip-address
        private static string GetExternalIPAddress() {
            return new WebClient().DownloadString("https://ipv4.icanhazip.com/").TrimEnd();
        }
        
        private static string GetLocalIPAddress() {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            
            return "127.0.0.1";
        }
    }
}