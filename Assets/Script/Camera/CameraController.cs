using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 5f;
    public float zoomSpeed = 5f;
    public float zoomMin = 10f;
    public float zoomMax = 60f;
    private float currentZoom = 40f;
    public Vector3 minBounds = new Vector3(-50, -30, -50);
    public Vector3 maxBounds = new Vector3(50, 0, 50);

    private Vector2 lastPanPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private Vector3 smoothDampVelocity = Vector3.zero;
    private float smoothDampTime = 0.2f;
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
            Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition.y += deltaMagnitude * zoomSpeed * Time.deltaTime;
            cameraPosition.z -= deltaMagnitude * zoomSpeed * Time.deltaTime;
            Camera.main.transform.position = cameraPosition;
        }

        // Double-tap zoom in/out
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2)
        {
            if (!doubleTapZoomIn)
            {
                // Zoom in
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.y -= 50f;
                cameraPosition.z += 50f;
                Camera.main.transform.position = cameraPosition;
                doubleTapZoomIn = true;
            }
            else
            {
                // Zoom out
                Vector3 cameraPosition = Camera.main.transform.position;
                cameraPosition.y += 50f;
                cameraPosition.z -= 50f;
                Camera.main.transform.position = cameraPosition;
                doubleTapZoomIn = false;
            }
        }

        // Clamp the target position within the specified bounds
        targetPosition = ClampPosition(targetPosition, initialPosition, minBounds, maxBounds);

        // Smoothly move the camera towards the target position
        // Use Vector3.SmoothDamp for smoother movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothDampVelocity, smoothDampTime);

        
    }

    private Vector3 ClampPosition(Vector3 position, Vector3 referencePos, Vector3 min, Vector3 max)
    {
        position.x = Mathf.Clamp(position.x, referencePos.x + min.x, referencePos.x + max.x);
        position.y = Mathf.Clamp(position.y, referencePos.y + min.y, referencePos.y + max.y);
        position.z = Mathf.Clamp(position.z, referencePos.z + min.z, referencePos.z + max.z);
        return position;
    }
}