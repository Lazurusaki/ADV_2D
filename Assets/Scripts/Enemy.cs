using UnityEngine;

namespace ADV_2D
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _speed;
        [SerializeField] private EnemyView _view;

        private Mover _mover;

        public void Initialize(Transform[] patrolPoints)
        {
            if (_view == null)
                throw new System.NullReferenceException("View is not set");
            
            _mover = new Mover(GetComponent<Rigidbody2D>());
            _view.Initialize(this);
        }

        public void MoveTo(Transform target)
        {
            _mover.StartMoving((target.position - transform.position).normalized, _speed);
        }

        public void LookTo(Transform target)
        {
            transform.right = Vector3.right * (target.position - transform.position).normalized.x;
        }
    }
}