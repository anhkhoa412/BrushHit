using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AnchorManager anchorManager;
    public Vector3 offSet;
    private float smoothSpeed = 1.0f;

 
    void Start()
    {
        offSet = transform.position - anchorManager.currentAnchor.position;
      
    }

    void LateUpdate()
    {
        
        Vector3 desiredPosition = anchorManager.currentAnchor.position + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}