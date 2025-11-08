using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinSessionByCode : MonoBehaviour {
    public TMP_InputField sessionJoinCodeField;
    public Button enterSessionButton;

    // Cached join code to the session.
    private string _sessionJoinCode;

    private void Start() {
        sessionJoinCodeField.onEndEdit.AddListener(OnEndEdit);
        // When the button is clicked, join the session.
        enterSessionButton.onClick.AddListener(OnJoinButtonClicked);
    }

    private async void OnJoinButtonClicked() {
        _sessionJoinCode = sessionJoinCodeField.text.Trim();

        if (!string.IsNullOrEmpty(_sessionJoinCode)) {
            await EnterSession();
        }
    }

    private async void OnEndEdit(string value) {
        _sessionJoinCode = value.Trim();

        // Optional: Pressing 'Enter' also triggers join
        if (!string.IsNullOrEmpty(_sessionJoinCode) && Input.GetKeyDown(KeyCode.Return)) {
            await EnterSession();
        }
    }

    // Method to handle requesting to join a session by join code.
    private async Task EnterSession() {
        _sessionJoinCode = sessionJoinCodeField.text.Trim();
        await SessionHandler.Instance.JoinSessionByCodeAsync(_sessionJoinCode);
    }
    
    private void OnDestroy() {
        // Clean up listeners to avoid memory leaks
        sessionJoinCodeField.onEndEdit.RemoveListener(OnEndEdit);
        enterSessionButton.onClick.RemoveListener(OnJoinButtonClicked);
    }
}