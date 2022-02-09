using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyRoaming : MonoBehaviour
{
    [SerializeField] List<NavigationPoint> _navigationPoints;

    int _index = 0;
    NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void NextIndex()
    {
        if (_index >= _navigationPoints.Count - 1)
            _index = 0;
        else
            _index++;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _navigationPoints[_index].transform.position) < 1) 
        {
            NextDestination();
        }
    }


    public void NextDestination()
    {
        NextIndex();
        _agent.SetDestination(_navigationPoints[_index].transform.position);
        _agent.isStopped = false;
    }

    public IEnumerator SetNavigationPoints(List<NavigationPoint> points)
    {
        _navigationPoints = points;
        yield return null;
        _agent.SetDestination(_navigationPoints[_index].transform.position);
    }
}
