//using TMPro;
//using UnityEngine;

//public class Block : MonoBehaviour
//{
//    private GameObject player;
//    private GameObject barrier;
//    public float health_points;
//    private Vector3 pos_1 = new Vector3((float)-0.7773138, (float)0.2, (float)12.37);
//    private Vector3 pos_2 = new Vector3((float)12.37, (float)0.2, (float)-0.7773138);
//    private Vector3 pos_3 = new Vector3((float)12.37, (float)0.2, (float)12.37);
//    public float Points;
//    public float WinPoints;
//    public float enemy_change;
//    public float enemy_change_kill;
 
//    public float i = 1;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        health_points = 2;
//        Points = 0;
//        enemy_change_kill = 0;
//        enemy_change = 0;
//        player = GameObject.FindWithTag("Player");
//        barrier = GameObject.FindWithTag("Barrier");
//    }


//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//    void OnTriggerEnter(Collider target)
//    {
     
//         if (target.gameObject.tag == "Enemy")
//        {

//            Points++;
//            enemy_change++;

//            if (Points >= 1 && Points! >= 2)
//            {

//                this.transform.position = pos_2;
//            }
//            else if (Points >= 2 && Points! >= 3)
//            {

//                this.transform.position = pos_3;
//            }
//            else if (Points >= 3)
//            {

//                this.transform.position = pos_1;
//                Points = 0;
//                WinPoints++;
//            }
//            if (WinPoints >= 2)
//            {
//                this.gameObject.SetActive(false);
//                this.enabled = false;
//                player.SetActive(false);
             
//            }

//        }

//        //PickupType
//    }
//}
