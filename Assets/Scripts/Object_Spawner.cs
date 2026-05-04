using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages a pool of reusable GameObjects to optimize instantiation and destruction performance in Unity scenes.
/// </summary>
/// <remarks>This class implements the singleton pattern, providing a global access point to the object pool via
/// the static Instance property. It initializes pools for each specified prefab in the itemsToPool list and allows
/// objects to be spawned and reused efficiently. Use SpawnFromPool to retrieve and activate pooled objects at a
/// specified position and rotation. This approach is particularly useful for scenarios with frequent object creation
/// and destruction, such as spawning projectiles or enemies.</remarks>
public class Object_Spawner : MonoBehaviour
{
    public List<Pool_Item> items_to_pool;
    public Dictionary<string, Queue<GameObject>> pool_dictionary;
    public Dictionary<string, int> spawn_stats;
    public static Object_Spawner Instance;
    public NavMeshSurface navSurface;
    public GameObject enemyToActivate;
    void Start()
    {
        if (navSurface != null)
        {
            navSurface.BuildNavMesh();
        }
    }
    void Awake()
    {
        Instance = this;
        pool_dictionary = new Dictionary<string, Queue<GameObject>>();
        spawn_stats = new Dictionary<string, int>(); 
        foreach (Pool_Item item in items_to_pool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < item.amount_to_pool; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false); 
                objectPool.Enqueue(obj);
            }
            pool_dictionary.Add(item.tag, objectPool);
            spawn_stats.Add(item.tag, 0);
        }
        StartCoroutine(InitializeMaze());
    }
    private void Update()
    {
      
    }
 /// <summary>
 /// Spawns an inactive GameObject from the specified object pool at the given position and rotation.
 /// </summary>
 /// <remarks>The spawned GameObject is immediately re-enqueued into the pool after activation. The method
 /// updates the navigation mesh after spawning. The returned GameObject is set active and its transform is updated to
 /// the specified position and rotation.</remarks>
 /// <param name="tag">The tag identifying the object pool from which to spawn the GameObject. Must correspond to an existing pool.</param>
 /// <param name="position">The world position at which to place the spawned GameObject.</param>
 /// <param name="rotation">The rotation to apply to the spawned GameObject.</param>
 /// <returns>The spawned GameObject if the specified pool exists; otherwise, null.</returns>
    public GameObject Spawn_From_Pool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!pool_dictionary.ContainsKey(tag)) 
            return null;
        spawn_stats[tag]++;
        Debug.Log($"Total {tag} spawned: {spawn_stats[tag]}");
        GameObject object_to_spawn = pool_dictionary[tag].Dequeue();

        object_to_spawn.SetActive(true);
        object_to_spawn.transform.position = position;
        object_to_spawn.transform.rotation = rotation;


        pool_dictionary[tag].Enqueue(object_to_spawn);
        if (GetTotalSpawnCount() >= 6)
        {
            Debug.Log("You've reached 6 floors! Winning...");
            SceneManager.LoadScene("You_Win");
        }

        UpdateMazeNavMesh();

        return object_to_spawn;
    }

    /// <summary>
    /// Initializes the maze and activates the enemy after a short delay.
    /// </summary>
    /// <remarks>This coroutine can be used with Unity's StartCoroutine method to introduce a delay before
    /// activating the enemy object in the maze.</remarks>
    /// <returns>An enumerator that performs the initialization sequence when iterated.</returns>
    IEnumerator InitializeMaze()
    {        
        yield return new WaitForSeconds(0.5f);
        if (enemyToActivate != null) enemyToActivate.SetActive(true);
    }

    /// <summary>
    /// Initiates an update of the maze navigation mesh if a navigation surface is available.
    /// </summary>
    /// <remarks>This method starts the baking process for the navigation mesh on the next frame. It has no
    /// effect if the navigation surface is not set.</remarks>
    public void UpdateMazeNavMesh()
    {
        if (navSurface != null)
        {
            StartCoroutine(BakeNextFrame());
        }
    }
    /// <summary>
    /// Advances the coroutine by one frame and triggers a NavMesh rebuild on the next fixed update, if a navigation
    /// surface is available.
    /// </summary>
    /// <remarks>Use this coroutine to defer NavMesh rebuilding until the next fixed update cycle. This can
    /// help synchronize navigation updates with physics or other time-sensitive operations.</remarks>
    /// <returns>An enumerator that yields control for one frame and then until the next fixed update, after which the navigation
    /// mesh is rebuilt if applicable.</returns>
    private IEnumerator BakeNextFrame()
    {
        yield return null;
        yield return new WaitForFixedUpdate();

        if (navSurface != null)
        {
            navSurface.BuildNavMesh();
            Debug.Log("NavMesh Expanded onto new floors!");
        }

    }
    /// <summary>
    /// Calculates the total number of spawned entities recorded in the current statistics.
    /// </summary>
    /// <returns>The sum of all spawn counts. Returns 0 if no entities have been spawned.</returns>
    public int GetTotalSpawnCount()
    {
        int total = 0;
        foreach (var count in spawn_stats.Values) total += count;
        return total;
    }
    /// <summary>
    /// Gets the number of spawned entities associated with the specified tag.
    /// </summary>
    /// <param name="tag">The tag used to identify the group of spawned entities. Cannot be null.</param>
    /// <returns>The number of spawned entities for the given tag. Returns 0 if the tag does not exist.</returns>
    public int GetSpawnCountByTag(string tag)
    {
        return spawn_stats.ContainsKey(tag) ? spawn_stats[tag] : 0;
    }
}