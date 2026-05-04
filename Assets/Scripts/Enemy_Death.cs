using UnityEngine;

public class Enemy_Death : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
  public void Die()
        {
        FindObjectOfType<Enemy_Spawner>().EnemyDied();
        Destroy(this.gameObject);// Destroy the enemy game object when it dies
    }
}
