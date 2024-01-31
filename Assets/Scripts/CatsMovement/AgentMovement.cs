using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    private Vector3 _targetPositions;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        SetTargetPositions();
        SetAgentPosition();
    }

    private void SetTargetPositions()
    {
        if(Input.GetMouseButtonDown(0))
        {
           
            _targetPositions = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void SetAgentPosition()
    {
       
        _agent.SetDestination(new Vector3(_targetPositions.x, _targetPositions.y, transform.position.z));
    }
}
