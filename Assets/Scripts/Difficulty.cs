using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DifficultyCheckpoint : MonoBehaviour
{
    public GameObject DifficultyCanvas; // UI panel with Easy + Hard buttons
    public Button EasyButton;
    public Button HardButton;
    public Button CancelButton;

    public GameObject EasyPath;
    public GameObject HardPath;

    public TextMeshProUGUI resultText;

    private bool triggered = false;

    private void Start()
    {
        DifficultyCanvas.SetActive(false);
        resultText.gameObject.SetActive(false);

        // Hook up button actions
        EasyButton.onClick.AddListener(SetEasy);
        HardButton.onClick.AddListener(SetHard);
        CancelButton.onClick.AddListener(CloseMenu);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            PauseGame();
            DifficultyCanvas.SetActive(true);
        }
    }

    private void SetEasy()
    {
        EasyPath.SetActive(true);
        HardPath.SetActive(false);

        OnDifficultySelected("Easy Path chosen!");
    }

    private void SetHard()
    {
        HardPath.SetActive(true);
        EasyPath.SetActive(false);

        OnDifficultySelected("Hard Path chosen!");
    }

    private void OnDifficultySelected(string message)
    {
        triggered = false;
        DifficultyCanvas.SetActive(false);
        resultText.text = message;
        resultText.gameObject.SetActive(true);

        ResumeGame();
        StartCoroutine(HideMessage(3f));
        Destroy(gameObject); // remove checkpoint so it can’t be reused
    }

    private void CloseMenu()
    {
        DifficultyCanvas.SetActive(false);
        ResumeGame();
        triggered = false;
    }

    IEnumerator HideMessage(float delay)
    {
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
    }
}
