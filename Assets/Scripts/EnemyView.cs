using UnityEngine;

namespace ADV_2D
{
    [RequireComponent(typeof(Animator))]
    public class EnemyView : MonoBehaviour
    {
        private readonly int IsChangingDirection = Animator.StringToHash("IsChangingDirection");

        private Animator _animator;
        private Enemy _enemy;
        private float _xDirection;

        public void Initialize(Enemy enemy)
        {
            _animator = GetComponent<Animator>();
            _enemy = enemy;
            _xDirection = 1;
        }

        private void LateUpdate()
        {
            if (_enemy.transform.right.x != _xDirection)
            {
                _xDirection = _enemy.transform.right.x;
                _animator.SetTrigger(IsChangingDirection);
            }
        }
    }
}