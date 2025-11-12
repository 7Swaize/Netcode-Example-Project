using UnityEngine;
using UnityEngine.UI;

public class CreateSession : MonoBehaviour {
    public Button createSessionButton;
    
    private void Start() {
        // When the button is clicked, create a new session.
        createSessionButton.onClick.AddListener(OnCreateButtonClicked);
    }

    // Method to handle requesting to create a new session as host.
    private async void OnCreateButtonClicked() {
        await SessionHandler.Instance.CreateSessionAsHostAsync();
    }
    
    private void OnDestroy() {
        // Clean up listeners to avoid memory leaks
        createSessionButton.onClick.RemoveListener(OnCreateButtonClicked);
    }
}
