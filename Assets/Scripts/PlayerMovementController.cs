using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform hookTransform;
    private Rigidbody2D rb;

    private LineRenderer lineRenderer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.05f;
    }

    void Update()
    {
        if (hookTransform != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hookTransform.position);
        }
        
        HandleHook();
        
        
    }

    public void HandleHook()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                hookTransform.position = hit.point;
                rb.velocity = 3 * (hookTransform.position - transform.position);
                lineRenderer.enabled = true;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            hookTransform.position = transform.position;
            lineRenderer.enabled = false;
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        { 
            //TODO maybe implement a hook for the pull mechanic?
        }
    }
}