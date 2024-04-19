using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Singleton;

    [SerializeField] private ChatMessage chatMessagePrefab;
    [SerializeField] private Transform chatContent;
    [SerializeField] private TMP_InputField chatInput;

    public string playerName = "Player";
    public string chatSceneName = "ChatUIScene";
    private bool isChatSceneLoaded = true; // Initial state with the scene loaded
    private bool isProcessingSceneChange = false; // To prevent overlapping scene load/unload calls

    private void Awake()
    {
        // Ensure this GameObject persists across scene changes
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Singleton != this)
        {
            Destroy(gameObject);
        }

        if (chatInput != null)
        {
            chatInput.onEndEdit.AddListener(delegate { TryToSendChatMessage(); });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !chatInput.isFocused && !isProcessingSceneChange)
        {
            // Toggle the state of the chat scene
            if (isChatSceneLoaded)
            {
                StartCoroutine(UnloadChatScene());
            }
            else
            {
                StartCoroutine(LoadChatScene());
            }
        }
    }

    public void TryToSendChatMessage()
    {
        if (!string.IsNullOrWhiteSpace(chatInput.text))
        {
            string message = chatInput.text;
            if (message == "!help")
            {
                AddMessage("Type !commands for a list of commands. Use !info for game details.");
            }
            else if (message == "!info")
            {
                AddMessage("This is a multiplayer game. Enjoy chatting and interacting!");
            }
            else
            {
                message = FormatMessage(message, playerName);
                AddMessage(message);
            }
            chatInput.text = "";
            if (isChatSceneLoaded)
            {
                chatInput.ActivateInputField();
            }
        }
    }

    private string FormatMessage(string message, string sender)
    {
        return $"{sender}: {message}";
    }

    private void AddMessage(string msg)
    {
        ChatMessage newMessage = Instantiate(chatMessagePrefab, chatContent);
        newMessage.SetText(msg);
    }

    private IEnumerator LoadChatScene()
    {
        isProcessingSceneChange = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(chatSceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        isChatSceneLoaded = true;
        isProcessingSceneChange = false;
    }

    private IEnumerator UnloadChatScene()
    {
        isProcessingSceneChange = true;
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(chatSceneName);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
        isChatSceneLoaded = false;
        isProcessingSceneChange = false;
    }
}
