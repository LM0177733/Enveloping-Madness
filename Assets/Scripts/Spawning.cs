using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// Handles the spawning of new floor pieces when the player enters a designated trigger collider in the game
/// environment.
/// </summary>
/// <remarks>Attach this component to a trigger collider that is a child of a floor piece GameObject. When the
/// player enters the trigger, a new floor piece is spawned in front of the current one, and the trigger is temporarily
/// disabled to prevent repeated spawning. The trigger is reset and the floor piece deactivated after a short delay,
/// allowing for efficient reuse of floor segments. Ensure that the trigger collider has a parent GameObject assigned
/// for correct operation.</remarks>
public class Spawning : MonoBehaviour
{
    private GameObject player;
    private GameObject spawning;
    // This script is attached to a trigger collider that spawns floor pieces when the player enters it.
    private string[] floor_types = { "Front_Maze", "Left_Maze", "Right_Maze" };
    private bool has_triggered = false;
    private static string lastSpawnedTag = "";
    private static int consecutiveCount = 0;
    // The my_floor variable is used to reference the parent GameObject of the trigger, which is the floor piece it is attached to. This allows the script to know where to spawn new floor pieces in front of the current one.
    private GameObject my_floor;

    public void Start()
    {
        spawning=GameObject.FindGameObjectWithTag("Sensor");
        player = GameObject.FindGameObjectWithTag("Player");
    }


  /// <summary>
  /// Handles collision events when another collider enters the trigger, spawning a new floor segment if the entering
  /// object is tagged as a player and the trigger has not already been activated.
  /// </summary>
  /// <remarks>This method ensures that a new floor segment is spawned only once per trigger activation and only
  /// when the player enters the trigger. It also manages the type of floor spawned to avoid repeating the same type
  /// more than twice consecutively. If the expected exit point is missing, a fallback position and rotation are used
  /// for spawning.</remarks>
  /// <param name="other">The collider that has entered the trigger area. Must not be null.</param>
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player") && !has_triggered)
        {
            if (my_floor == null) { return; }

            has_triggered = true;
            Vector3 spawnPos;
            Quaternion spawnRot;
            Transform exitPoint = my_floor.transform.Find("Sensor");
            int randomIndex = UnityEngine.Random.Range(0, floor_types.Length);
            string randomTag = floor_types[randomIndex];
            if (exitPoint != null)
            {
                spawnPos = exitPoint.position;
                Vector3 currentEuler = exitPoint.rotation.eulerAngles;
                float snappedY = Mathf.Round(currentEuler.y / 90f) * 90f;
                spawnRot = Quaternion.Euler(0, snappedY, 0);

            }
            else
            {
                spawnPos = my_floor.transform.position + (my_floor.transform.forward * 10f);
                spawnRot = my_floor.transform.rotation;
                Debug.LogWarning("ExitNode missing on " + my_floor.name + "! Using fallback.");
            }

            if (randomTag == lastSpawnedTag && consecutiveCount >= 2) 
            {
                randomIndex = (randomIndex + 1) % floor_types.Length;
                randomTag = floor_types[randomIndex];
                consecutiveCount = 1;
                Debug.Log("Streak broken! Forced a different floor type.");
            }
            else if (randomTag == lastSpawnedTag)
            {
                consecutiveCount++; 
            }
            else
            {
                consecutiveCount = 1; 
            }
            lastSpawnedTag = randomTag;
            Object_Spawner.Instance.Spawn_From_Pool(randomTag, spawnPos, spawnRot);
        

        Invoke("Recycle", 5f);
        }
    }
    // The Recycle method is called after a delay (5 seconds in this case) to reset the trigger and deactivate the floor piece. This allows the floor piece to be reused later when the player triggers it again.
    void Recycle()
    {
        has_triggered = false;
        transform.parent.gameObject.SetActive(false);
    }
    // The OnEnable method is called whenever the GameObject this script is attached to is enabled. It resets the has_triggered flag to ensure that the trigger can function correctly when reused.
    private void OnEnable()
    {

        has_triggered = false;
        // The following code checks if the trigger has a parent GameObject (which should be the floor piece it is attached to) and assigns it to the my_floor variable. This allows the script to know which floor piece to spawn new pieces in front of.
        if (transform.parent != null)
        {
            my_floor = transform.parent.gameObject;
        }
    }
}