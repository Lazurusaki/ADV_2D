using System;
using UnityEngine;

namespace ADV_2D
{
    [RequireComponent(typeof(Animator))]
    public class CharacterView : MonoBehaviour
    {
        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsJumpingKey = Animator.StringToHash("IsJumping");
        private readonly int IsFallingKey = Animator.StringToHash("IsFalling");
        private readonly int IsGroundedKey = Animator.StringToHash("IsGrounded");

        private Animator _animator;
        private Character _character;

        public void Initialize(Character character)
        {
            _animator = GetComponent<Animator>();
            _character = character;
        }

        private void LateUpdate()
        {
            _animator.SetBool(IsRunningKey,_character.XVelocity != 0);
            _animator.SetBool(IsJumpingKey, _character.YVelocity > 0);
            _animator.SetBool(IsFallingKey, _character.YVelocity < 0);
            _animator.SetBool(IsGroundedKey, _character.IsGrounded);
        }
    }
}
