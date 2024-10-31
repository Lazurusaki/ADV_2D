using UnityEngine;

namespace ADV_2D
{
    public class PlayerController
    {
        private readonly InputDetector _inputDetector;
        
        private Character _character;
        

        private float _xInput;

        public PlayerController(InputDetector inputDetector)
        {
            _inputDetector = inputDetector;
        }

        public void SetCharacter(Character character)
        {
            _character = character;
        }
        
        public void Update()
        {
            if (_character is not null)
            {
                _xInput = _inputDetector.HorizontalInput;

                if (_inputDetector.IsJumpPressed && _character.IsWantToJump == false)
                    _character.IsWantToJump = true;
            }
        }

        public void FixedUpdate()
        {
            if (_character is not null)
            {
                if (_xInput != 0)
                    _character.StartMoving(new Vector2(_xInput, 0));
                else
                    _character.StopMoving();
            }
        }
    }
}