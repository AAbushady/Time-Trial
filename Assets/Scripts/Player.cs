using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Car Reference")]
    public Car car;

    void Start()
    {
        // Auto-find car component if not assigned
        if (car == null)
            car = GetComponent<Car>();

        if (car == null)
            Debug.LogError("Player needs a Car component!");
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (car == null) return;

        // Check if race has started - no input until countdown finishes!
        if (!RaceManager.IsRaceStarted)
        {
            car.SetInputs(0, 0); // Lock all movement
            return;
        }

        // Get input - simple and reliable
        float horizontal = Input.GetAxis("Horizontal");  // A/D, Left/Right arrows
        float vertical = Input.GetAxis("Vertical");      // W/S, Up/Down arrows

        // Send both inputs at once - much cleaner!
        car.SetInputs(vertical, horizontal);
    }
}