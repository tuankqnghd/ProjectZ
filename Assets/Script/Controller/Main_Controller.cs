using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Controller : MonoBehaviour
{
    private Main_Character_Unit selectedUnit;
    private bool isDoubleTap = false;
    private float doubleTapTimeThreshold = 0.2f; // Adjust this value to set the double tap time threshold
    private float doubleTapTimer = 0f;

    private void Update()
    {
        if (Input.touchCount == 1 && selectedUnit)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!isDoubleTap)
                {
                    isDoubleTap = true;
                    doubleTapTimer = Time.time;
                }
                else
                {
                    if (Time.time - doubleTapTimer <= doubleTapTimeThreshold)
                    {
                        // Double tap detected
                        selectedUnit.SetDestination(Touch3D.GetTouchWorldPosition());
                        isDoubleTap = false;
                    }
                    else
                    {
                        // Reset double tap if the time threshold is exceeded
                        doubleTapTimer = Time.time;
                    }
                }
            }
        }
    }

    public void SelectUnit()
    {
        if (selectedUnit)
        {
            selectedUnit.SetSelected(false);
            selectedUnit = null;
        }
        else
        {
            GameObject mainCharacterObject = GameObject.FindGameObjectWithTag("MainCharacter");
            if (mainCharacterObject != null)
            {
                selectedUnit = mainCharacterObject.GetComponent<Main_Character_Unit>();
                if (selectedUnit != null)
                {
                    selectedUnit.SetSelected(true);
                }
            }
        }
    }
}