using TMPro;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    public void SetText(string text)
    {
        if (messageText != null)
        {
            messageText.text = text;
        }
    }
}
