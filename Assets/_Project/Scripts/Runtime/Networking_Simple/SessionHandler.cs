using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;

// Makes sure this executes before its dependers.
[DefaultExecutionOrder(-100)]
public class SessionHandler : MonoBehaviour {
    // Static singleton instance
    public static SessionHandler Instance { get; private set; }
    // Interface reference to the current session the player is in.
    public ISession ActiveSession { get; private set; }

    // Event that is fired when the player joins a session.
    public event Action<ISession> OnSessionJoined = delegate { };

    // Initializes the singleton instance.
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    // Initializes the Unity Services and signs in the player anonymously.
    // This is required before using any Unity Multiplayer features (Relay, Lobby, or Session handling),
    // since all Unity Gaming Services require an authenticated user session to function properly.
    private async void Start() {
        // Also makes sure Unity Services are initialized for everything to work.
        if (UnityServices.State != ServicesInitializationState.Initialized) {
            await UnityServices.InitializeAsync();
        }
        
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    
    // Entry point method to create a session as a host.
    public async Task CreateSessionAsHostAsync() {
        // SessionOptions object that defines the session configuration.
        SessionOptions sessionOptions = new SessionOptions {
            MaxPlayers = 8, // Maximum number of players that can join the session.
            IsLocked = false, // Whether the session is locked and can't be joined by anyone else.'
            IsPrivate = false, // Whether the session is private and only visible to the session host.
            Name = "Test Session", // Name of the session.
        }.WithRelayNetwork(); // 'WithRelay' builder method to set the network type to Relay.
        
        try {
            // Creates a session and sets the 'ActiveSession' property to the newly created session.
            ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(sessionOptions);
            OnSessionJoined.Invoke(ActiveSession);
        }
        catch (SessionException ex) {
            Debug.LogException(ex);
        }
    }

    // Entry point method to join a session by code.
    // This method is called when the player enters a session code in the UI.
    public async Task JoinSessionByCodeAsync(string sessionCode) {
        try {
            // Joins a session by code and sets the 'ActiveSession' property to the newly joined session.
            ActiveSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(sessionCode);
            OnSessionJoined.Invoke(ActiveSession);
        }
        catch (SessionException ex) {
            Debug.LogException(ex);
        }
    }
}