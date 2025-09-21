using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    [Header("UI References")]
    public Text countdownText;
    public Text timerText;

    [Header("Countdown Settings")]
    public float countdownDuration = 3f;

    [Header("Audio (Optional)")]
    public AudioSource audioSource;
    public AudioClip countBeep;
    public AudioClip goBeep;

    // Internal systems
    private CountdownSystem countdown;
    private TimerSystem timer;

    // Singleton for easy access
    private static RaceManager instance;
    public static RaceManager Instance => instance;

    // Public API - what other scripts use
    public static bool IsRaceStarted => Instance?.countdown?.IsComplete ?? false;
    public static float CurrentTime => Instance?.timer?.ElapsedTime ?? 0f;
    public static bool IsTimerRunning => Instance?.timer?.IsRunning ?? false;

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
        SetupSystems();
        StartRace();
    }

    void Update()
    {
        // Start timer when countdown completes
        if (countdown.IsComplete && !timer.IsRunning)
        {
            timer.StartTimer();
        }

        // Update timer display
        timer.UpdateDisplay();
    }

    private void SetupSystems()
    {
        // Auto-find UI components if not assigned
        if (countdownText == null)
            countdownText = GameObject.Find("CountdownText")?.GetComponent<Text>();

        if (timerText == null)
            timerText = GameObject.Find("TimerText")?.GetComponent<Text>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // Initialize internal systems
        countdown = new CountdownSystem(this, countdownText, audioSource,
                                      countBeep, goBeep, countdownDuration);
        timer = new TimerSystem(timerText);

        // Validate setup
        if (countdownText == null)
            Debug.LogWarning("RaceManager: No countdown text found. Create UI Text named 'CountdownText'");
        if (timerText == null)
            Debug.LogWarning("RaceManager: No timer text found. Create UI Text named 'TimerText'");
    }

    // Public methods for external control
    public void StartRace()
    {
        countdown.StartCountdown();
        Debug.Log("Race starting...");
    }

    public void RestartRace()
    {
        countdown.Reset();
        timer.Reset();
        StartRace();
        Debug.Log("Race restarting...");
    }

    public void StopRace()
    {
        timer.StopTimer();
        Debug.Log("Race stopped");
    }

    // Public getters for external systems
    public float GetRaceTime() => timer.ElapsedTime;
    public bool HasRaceStarted() => countdown.IsComplete;
}