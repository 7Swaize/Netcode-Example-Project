using TMPro;
using Unity.Services.Multiplayer;
using UnityEngine;

public class ToggleOnJoin : MonoBehaviour {
    // References to UI elements.
    public GameObject joinSessionByCodeObject;
    public GameObject createSessionObject;
    public GameObject joinCodeDisplayObject;
    
    public void Start() {
        SessionHandler.Instance.OnSessionJoined += OnPlayerJoinedSession;
    }

    // Hide and show UI elements when the player joins or creates a session.
    private void OnPlayerJoinedSession(ISession obj) {
        joinSessionByCodeObject.SetActive(false);
        createSessionObject.SetActive(false);
        joinCodeDisplayObject.SetActive(true);
        
        // Update the display code with the code for the newly joined session.
        joinCodeDisplayObject
            .GetComponentInChildren<TMP_Text>()
            .SetText(SessionHandler.Instance.ActiveSession.Code);
    }
    
    private void OnDestroy() {
        // Clean up listeners to avoid memory leaks
        SessionHandler.Instance.OnSessionJoined -= OnPlayerJoinedSession;
    }
}
