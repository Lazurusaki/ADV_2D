using UnityEngine;

public class Jumper
{
    private Rigidbody2D _rigidbody;
    private float _height;
    private float _timeToHeight;
    
    private float StartYVelocity => 2f * _height / +_timeToHeight;
    
    public Jumper(Rigidbody2D rigidbody, float maxHeight, float timeToMaxHeight)
    {
        _rigidbody = rigidbody;
        _height = maxHeight;
        _timeToHeight= timeToMaxHeight;
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, StartYVelocity);
    }
}
