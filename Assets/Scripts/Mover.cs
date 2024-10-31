using Unity.VisualScripting;
using UnityEngine;

namespace ADV_2D
{
    public class Mover
    {
        private readonly Rigidbody2D _rigidbody;

        private Vector2 _direction;

        public Mover(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void StartMoving(Vector2 direction, float speed)
        {
            _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);
        }

        public void StopMoving()
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
    }
}
