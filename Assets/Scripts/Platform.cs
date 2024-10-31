using UnityEngine;

namespace ADV_2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float _touchTime;
        [SerializeField] private LayerMask _reactLayer;

        private ObstacleChecker _playerChecker;
        private Rigidbody2D _rigidbody;
        private float _timer;

        public void Initialize()
        {
            _playerChecker = new ObstacleChecker(_reactLayer, GetComponent<BoxCollider2D>(), Vector2.up);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _playerChecker.Update();
            
            if (_playerChecker.IsTouches)
                _timer += Time.deltaTime;
            else if (_timer > 0)
                _timer = 0;

            if (_timer >= _touchTime)
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}