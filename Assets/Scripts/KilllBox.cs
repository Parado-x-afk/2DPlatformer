using UnityEngine;
using UnityEngine.SceneManagement;

public class KilllBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Respawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(Respawn);
        }
    }
}
