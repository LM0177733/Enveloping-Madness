using UnityEngine;

[System.Serializable]
public class Pool_Item 
{
    // This class is a simple data structure to hold information about each type of object we want to pool.
    public string tag;
    public GameObject prefab;
    public int amount_to_pool;
  
}
