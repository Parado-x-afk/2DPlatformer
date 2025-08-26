using UnityEngine;
using UnityEngine.UI;

public class CheckpointBuff : MonoBehaviour
{
    public GameObject buffUI;          // Parent panel with 2 buttons
    public Button buff1Button;         // Button 1
    public Button buff2Button;         // Button 2

    private bool triggered = false;

    private void Start()
    {
        // Hide UI at start
        buffUI.SetActive(false);

        // Hook up buttons
        buff1Button.onClick.AddListener(() => OnBuffSelected("Buff 1"));
        buff2Button.onClick.AddListener(() => OnBuffSelected("Buff 2"));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            PauseGame();
            buffUI.SetActive(true);
        }
    }

    private void OnBuffSelected(string buffName)
    {
        Debug.Log(buffName + " chosen!"); // Later you can apply effect here

        buffUI.SetActive(false);
        ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // freeze game
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // unfreeze game
    }
}
