using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class Checkpoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject BuffCanvas;
    public Button buff1;
    public Button buff2;

    public TextMeshProUGUI resultText;

    private bool triggered = false;

    void Start()
    {
        BuffCanvas.SetActive(triggered);
        resultText.gameObject.SetActive(false);

        buff1.onClick.AddListener(() => OnBuffSelected/*Method you want*/("Buff1"));
        buff2.onClick.AddListener(() => OnBuffSelected("Buff2"));
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            PauseGame();
            BuffCanvas.SetActive(triggered);
        }
    }

    private void OnBuffSelected(string buffer)
    {
        triggered = false;
        BuffCanvas.SetActive(triggered);
        resultText.text = "Selected: " + buffer;
        resultText.gameObject.SetActive(true);
        StartCoroutine(WaitForEndOfFrame(3f));
    }

    IEnumerator WaitForEndOfFrame(float delay)
    {
        
        ResumeGame();
        yield return new WaitForSecondsRealtime(delay);
        resultText.gameObject.SetActive(false);


    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
