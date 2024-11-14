using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int score = 0;                     // Player's score
    public TMP_Text scoreText;                 // UI Text to display score
    public TMP_Text messageText;               // UI Text for messages
    public TMP_Text targetColorText;           // UI Text to display the target color
    public string targetColor;                 // Target color to collect (Red, Green, or Blue)
    public TMP_Text timerText; 
    public float timeRemaining = 60f;
    public TMP_Text gameOverText;
    public Button retryButton;

    public float moveSpeed = 5f;               // Movement speed of the player
    private Rigidbody rb;                      // Rigidbody component for physics-based movement

    private string[] colorNames = { "Red", "Green", "Blue" };  // Names for colors
    private Color[] collectibleColors = { Color.red, Color.green, Color.blue };  // Color objects for collectibles

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UpdateScore();
        ChangeTargetColor(); // Initialize with a random target color at game start
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            // Handle movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

            // Update timer
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();  
        }
        else
        {
            timerText.text = "Time: 0";
            GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Fetch the Collectible component (ensure each collectible has this component)
            Renderer renderer = other.GetComponent<Renderer>();
            Color collectibleColor = renderer.material.color;

            Debug.Log("Collectible Color: " + collectibleColor);  // Log the collectible color

            if (IsCorrectColor(collectibleColor))
            {
                score += 10;  // Add points for correct color
                messageText.text = "Correct Color!";
                Debug.Log("Correct Color! Score: " + score);
            }
            else
            {
                score -= 5;  // Subtract points for wrong color
                messageText.text = "Wrong Choice!";
                Debug.Log("Wrong Color! Score: " + score);
            }

            // Prevent score from going negative by clamping the score
            score = Mathf.Max(score, 0);

            UpdateScore();
            Destroy(other.gameObject);

            // Call the method to spawn a new collectible
            FindObjectOfType<CollectibleSpawner>().SpawnCollectible();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateTargetColorText()
    {
        targetColorText.text = "Target Color: " + targetColor;
    }

    // Method to check if the color matches the target color
    bool IsCorrectColor(Color collectibleColor)
    {
        Color targetColorEnum = GetColorFromString(targetColor);

        // Convert both colors to HSV for better matching
        Color.RGBToHSV(targetColorEnum, out float targetH, out _, out _);
        Color.RGBToHSV(collectibleColor, out float collectibleH, out _, out _);

        // Set a hue tolerance to allow for slight variations
        float hueTolerance = 0.1f;  // Tolerance can be adjusted if needed
        bool colorMatch = Mathf.Abs(targetH - collectibleH) < hueTolerance;

        // Debug output to verify if the colors are matching correctly
        Debug.Log($"Target: {targetColorEnum}, Collectible: {collectibleColor}, Match: {colorMatch}");

        return colorMatch;
    }

    Color GetColorFromString(string colorName)
    {
        switch (colorName)
        {
            case "Red":
                return Color.red;
            case "Green":
                return Color.green;
            case "Blue":
                return Color.blue;
            default:
                return Color.white;
        }
    }

    // Change target color to a random color
    public void ChangeTargetColor()
    {
        int randomIndex = Random.Range(0, colorNames.Length);
        targetColor = colorNames[randomIndex];  // Set target color name
        Debug.Log("New Target Color: " + targetColor);  // Debug output for target color
        UpdateTargetColorText();  // Update the UI text with the target color
    }

    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over! Final Score: " + score;
        retryButton.gameObject.SetActive(true);
        retryButton.onClick.AddListener(RetryGame);
    }

    void RetryGame()
    {
        score = 0;
        timeRemaining = 60f;
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        UpdateScore();
        ChangeTargetColor();  // Reset target color after retry
    }
}
