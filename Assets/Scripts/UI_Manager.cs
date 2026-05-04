using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages the user interface elements and game state transitions for the game, including health, points, and
/// end-of-game displays.
/// </summary>
/// <remarks>UIManager provides centralized control over UI updates and game state changes such as win or loss
/// conditions. It exposes static fields and methods for updating points and health, and maintains references to key UI
/// components. The class is designed to be accessed via its static Instance property, following a singleton pattern.
/// UIManager should be attached to a GameObject in the Unity scene to function correctly.</remarks>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private GameObject player;
    private GameObject enemy;

    public TextMeshProUGUI health_text;
    public TextMeshProUGUI points_text;
    public TextMeshProUGUI high_score_text;

    public static int current_health = 4;
    public static int current_points = 0;

    void Awake()
    {
        Instance = this;
        // Initialize health and points at the start of the game
        current_health = 4;
        current_points = 0;
    }
    /// <summary>
    /// Initializes references to the player and enemy game objects and updates the high score display at the start of
    /// the component's lifecycle.
    /// </summary>
    /// <remarks>This method is typically called automatically by Unity when the script instance is being
    /// loaded. It searches for game objects with the tags "Player" and "Enemy" and updates the high score UI if the
    /// relevant text component is assigned.</remarks>
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        if (high_score_text != null)
        {
            high_score_text.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }
    /// <summary>
    /// Updates the user interface to reflect the current health and points, and checks for game over conditions.
    /// </summary>
    /// <remarks>This method should be called regularly, such as once per frame, to ensure the UI remains in
    /// sync with the game state. If the player's health reaches zero, the game ends with a loss. If the player's points
    /// reach or exceed four, the game ends with a win.</remarks>
    void Update()
    {
        health_text.text = "HP: " + current_health;
        points_text.text = "Points: " + current_points;

        if (current_health <= 0) game_over("Lose");
        if (current_points >= 20) game_over("Win");
    }
    /// <summary>
    /// Handles the actions required when the player wins the game, including saving the current score and transitioning
    /// to the win scene.
    /// </summary>
    /// <remarks>This method updates the stored high score if the current score exceeds it and saves the last
    /// game score. It then loads the scene named "You_Win" to indicate the player's victory. Ensure that the
    /// PlayerPrefs system is available and that the "You_Win" scene exists in the build settings.</remarks>
    public void win_game()
    {
        // Save the last game score using PlayerPrefs
        PlayerPrefs.SetInt("LastGameScore", current_points);

        // Check if the current points are greater than the stored high score and update it if necessary
        if (current_points > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", current_points);
        }

        SceneManager.LoadScene("You_Win");
    }
    /// <summary>
    /// Handles the end-of-game sequence based on the specified game state.
    /// </summary>
    /// <remarks>This method disables both the player and enemy objects before displaying the appropriate
    /// end-of-game message and performing the corresponding actions. If the state is not "Win", the method transitions
    /// to the game over scene.</remarks>
    /// <param name="state">A string indicating the outcome of the game. Use "Win" to indicate a victory; any other value is treated as a
    /// loss.</param>
    void game_over(string state)
    {
        if (player != null) player.SetActive(false);
        if (enemy != null) enemy.SetActive(false);

        if (state == "Win")
        {
            set_text("You Win!");
            win_game();
        }
        else
        {
            set_text("Game Over");
            SceneManager.LoadScene("Game_Over");
        }
    }
    /// <summary>
    /// Updates the displayed health text to the specified value and ensures it is fully visible.
    /// </summary>
    /// <remarks>If the health text UI element has a CanvasGroup component, its alpha is set to 1.0 to make
    /// the text fully visible.</remarks>
    /// <param name="text_to_display">The text to display in the health text UI element. Can be any string, including empty or null.</param>
    public void set_text(string text_to_display)
    {
        health_text.text = text_to_display;

        // Ensure the text is fully visible
        CanvasGroup group = health_text.GetComponent<CanvasGroup>();
        if (group != null)
        {
            group.alpha = 1.0f;
        }
    }

    // Static method to be called when the player gets points. This allows other scripts to add points without needing a reference to the UIManager instance.
    public static void add_point()
    {
        current_points++;
    }
    
 
}
