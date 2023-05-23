using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Main_Character_Unit : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject selectedVisualGameObject;
    private bool isSelected;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        selectedVisualGameObject = transform.Find("Selected").gameObject;
        SetSelected(false);
    }

    public void SetDestination(Vector3 destinationPosition)
    {
        navMeshAgent.SetDestination(destinationPosition);
    }

    public void SetSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        selectedVisualGameObject.SetActive(isSelected);
    }
}
