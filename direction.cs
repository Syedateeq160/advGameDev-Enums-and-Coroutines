using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum NavigationDirection
{
    Upward,
    Downward,
    Leftward,
    Rightward
}

public class Direction : MonoBehaviour
{
    private const string VERSION = "1.0.0";
    
    public NavigationDirection _navigationDirection;
    public Player player;
    
    // Adding new UI button references
    [Header("UI Controls")]
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
    
    void Start()
    {
        Debug.Log("Direction controller initialized with version: " + VERSION);
        
        // Setting up button listeners
        SetupButtonListeners();
    }
    
    // Adding new method for UI setup
    private void SetupButtonListeners()
    {
        if (upButton != null)
            upButton.onClick.AddListener(() => SetDirectionUpward());
            
        if (downButton != null)
            downButton.onClick.AddListener(() => SetDirectionDownward());
            
        if (leftButton != null)
            leftButton.onClick.AddListener(() => SetDirectionLeftward());
            
        if (rightButton != null)
            rightButton.onClick.AddListener(() => SetDirectionRightward());
    }
    
    // Changing method names and implementations
    public void SetDirectionUpward() 
    {
        if (player != null)
            player.QueueDirectionChange(NavigationDirection.Upward);
    }
    
    public void SetDirectionDownward() 
    {
        if (player != null)
            player.QueueDirectionChange(NavigationDirection.Downward);
    }
    
    public void SetDirectionLeftward() 
    {
        if (player != null)
            player.QueueDirectionChange(NavigationDirection.Leftward);
    }
    
    public void SetDirectionRightward() 
    {
        if (player != null)
            player.QueueDirectionChange(NavigationDirection.Rightward);
    }
}