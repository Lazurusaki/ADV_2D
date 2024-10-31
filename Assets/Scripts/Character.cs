using UnityEngine;

namespace ADV_2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _timeToJumpHeight;
        [SerializeField] private CharacterView _view;
        [SerializeField] private BoxCollider2D _wallCollider;
        [SerializeField] private LayerMask _groundMask;

        private Mover _mover;
        private Jumper _jumper;
        private ObstacleChecker _groundChecker;
        private ObstacleChecker _ceilChecker;
        private ObstacleChecker _wallChecker;
        private Rigidbody2D _rigidbody;

        public bool IsGrounded => _groundChecker.IsTouches;
        public float XVelocity => _rigidbody.velocity.x;
        public float YVelocity => _rigidbody.velocity.y;
        private float _baseGravity => 2f * _jumpHeight / (_timeToJumpHeight * _timeToJumpHeight);
        private bool _isInitialized;

        public bool IsDead { get; private set; }
        public bool IsWantToJump;

        private void OnValidate()
        {
            _speed = Mathf.Max(_speed, 0);
            _jumpHeight = Mathf.Max(_jumpHeight, 0);
            _timeToJumpHeight = Mathf.Max(_timeToJumpHeight, 0);
        }

        public void Initialize()
        {
            if (_view is null)
                throw new System.NullReferenceException("View is not set");

            if (_groundMask == 0)
                throw new System.NullReferenceException("Ground mask is not set");

            _rigidbody = GetComponent<Rigidbody2D>();
            _mover = new Mover(_rigidbody);
            _groundChecker = new ObstacleChecker(_groundMask, GetComponent<BoxCollider2D>(), Vector2.down);
            _ceilChecker = new ObstacleChecker(_groundMask, GetComponent<BoxCollider2D>(), Vector2.up);
            _wallChecker = new ObstacleChecker(_groundMask, _wallCollider, Vector2.down);
            _jumper = new Jumper(_rigidbody, _jumpHeight, _timeToJumpHeight);
            _view.Initialize(this);
            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _groundChecker.Update();
                _ceilChecker.Update();
                _wallChecker.Update();
            }
        }

        private void FixedUpdate()
        {
            if (_isInitialized)
            {
                HandleGravity();
                HandleJump();
                HandleCeil();
                LookToVelocity(_rigidbody.velocity.x);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent(out IDamagable damagable))
            {
                Die();
            }
        }

        private void Die()
        {
            IsDead = true;
        }
        
        private void HandleJump()
        {
            if (IsWantToJump && (_groundChecker.IsTouches || _wallChecker.IsTouches))
                _jumper.Jump();

            IsWantToJump = false;
        }

        private void HandleGravity()
        {
            if (_groundChecker.IsTouches)
            {
                _rigidbody.velocity = Vector2.right * _rigidbody.velocity.x;
            }
            else
            {
                float yVelocity = _rigidbody.velocity.y - _baseGravity * Time.fixedDeltaTime;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, yVelocity);
            }
        }

        private void HandleCeil()
        {
            if (_ceilChecker.IsTouches)
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Min(0, _rigidbody.velocity.y));
        }
        
        private void LookToVelocity(float xVelocity)
        {
            if (xVelocity != 0)
                transform.right = Vector3.right * xVelocity;
        }

        public void StartMoving(Vector2 direction)
        {
            _mover.StartMoving(direction, _speed);
        }

        public void StopMoving()
        {
            _mover.StopMoving();
        }

        public void Reset()
        {
            IsDead = false;
        }
    }
}