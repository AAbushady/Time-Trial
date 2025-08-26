using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    [Header("Countdown Setup")]
    public Text countdownText;
    public float countdownDuration = 3f;
    
    [Header("Audio (Optional)")]
    public AudioSource audioSource;
    public AudioClip countBeep;
    public AudioClip goBeep;
    
    private static CountdownManager instance;
    private bool raceStarted = false;
    
    public static bool RaceStarted => instance != null && instance.raceStarted;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    void Start()
    {
        // Auto-find UI components if not assigned
        if (countdownText == null)
            countdownText = FindFirstObjectByType<Text>();
            
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        StartCountdown();
    }
    
    public void StartCountdown()
    {
        raceStarted = false;
        StartCoroutine(CountdownSequence());
    }
    
    private IEnumerator CountdownSequence()
    {
        // Show countdown numbers
        for (int i = (int)countdownDuration; i > 0; i--)
        {
            if (countdownText != null)
            {
                countdownText.text = i.ToString();
                countdownText.fontSize = 80;
                countdownText.color = Color.red;
            }
            
            // Play beep sound
            if (audioSource != null && countBeep != null)
                audioSource.PlayOneShot(countBeep);
            
            yield return new WaitForSeconds(1f);
        }
        
        // Show GO!
        if (countdownText != null)
        {
            countdownText.text = "GO!";
            countdownText.fontSize = 100;
            countdownText.color = Color.green;
        }
        
        // Play go sound
        if (audioSource != null && goBeep != null)
            audioSource.PlayOneShot(goBeep);
        
        // Race starts now!
        raceStarted = true;
        
        // Hide countdown after 1 second
        yield return new WaitForSeconds(1f);
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }
    
    // Method to restart the countdown (useful for race restarts)
    public void RestartCountdown()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(true);
        StartCountdown();
    }
}