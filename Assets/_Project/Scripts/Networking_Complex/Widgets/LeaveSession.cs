using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.UI;

namespace VS.NetcodeExampleProject.Networking {
    public class LeaveSession : SessionActionBase {
        private void Start() {
            sessionActionButton.interactable = false;
        }

        protected override async void SessionSessionAction() {
            await SessionHandler.Instance.LeaveSessionAsync();
        }

        public override void OnSessionJoined(ISession session) {
            base.OnSessionJoined(session);
            
            sessionActionButton.interactable = true;
            sessionActionButton.onClick.AddListener(SessionSessionAction);
        }
        
        public override void OnSessionLeft() {
            base.OnSessionLeft();
            
            sessionActionButton.interactable = false;
            sessionActionButton.onClick.RemoveAllListeners();
        }
    }
}