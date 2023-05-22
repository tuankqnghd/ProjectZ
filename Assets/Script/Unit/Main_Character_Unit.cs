using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Main_Character_Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private NavMeshAgent navMeshAgent;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(Touch3D.GetTouchWorldPosition());
    }
}
