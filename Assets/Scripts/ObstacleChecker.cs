using UnityEngine;

public class ObstacleChecker
{
    private readonly float _distanceToCheck = 0.1f;
    private readonly  LayerMask _obstacleMask;
    private readonly BoxCollider2D _collider;
    private readonly  Vector2 _direction;
    
    public bool IsTouches { get; private set; }

    public ObstacleChecker(LayerMask obstacleMask, BoxCollider2D collider, Vector2 direction)
    {
        _obstacleMask = obstacleMask;
        _collider = collider;
        _direction = direction;
    }

    public void Update()
    {
        IsTouches = Physics2D.BoxCast(_collider.bounds.center, _collider.size, 0, _direction, _distanceToCheck,
            _obstacleMask);
    }
}