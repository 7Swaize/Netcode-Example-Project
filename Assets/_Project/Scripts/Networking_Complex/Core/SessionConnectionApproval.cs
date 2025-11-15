using Unity.Netcode;
using UnityEngine;

namespace VS.NetcodeExampleProject.Networking {
    public class SessionConnectionApproval : MonoBehaviour {
        [SerializeField] private SessionConfig sessionConfig;
        
        private void Start() {
            NetworkManager.Singleton.ConnectionApprovalCallback += ConnectionApprovalWithSpawnPosition;
        }

        // must enable 'Connection Approval' in the NetworkManager
        private void ConnectionApprovalWithSpawnPosition(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response) {
            response.CreatePlayerObject = true;
            response.Position = sessionConfig.onClientConnectedPosition;
            response.Rotation = sessionConfig.onClientConnectedRotation;
            response.Approved = true; // connection is always approved
        }
    }
}