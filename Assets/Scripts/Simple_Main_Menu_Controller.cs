using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Provides control logic for a simple main menu in a Unity application, enabling scene transitions to the game or help
/// screens and handling application exit.
/// </summary>
/// <remarks>This controller assumes that the scenes named "Game" and "help" are included in the Unity build
/// settings. The Quit_Game method will close the application when built, and stops play mode when running in the Unity
/// Editor.</remarks>
public class Simple_Main_Menu_Controller : MonoBehaviour
{// This method is responsible for transitioning the application to the game scene. It calls SceneManager.LoadScene() with the name of the game scene to load that scene. Ensure that the "Game" scene is added to the build settings for this to work correctly.
    public void Play_Game()
    {
        SceneManager.LoadScene("Game"); 
    }
    //  This method is responsible for opening the help scene. It calls SceneManager.LoadScene() with the name of the help scene to transition the application to that scene. Ensure that the "help" scene is added to the build settings for this to work correctly.
    public void Open_Help()
    {
        SceneManager.LoadScene("help");
    }
    // This method is responsible for returning to the main menu scene. It calls SceneManager.LoadScene() with the name of the main menu scene to transition back to it. Ensure that the "Main_Menu" scene is added to the build settings for this to work correctly.
    public void Open_Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    // This method is responsible for quitting the application. It calls Application.Quit() to close the application and logs a message to the console for confirmation. Additionally, it includes a conditional compilation directive to ensure that when running in the Unity Editor, play mode is stopped instead of quitting the editor itself.
    public void Quit_Game()
    {
        Application.Quit();
        Debug.Log("Quit successful");

        // In the Unity Editor, Application.Quit() does not stop play mode, so we need to explicitly set it to false.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
