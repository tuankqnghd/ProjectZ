using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 5f;
    public float zoomSpeed = 20f;
    private float currentZoom = 40f;
    public Vector3 minBounds = new Vector3(-75f, -20f, -25f);
    public Vector3 maxBounds = new Vector3(75f, 50f, 125f);
    public Vector3 minBounds_origin = new Vector3(-75f, -20f, -25f);
    public Vector3 maxBounds_origin = new Vector3(75f, 50f, 125f);
    private Vector2 lastPanPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private Vector3 smoothDampVelocity = Vector3.zero;
    private float smoothDampTime = 0.3f;
    private Vector3 initialPosition; // Declare initialPosition here

    private bool doubleTapZoomIn = false;

    private void Start()
    {
        initialPosition = transform.position;
        // Set the initial camera position as the target position
        targetPosition = initialPosition;
    }

    private void Update()
    {
        // Camera panning
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
            Vector3 panTranslation = new Vector3(-deltaPosition.x, 0, -deltaPosition.y) * panSpeed * Time.deltaTime;
            targetPosition += panTranslation;
        }

        // Camera zooming
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitude = prevMagnitude - currentMagnitude;

            // Adjust the zoom by modifying Y and Z axes
            targetPosition = Camera.main.transform.position;
            targetPosition.y += deltaMagnitude * zoomSpeed * Time.deltaTime;
            targetPosition.z -= deltaMagnitude * zoomSpeed * Time.deltaTime;

            if (targetPosition.y > maxBounds_origin.y){
                targetPosition = new Vector3(targetPosition.x, maxBounds_origin.y, targetPosition.z + (targetPosition.y -  maxBounds_origin.y));
            }
            else if (targetPosition.y < minBounds_origin.y){
                targetPosition = new Vector3(targetPosition.x, minBounds_origin.y, targetPosition.z - (minBounds_origin.y - targetPosition.y));
            }

            float zoomLevel = (maxBounds_origin.y - targetPosition.y)/(maxBounds_origin.y - minBounds_origin.y)/2f;

            minBounds = new Vector3(minBounds_origin.x - zoomLevel*(maxBounds_origin.x - minBounds_origin.x),minBounds.y, minBounds_origin.z + zoomLevel*(maxBounds_origin.z - minBounds_origin.z)*0.747f);
            maxBounds = new Vector3(maxBounds_origin.x + zoomLevel*(maxBounds_origin.x - minBounds_origin.x),maxBounds.y, maxBounds_origin.z + zoomLevel*(maxBounds_origin.z - minBounds_origin.z)*1.131f);
        
            Debug.Log(minBounds.z);
        }
            // Debug.Log(minBounds);
            // Debug.Log(maxBounds);

            // targetPosition = ClampPosition(cameraPosition, initialPosition, minBounds, maxBounds);
            // cameraPosition = ClampPosition(cameraPosition, initialPosition, minBounds, maxBounds);
            // Camera.main.transform.position = cameraPosition;

            // Clamp the target position within the specified bounds
            targetPosition = ClampPosition(targetPosition, initialPosition, minBounds, maxBounds);
            // Smoothly move the camera towards the target position
            // Use Vector3.SmoothDamp for smoother movement
            transform.position = Vector3.SmoothDamp(transform.position,  targetPosition, ref smoothDampVelocity, smoothDampTime);
    }

    private Vector3 ClampPosition(Vector3 position, Vector3 referencePos, Vector3 min, Vector3 max)
    {
        position.x = Mathf.Clamp(position.x, referencePos.x + min.x, referencePos.x + max.x);
        position.y = Mathf.Clamp(position.y, referencePos.y + min.y, referencePos.y + max.y);
        position.z = Mathf.Clamp(position.z, referencePos.z + min.z, referencePos.z + max.z);
        return position;
    }
}