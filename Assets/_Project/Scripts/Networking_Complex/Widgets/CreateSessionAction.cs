using TMPro;
using Unity.Services.Multiplayer;
using UnityEngine;

namespace VS.NetcodeExampleProject.Networking {
    public class CreateSessionAction : SessionActionBase {
        [SerializeField] private SessionConfig sessionConfig;
        [SerializeField] private TMP_InputField sessionNameField;

        public override void OnServicesInitialized() {
            sessionNameField.onEndEdit.AddListener(value => {
                if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(value)) {
                    SessionSessionAction();
                }
            });
            
            sessionNameField.onValueChanged.AddListener(value => {
                sessionActionButton.interactable = !string.IsNullOrEmpty(value);
            });
        }
        
        protected override async void SessionSessionAction() {
            sessionConfig.sessionName = sessionNameField.text;
            await SessionHandler.Instance.CreateSessionAsHostAsync(sessionConfig);
        }
        
        public override void OnSessionJoined(ISession session) {
            sessionNameField.text = string.Empty;
        }
    }
}