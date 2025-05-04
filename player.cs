using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Properties")]
    public float moveVelocity = 3.8f;
    public float travelRange = 4.5f;
    public float pauseDuration = 0.4f;
    
    [Header("Visual Feedback")]
    public bool changeColorWithDirection = true;
    public Color upColor = new Color(0.2f, 0.8f, 0.2f);  // Green
    public Color downColor = new Color(0.8f, 0.2f, 0.2f); // Red
    public Color leftColor = new Color(0.2f, 0.2f, 0.8f); // Blue
    public Color rightColor = new Color(0.8f, 0.8f, 0.2f); // Yellow
    
    [Header("UI References")]
    public TMP_Text directionIndicator;
    
    private Vector3 homePosition;
    private NavigationDirection activeDirection;
    private NavigationDirection queuedDirection;
    private bool directionUpdatePending = false;
    private Renderer objectRenderer;
    
    // New properties for animation state tracking
    private enum MovementState { AtHome, MovingOut, AtDestination, MovingHome }
    private MovementState currentState = MovementState.AtHome;
    
    void Start()
    {
        homePosition = transform.position;
        activeDirection = NavigationDirection.Rightward;
        queuedDirection = activeDirection;
        
        // Getting renderer for color changes
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null && changeColorWithDirection)
        {
            UpdateObjectColor();
        }
        
        StartCoroutine(AnimateMovement());
    }
    
    public void QueueDirectionChange(NavigationDirection direction)
    {
        queuedDirection = direction;
        directionUpdatePending = true;
        
        // Log direction change request
        Debug.Log("Direction change requested: " + direction.ToString());
    }
    
    public IEnumerator AnimateMovement()
    {
        while (true)
        {
            // Updating direction if we're at home position and a change is requested
            if (currentState == MovementState.AtHome && directionUpdatePending)
            {
                activeDirection = queuedDirection;
                directionUpdatePending = false;
                UpdateDirectionUI();
                
                // Update object color if enabled
                if (changeColorWithDirection && objectRenderer != null)
                {
                    UpdateObjectColor();
                }
            }
        
            // Calculating destination based on active direction
            Vector3 destination = CalculateDestination(activeDirection);
            
            // Moving to destination
            currentState = MovementState.MovingOut;
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
            currentState = MovementState.AtDestination;
            yield return new WaitForSeconds(pauseDuration);
            
            // Returning to home position
            currentState = MovementState.MovingHome;
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
            currentState = MovementState.AtHome;
            
            // Small pause at home before starting next cycle
            yield return new WaitForSeconds(0.2f);
        }
    }
    
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
    
    private void UpdateDirectionUI()
    {
        if (directionIndicator != null)
        {
            directionIndicator.text = "Direction: " + activeDirection.ToString();
        }
    }
    
    // Adding new method for visual feedback
    private void UpdateObjectColor()
    {
        if (objectRenderer == null) return;
        
        Color directionColor;
        
        switch (activeDirection)
        {
            case NavigationDirection.Upward:
                directionColor = upColor;
                break;
            case NavigationDirection.Downward:
                directionColor = downColor;
                break;
            case NavigationDirection.Leftward:
                directionColor = leftColor;
                break;
            case NavigationDirection.Rightward:
                directionColor = rightColor;
                break;
            default:
                directionColor = Color.white;
                break;
        }
        
        // Applying  color to material
        if (objectRenderer.material != null)
        {
            objectRenderer.material.color = directionColor;
        }
    }
}