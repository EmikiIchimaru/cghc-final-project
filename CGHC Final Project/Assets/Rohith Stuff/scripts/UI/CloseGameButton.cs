using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CloseGameButton : MonoBehaviour
{
    // Reference to the button UI component
    public UnityEngine.UI.Button closeButton;

    void Start()
    {
        // Add a listener to the button click event
        closeButton.onClick.AddListener(CloseGame);
    }

    // Function to close the game
    void CloseGame()
    {
        // Close the application (works in standalone builds)
        Application.Quit();

        // For the Unity Editor, you can use the UnityEditor namespace to stop playing the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
