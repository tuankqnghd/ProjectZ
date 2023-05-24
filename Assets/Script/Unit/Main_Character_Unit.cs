using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Main_Character_Unit : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject selectedVisualGameObject;
    private bool isSelected;
    private Main_Character_Animation_Controller animationController;

    private void Start()
    {
        // Assuming the Main_Character_Animation_Controller script is attached to the same GameObject as this OtherClass script
        animationController = GetComponent<Main_Character_Animation_Controller>();
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        selectedVisualGameObject = transform.Find("Selected").gameObject;
        SetSelected(false);
    }

    public void SetDestination(Vector3 destinationPosition)
    {
        animationController.switchMoving(true);
        navMeshAgent.SetDestination(destinationPosition);
        StartCoroutine(WaitForDestinationReached());
    }

    public void SetSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        selectedVisualGameObject.SetActive(isSelected);
    }

    private IEnumerator WaitForDestinationReached()
    {
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            yield return null;
        }

        animationController.switchMoving(false);
    }
}
