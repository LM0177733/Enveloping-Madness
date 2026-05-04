using System.Collections;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{// This script is responsible for spawning enemy game objects at random locations after a delay when an enemy dies. It uses
   
    public GameObject enemy_prefab;
    private Quaternion spawn_rotation_pos_3 = Quaternion.Euler(0, -43.5f, 0);
    private Quaternion spawn_rotation_pos_2 = Quaternion.Euler(0, -139.91f, 0);
    private Quaternion spawn_rotation_pos_1 = Quaternion.Euler(0, 90, 0);
    private Quaternion correct_rotation;
    private float saved_points = 0;
   
    private Vector3 pos_1 = new Vector3((float)0, (float)0.2, (float)20);
    private Vector3 pos_2 = new Vector3((float)20, (float)0.2, (float)-20);
    private Vector3 pos_3 = new Vector3((float)-20, (float)0.2, (float)-20);
    int random_location;
    private Vector3[] spawn_positions; // Array to hold the spawn positions
    private bool is_spawning = false;
    public AudioSource spawner_audio; // Optional: Assign an AudioSource component in the Unity Editor to play spawn sounds
    public AudioClip spawn_sound;     // Optional: Assign an AudioClip in the Unity Editor to specify the sound played when an enemy spawns
                                      // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    void Start()
    {// Initialize the spawn positions array with the defined positions
        spawn_positions = new Vector3[] {
            pos_1,
            pos_2,
            pos_3
        };


        Spawn_Enemy_Now();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.current_points >= 10)
        {
            saved_points = 1F;
        }
        else
        {             saved_points = 2F;
        }

    }
    /// <summary>
    /// Handles logic to be executed when an enemy is defeated.
    /// </summary>
    /// <remarks>This method initiates a delayed spawn operation after an enemy has died. It is typically
    /// called in response to an enemy's death event within the game logic.</remarks>
    public void EnemyDied()
    {
       
        StartCoroutine(Spawn_After_Delay(saved_points));
    }
   /// <summary>
   /// Spawns an enemy at a random position if no spawn operation is currently in progress.
   /// </summary>
   /// <remarks>This method instantiates the configured enemy prefab at a randomly selected spawn position. If
   /// a spawn sound is assigned, it plays at the spawn location. The method does nothing if a spawn operation is
   /// already underway.</remarks>
    public void Spawn_Enemy_Now()
    {
        if (is_spawning) return;

        int index = Random.Range(0, spawn_positions.Length);
        Vector3 spawn_pos = spawn_positions[index];
        if (spawn_pos == pos_3)
        { correct_rotation = spawn_rotation_pos_3; }
        else if (spawn_pos == pos_1) 
        { correct_rotation = spawn_rotation_pos_1; }
       else if (spawn_pos == pos_2) 
        { correct_rotation = spawn_rotation_pos_2; }
        Instantiate(enemy_prefab, spawn_pos, correct_rotation);
        if (spawn_sound != null)
        {
           AudioSource.PlayClipAtPoint(spawn_sound, spawn_pos);
        }
    }
 /// <summary>
 /// Waits for the specified delay before spawning an enemy.
 /// </summary>
 /// <remarks>This coroutine can be used with Unity's StartCoroutine method to introduce a timed delay before
 /// spawning an enemy. If called while a spawn is already in progress, the behavior depends on the implementation of
 /// the calling code.</remarks>
 /// <param name="delay">The amount of time, in seconds, to wait before spawning the enemy. Must be non-negative.</param>
 /// <returns>An enumerator that waits for the specified delay before triggering the enemy spawn.</returns>
    IEnumerator Spawn_After_Delay(float delay)
    {
        is_spawning = true;
        yield return new WaitForSeconds(delay);
        is_spawning = false;
        Spawn_Enemy_Now();
    }
}