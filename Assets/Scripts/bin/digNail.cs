using UnityEngine;

public class NailController : MonoBehaviour
{
    private Transform myTransform;
    private Collider2D topCollider;
    private float originalYPosition;
    private float moveDownAmount = 0.7f; // 1/3 of the collider's height
    private int maxHitCount = 3; // Number of hits needed to reach stop points
    private int currentHitCount = 0; // Count of hits
    private bool canMoveDown = true; // Flag to control movement
    private float hitVelocityThreshold = 4.0f;

    void Start()
    {
        myTransform = transform;
        topCollider = GetComponentInChildren<Collider2D>(); // Find the collider in the children
        originalYPosition = myTransform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && canMoveDown)
        {
            if (CanMoveDown(other.contacts))
            {
                Debug.Log(other.relativeVelocity.magnitude);
                if (other.relativeVelocity.magnitude > hitVelocityThreshold)
                {
                    MoveNailDown();
                }
                
            }
        }
    }

    bool CanMoveDown(ContactPoint2D[] contacts)
        {
            foreach (ContactPoint2D contact in contacts)
            {
                // Check if the collision point is at the top of the nail
                if (contact.point.y >= myTransform.position.y)
                {
                    return true;
                }
            }
            return false;
        }
        
    void MoveNailDown()
    {

        // Move the nail down
        myTransform.Translate(0, -moveDownAmount, 0);

        // Increment the hit count
        currentHitCount++;

        // Check if it's the last hit
        if (currentHitCount >= maxHitCount)
        {
            // Stop further movement
            canMoveDown = false;
            currentHitCount = 0; // Reset hit count for future us
        }
    }
}