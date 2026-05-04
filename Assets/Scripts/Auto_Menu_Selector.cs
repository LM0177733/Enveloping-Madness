using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Provides automatic cycling and selection of menu buttons in a Unity UI, enabling users to navigate and select menu
/// options using keyboard input or after a configurable delay.
/// </summary>
/// <remarks>This class is intended for use with Unity's UI system and should be attached to a GameObject
/// containing Button components. It supports both manual selection via the Space key and automatic selection after a
/// period of inactivity. Ensure that the menu_button array is populated with valid Button references in the desired
/// order.</remarks>
public class Auto_Menu_Selector : MonoBehaviour
{
    public Button[] menu_button;
    private AudioSource audio_source;
    private int current_index = 0;
    private float selection_timer = 0f;
    private const float AUTO_SELECT_DELAY = 5f;
    private bool has_started = false;
    private float menu_cooldown = 0f;
    private const float COOLDOWN_TIME = 0.5f;
    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }
    // This method is called once per frame and handles the input for cycling through menu options and auto-selecting after a delay.

    /// <summary>
    /// Handles user input and menu selection logic for each frame. Processes input for cycling through menu options and
    /// triggers automatic selection after a specified delay.
    /// </summary>
    /// <remarks>This method should be called once per frame, typically from the game loop or Unity's Update
    /// event. It manages both manual selection (via key press or blink detection) and automatic selection after a
    /// timeout. The method resets relevant state when input is detected and ensures only one selection action occurs
    /// per input event.</remarks>
    void Update()
    {
        
        if (menu_cooldown > 0) menu_cooldown -= Time.deltaTime;        
        if ((Input.GetKeyDown(KeyCode.Space)))
        {           
           
            menu_cooldown = 0.5f; 
            selection_timer = 0f;

            if (!has_started)
            {
                has_started = true;
                Highlight_Button(0);
            }
            else
            {
                Cycle_Selection();
            }
        }

        
    }


    // This method cycles through the menu buttons, updating the current index and visually highlighting the selected button.
    void Cycle_Selection()
    {
        current_index = (current_index + 1) % menu_button.Length;
        Highlight_Button(current_index);
    }

    // This method visually highlights the button at the specified index, indicating that it is currently selected.
    void Highlight_Button(int index)
    { 
        EventSystem.current.SetSelectedGameObject(null);

        // Select the button component
        menu_button[index].Select();

        // FORCE the EventSystem to focus on the button's game object
        EventSystem.current.SetSelectedGameObject(menu_button[index].gameObject);

        if (audio_source != null && audio_source.clip != null)
        {
            audio_source.PlayOneShot(audio_source.clip);
        }
    }

    // This method invokes the onClick event of the currently selected button, effectively simulating a button press.
    void Select_Current_Button()
    {
        
        menu_button[current_index].onClick.Invoke();
        has_started = false; // Reset to initial state after selection
    }

    /// <summary>
    /// Starts the game by loading the main game scene.
    /// </summary>
    /// <remarks>This method transitions the application to the scene named "Game". Ensure that the scene is added to
    /// the build settings to avoid runtime errors.</remarks>

    public void Play_Game()
    {
        SceneManager.LoadScene("Game");
    }
    /// <summary>
    /// Loads the Help scene in the application.
    /// </summary>
    /// <remarks>Use this method to navigate to the Help section. The current scene will be replaced by the
    /// Help scene. Ensure that the scene named "Help" is included in the build settings.</remarks>
    public void Open_Help()
    {
        SceneManager.LoadScene("Help");
    }

    // This method is responsible for returning to the main menu scene. It calls SceneManager.LoadScene() with the name of the main menu scene to transition back to it. Ensure that the "Main_Menu" scene is added to the build settings for this to work correctly.
    public void Open_Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    /// <summary>
    /// Exits the application or stops play mode in the Unity Editor.
    /// </summary>
    /// <remarks>In a built application, this method closes the application. When running in the Unity Editor,
    /// it stops play mode instead of closing the editor. This method has no effect in WebGL builds.</remarks>
    public void Quit_Game()
    {
        Debug.Log("Quitting...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}