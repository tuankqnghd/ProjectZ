using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch3D : MonoBehaviour
{
    public static Touch3D Instance { get; private set; }

    [SerializeField] private LayerMask touchColliderLayerMask = new LayerMask();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, touchColliderLayerMask))
                {
                    transform.position = raycastHit.point;
                }
            }
        }
    }

    public static Vector3 GetTouchWorldPosition()
    {
        return Instance.GetTouchWorldPosition_Instance();
    }

    private Vector3 GetTouchWorldPosition_Instance()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, touchColliderLayerMask))
                {
                    return raycastHit.point;
                }
            }
        }

        return Vector3.zero;
    }
}