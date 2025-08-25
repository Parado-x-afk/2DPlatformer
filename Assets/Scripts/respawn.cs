using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{
    public float fallLimit = -5f; // Y position where the player respawns
    public Vector3 respawnPoint = new Vector3(50, 0, 0); // Where the player respawns

    void Update()
    {
        if (transform.position.y < fallLimit) // If player falls below limit
        {
            transform.position = respawnPoint; // Respawn at the set point
            Debug.Log("Player Respawned!");
        }
    }
}

