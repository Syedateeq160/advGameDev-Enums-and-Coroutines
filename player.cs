using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveVelocity = 3.8f;
    public float travelRange = 4.5f;
    
    private Vector3 homePosition;
    private NavigationDirection activeDirection;
    private NavigationDirection queuedDirection;
    
    // Adding new direction change flag with different name
    private bool directionUpdatePending = false;
    
    public bool isPaused = false;
    public TMP_Text directionIndicator;
    
    void Start()
    {
        homePosition = transform.position;
        activeDirection = NavigationDirection.Rightward;
        queuedDirection = activeDirection;
        StartCoroutine(AnimateMovement());
    }
    
    public void QueueDirectionChange(NavigationDirection direction)
    {
        queuedDirection = direction;
        directionUpdatePending = true;
    }
    
    //implementing coroutine with different approach
    public IEnumerator AnimateMovement()
    {
        while (true)
        {
            // Only proceed if not paused
            if (!isPaused)
            {
                // Calculate destination based on active direction
                Vector3 destination = CalculateDestination(activeDirection);
                
                // Updating UI display
                UpdateDirectionUI();
                
                // Moving to destination
                float remainingDistance = Vector3.Distance(transform.position, destination);
                while (remainingDistance > 0.05f)
                {
                    float step = moveVelocity * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, destination, step);
                    remainingDistance = Vector3.Distance(transform.position, destination);
                    yield return null;
                }
                
                // Ensuring exact position
                transform.position = destination;
                
                // Pausing at destination
                yield return new WaitForSeconds(0.4f);
                
                // Returning to home position
                remainingDistance = Vector3.Distance(transform.position, homePosition);
                while (remainingDistance > 0.05f)
                {
                    float step = moveVelocity * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, homePosition, step);
                    remainingDistance = Vector3.Distance(transform.position, homePosition);
                    yield return null;
                }
                
                // Ensuring exact home position
                transform.position = homePosition;
            }
            
            // Direction change logic will be implemented in next commit
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    // Adding new helper method
    private Vector3 CalculateDestination(NavigationDirection direction)
    {
        Vector3 targetPos = homePosition;
        
        switch (direction)
        {
            case NavigationDirection.Upward:
                targetPos += new Vector3(0, travelRange, 0);
                break;
            case NavigationDirection.Downward:
                targetPos += new Vector3(0, -travelRange, 0);
                break;
            case NavigationDirection.Leftward:
                targetPos += new Vector3(-travelRange, 0, 0);
                break;
            case NavigationDirection.Rightward:
                targetPos += new Vector3(travelRange, 0, 0);
                break;
        }
        
        return targetPos;
    }
    
    // Adding new helper method
    private void UpdateDirectionUI()
    {
        if (directionIndicator != null)
        {
            directionIndicator.text = "Current: " + activeDirection.ToString();
        }
    }
}