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
    private Vector2 lastPanPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private Vector3 smoothDampVelocity = Vector3.zero;
    private float smoothDampTime = 0.2f;

    private void Start()
    {
        targetPosition = transform.position; // Set the initial camera position as the target position
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

            currentZoom += deltaMagnitude * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, zoomMin, zoomMax);
            Camera.main.fieldOfView = currentZoom;
        }

        // Smoothly move the camera towards the target position
        // Use Vector3.SmoothDamp for smoother movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothDampVelocity, smoothDampTime);
    }
}
