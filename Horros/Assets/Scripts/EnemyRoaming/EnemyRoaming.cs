using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyRoaming 
{
    private List<NavigationPoint> _navigationPoints;
    private readonly NavMeshAgent _agent;
    private int _index;
    private int _id;
    private GameObject _player;

    public EnemyRoaming(NavMeshAgent navMeshAgent, GameObject player)
    {
        _agent = navMeshAgent;
        _player = player;
    }

    public int ID => _id;
    
    void NextIndex()
    {
        if (_index >= _navigationPoints.Count - 1)
            _index = 0;
        else
            _index++;
    }

    public void Roam()
    {
        if (Vector3.Distance(_agent.transform.position, _navigationPoints[_index].transform.position) < 1) 
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

    public void SetID(int id) => _id = id;

    public void FollowPlayer()
    {
        _agent.SetDestination(_player.transform.position);
    }
    
    public bool LookForPlayer()
    {
        if (Vector3.Distance(_agent.transform.position, _player.transform.position) > 10)
        {
            return false;
        }

        if (Vector3.Angle(_agent.transform.forward, _player.transform.position - _agent.transform.position) < 45)
        {
            RaycastHit hit;
            Physics.Raycast(
                new Ray(_agent.transform.position, (_player.transform.position - _agent.transform.position).normalized),
                out hit, 10);

            if (hit.collider.CompareTag("Player"))
                return true;
        }

        return false;
    }
}
