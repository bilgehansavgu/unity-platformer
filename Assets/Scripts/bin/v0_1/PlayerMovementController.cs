using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform hookTransform;
    private Rigidbody2D _rb;

    private LineRenderer _lineRenderer;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.02f;
        _lineRenderer.endWidth = 0.05f;
    }

    void Update()
    {
        if (hookTransform != null)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, hookTransform.position);
        }
        
        HandleHook();
        
        
    }

    private void HandleHook()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                hookTransform.position = hit.point;
                _rb.velocity = 3 * (hookTransform.position - transform.position);
                _lineRenderer.enabled = true;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            hookTransform.position = transform.position;
            _lineRenderer.enabled = false;
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        { 
            //TODO maybe implement a hook for the pull mechanic?
        }
    }
}