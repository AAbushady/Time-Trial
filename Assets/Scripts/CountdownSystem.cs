using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownSystem
{
    private MonoBehaviour owner;
    private Text countdownText;
    private AudioSource audioSource;
    private AudioClip countBeep;
    private AudioClip goBeep;
    private float countdownDuration;
    private bool isComplete = false;

    public bool IsComplete => isComplete;

    public CountdownSystem(MonoBehaviour owner, Text text, AudioSource audio,
                         AudioClip countSound, AudioClip goSound, float duration)
    {
        this.owner = owner;
        this.countdownText = text;
        this.audioSource = audio;
        this.countBeep = countSound;
        this.goBeep = goSound;
        this.countdownDuration = duration;
    }

    public void StartCountdown()
    {
        isComplete = false;
        owner.StartCoroutine(CountdownSequence());
    }

    public void Reset()
    {
        isComplete = false;
        if (countdownText != null)
            countdownText.gameObject.SetActive(true);
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

        // Countdown complete!
        isComplete = true;

        // Hide countdown after 1 second
        yield return new WaitForSeconds(1f);
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }
}