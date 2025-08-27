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

    private player_movement player;

    private delegate void Effect();
    private List<(string name, string secretName, Effect effect)> effects = new List<(string,string, Effect)>();

    void Start()
    {
        BuffCanvas.SetActive(triggered);
        resultText.gameObject.SetActive(false);

        //Tries to load the player script
        player = GameObject.FindFirstObjectByType<player_movement>();
        if (player == null)
        {
            Debug.Log("Player not found");
        }
        //Effects adding
        effects.Add(("?", "There is a Black Man behind you!!", () => player.speed *= 1.5f));
        effects.Add(("?", "Jump Suit Activated", () => player.jumpForce *= 2f));
        effects.Add(("?", "Just saw your crush, well I know the feeling", () => Debug.Log("Heal")));

        //Debuffs adding
        effects.Add(("?", "Your Heart is getting heavy", () => player.speed *= 0.25f));
        effects.Add(("?", "Jump Suit Corrupted", () => player.jumpForce *= 0.5f));
        effects.Add(("?", "Ok, sadly You got caught by that Black man", () => Debug.Log("Damage")));

     
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

            AssignRandomEffects();    
        }
    }

    private void AssignRandomEffects()
    {
        int e1 = Random.Range(0, effects.Count);
        int e2 = Random.Range(0, effects.Count);
        var text1 = buff1.GetComponentInChildren<TextMeshProUGUI>();
        var text2 = buff2.GetComponentInChildren<TextMeshProUGUI>();

        if (text1 != null) text1.text = effects[e1].name;
        if (text2 != null) text2.text = effects[e2].name;
        buff1.GetComponentInChildren<TextMeshProUGUI>().text = effects[e1].name;
        buff2.GetComponentInChildren<TextMeshProUGUI>().text = effects[e2].name;
        


        buff1.onClick.AddListener(() =>
        {
            effects[e1].effect();
            OnBuffSelected(effects[e1].secretName);
        });
        buff2.onClick.AddListener(() =>
        {
            effects[e2].effect();
            OnBuffSelected(effects[e2].secretName);
        });


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
        
        
        yield return new WaitForSecondsRealtime(delay);
        resultText.gameObject.SetActive(false);
        ResumeGame();


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
