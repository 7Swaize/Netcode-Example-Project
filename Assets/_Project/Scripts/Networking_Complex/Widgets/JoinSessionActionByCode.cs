using TMPro;
using Unity.Services.Multiplayer;
using UnityEngine;

namespace VS.NetcodeExampleProject.Networking {
    public class JoinSessionActionByCode : SessionActionBase {
        [SerializeField] private TMP_InputField sessionJoinCodeField;
        
        private string _sessionJoinCode;

        public override void OnServicesInitialized() {
            sessionJoinCodeField.onEndEdit.AddListener(value => {
                if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(value)) {
                    SessionSessionAction();
                }
            });
            
            sessionJoinCodeField.onValueChanged.AddListener(value => {
                sessionActionButton.interactable = !string.IsNullOrEmpty(value);
            });
        }

        protected override async void SessionSessionAction() {
            _sessionJoinCode = sessionJoinCodeField.text;
            await SessionHandler.Instance.JoinSessionByCodeAsync(_sessionJoinCode);
        }

        public override void OnSessionJoined(ISession session) {
            base.OnSessionJoined(session);
            
            sessionJoinCodeField.text = string.Empty;
        }
    }
}