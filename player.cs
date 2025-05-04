using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Changing property names and default values
    public float moveVelocity = 3.8f;
    public float travelRange = 4.5f;
    
    private Vector3 homePosition;
    private NavigationDirection activeDirection;
    private NavigationDirection queuedDirection;
    
    // Adding new property
    public bool isPaused = false;
    
    public TMP_Text directionIndicator;
    
    void Start()
    {
        homePosition = transform.position;
        activeDirection = NavigationDirection.Rightward;
        queuedDirection = activeDirection;
        
        // Begin movement sequence
        StartCoroutine(AnimateMovement());
    }
    
    // Changing method name from SetNewDirection to QueueDirectionChange
    public void QueueDirectionChange(NavigationDirection direction)
    {
        queuedDirection = direction;
        // No direction change flag - will implement in next commit
    }
    
    // Basic movement stub - will expand in next commit
    public IEnumerator AnimateMovement()
    {
        Debug.Log("Movement animation initialized");
        yield return null;
    }
}