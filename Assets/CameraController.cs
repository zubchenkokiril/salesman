using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 
    public float scrollSpeed = 2f; 
    public float minDistance = 1f; 
    public float maxDistance = 10f; 

    private float currentDistance;

    void Start()
    {
        
        currentDistance = offset.magnitude;
    }

    void Update()
    {
        HandleCameraZoom();
    }

    void LateUpdate()
    {
        
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position = player.position + direction * currentDistance;
        transform.LookAt(player);
    }

    void HandleCameraZoom()
    {
        
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
       
        currentDistance -= scrollInput * scrollSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }
}
