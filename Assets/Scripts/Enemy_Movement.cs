using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Controls enemy movement and interactions within the game, including pursuing the player and handling collision
/// events that affect player health or points.
/// </summary>
/// <remarks>Attach this component to enemy game objects to enable automated movement toward the player and to
/// manage interactions with other objects such as the player or barriers. This class relies on Unity's MonoBehaviour
/// lifecycle methods and requires references to other components and tagged game objects present in the
/// scene.</remarks>
public class Enemy_Movement : MonoBehaviour
{
  private Enemy_Spawner enemy_spawner;
   private Enemy_Death enemy_death;
    private List<Vector3> target_locations=new List<Vector3>();
    private GameObject player;
    private GameObject barrier;
    private GameObject enemy;
    private GameObject cam;

    private Vector3 pos_1=new Vector3((float)-0.7773138, (float)0.2, (float)12.37);
    private Vector3 pos_2 = new Vector3((float)12.37, (float)0.2, (float)-4.12);
    private Vector3 pos_3 = new Vector3((float)-8.37, (float)0.2, (float)-8.0);
   
    public float WinPoints;
    public float enemy_change;
    public float enemy_change_kill;
    public float i = 1;
    int random_location;
    public float speed = 5f;
    private TextMeshProUGUI textf_1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// Initializes the component and sets up references to required game objects and components before the first frame
    /// update.
    /// </summary>
    /// <remarks>This method is called automatically by Unity before the first execution of Update. Use Start
    /// to perform any initialization that depends on other components or objects being present in the scene.</remarks>
    void Start()
    {
       
        enemy_death = GetComponent<Enemy_Death>();
        enemy_spawner = GetComponent<Enemy_Spawner>();
     
    
        enemy_change_kill = 0;
        enemy_change = 0;
        player = GameObject.FindWithTag("Player");
        barrier = GameObject.FindWithTag("Barrier");
        enemy = GameObject.FindWithTag("Enemy");
       // textf_1 = GameObject.FindWithTag("Health_Points").GetComponent<TextMeshProUGUI>();
        target_locations.Add(pos_1);
        target_locations.Add(pos_2);
        target_locations.Add(pos_3);
       random_location = Random.Range(0, target_locations.Count);
    }

   /// <summary>
   /// Updates the object's position each frame and handles user input for quitting the application.
   /// </summary>
   /// <remarks>This method is typically called once per frame by the Unity engine. It moves the object toward
   /// the player's position at a constant speed and exits the application if the 'down' key is pressed.</remarks>
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            return; 
        }
     
            if (this.transform.position != player.transform.position)
            {
                if (UIManager.current_points >= 15)
                {
                    speed = 10f;
                }
                else if (UIManager.current_points >= 5)
                {
                    speed = 7f;
                }
                else
                {
                    speed = 5f;
                }
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            
        }
        if (Input.GetKeyDown("down"))
        {
            Application.Quit();
        }
        //cam.transform.position = barrier.transform.position;
        //if ()
        //{ 
        //}
    }

    /// <summary>
    /// Handles trigger events when another collider enters the trigger collider attached to this object. Updates player
    /// health or points and triggers effects based on the tag of the colliding object.
    /// </summary>
    /// <remarks>If the colliding object is tagged as "Player", the player's health is decreased and a hit
    /// effect is played if available. If the colliding object is tagged as "Barrier", the player's points are increased
    /// and associated visual and audio effects are played if available. In both cases, the enemy's death routine is
    /// invoked.</remarks>
    /// <param name="target">The collider that enters the trigger. Must not be null. The behavior depends on the tag assigned to this
    /// collider.</param>
    void OnTriggerEnter(Collider target)
    {

        // Check if the colliding object is the player
        if (target.CompareTag("Player"))
        {
            UIManager.current_health--;
            if (target.GetComponent<Player_Effects>() != null)
            {
                target.GetComponent<Player_Effects>().play_hit_effect();
            }

            enemy_death.Die();
        }

     
        else if (target.CompareTag("Barrier"))
        {
            UIManager.current_points++;
            ParticleSystem barrier_fx = target.GetComponentInChildren<ParticleSystem>();

            if (barrier_fx != null)
            {
                barrier_fx.Play();
            }
            AudioSource enemy_death_fx = target.GetComponent<AudioSource>();
            if (enemy_death_fx != null)
            {
                enemy_death_fx.PlayOneShot(enemy_death_fx.clip);
            }

            enemy_death.Die();
        }

    }
       

       
        
        //PickupType
    
 
}
