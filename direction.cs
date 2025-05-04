using UnityEngine;
using System.Collections;

// Renamed enum from MovementDirection to NavigationDirection
public enum NavigationDirection
{
    // Renamed values but kept functionality
    Upward,
    Downward,
    Leftward,
    Rightward
}

public class Direction : MonoBehaviour
{
    // Adding version tracking
    private const string VERSION = "1.0.0";
    
    public NavigationDirection _navigationDirection;
    public Player player;
    
    // Adding debugging functionality
    void Start()
    {
        Debug.Log("Direction controller initialized with version: " + VERSION);
    }
}