using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset; // Offset between the camera and the player

    void Update()
    {
        if (target != null)
        {
            // Update the camera's position to follow the player with the offset
            transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        }
    }
}