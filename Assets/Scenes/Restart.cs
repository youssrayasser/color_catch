using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Required for Button

public class Restart : MonoBehaviour
{
    public Button restartButton; // Reference to the Button in the scene

    // Start is called before the first frame update
    void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);  // Assign the RestartGame function to the button's OnClick event
        }
    }

    // Method to restart the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
