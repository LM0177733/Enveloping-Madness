using UnityEngine;
using TMPro;
/// <summary>
/// Represents a Unity MonoBehaviour that displays the final and high scores on a scene using TextMeshProUGUI
/// components.
/// </summary>
/// <remarks>This class retrieves the last game score and high score from PlayerPrefs and updates the associated
/// UI text elements when the scene starts. Attach this component to a GameObject in the scene and assign the
/// TextMeshProUGUI fields in the Unity Editor to display the scores correctly.</remarks>
public class Win_Scene_Display : MonoBehaviour
{
    public TextMeshProUGUI high_score_text;
    // Start is called before the first frame update
    void Start()
    {

    }
   
}