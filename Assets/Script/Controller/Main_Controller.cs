using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Controller : MonoBehaviour
{
    private Main_Character_Unit selectedUnit;

    // Start is called before the first frame updat
    private void Awake() {
        selectedUnit = null;
    } 

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0){
            DeselectUnit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out RaycastHit raycastHit)) {
                if(raycastHit.collider.TryGetComponent<Main_Character_Unit>(out Main_Character_Unit main_Character_Unit)) {
                    selectedUnit = main_Character_Unit;
                    selectedUnit.SetSelected(true);
                }
            }
        }

        if (Input.touchCount == 1){
            selectedUnit.SetDestination(Touch3D.GetTouchWorldPosition());
        }
    }

    private void DeselectUnit() {
        selectedUnit.SetSelected(false);
        selectedUnit = null;
    }
}