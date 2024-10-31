using ADV_2D;
using UnityEngine;

public class EnemyAiController
{
    private readonly Enemy _enemy;
    private readonly Transform[] _patrolPoints;
    private readonly float _reachedDeadZone = 0.55f;

    private int _currentPointIndex;
    private float _currentDirection;
    
    public EnemyAiController(Enemy enemy, Transform[] patrolPoints)
    {
        _enemy = enemy;
        _patrolPoints = patrolPoints;
        _currentPointIndex = 0;
    }

    private void ChangePatrolPoint()
    {
        if (_patrolPoints.Length > 1)
        {
            _currentPointIndex = _currentPointIndex == _patrolPoints.Length - 1 ? 0 : ++_currentPointIndex;
        }
    }

    public void FixedUpdate()
    {
        if (_enemy is not null && _patrolPoints.Length > 0)
        {
            if (Vector3.Distance(_patrolPoints[_currentPointIndex].position, _enemy.transform.position) <=
                _reachedDeadZone)
                ChangePatrolPoint();

            _enemy.MoveTo(_patrolPoints[_currentPointIndex]);
            _enemy.LookTo(_patrolPoints[_currentPointIndex]);
        }
    }
}