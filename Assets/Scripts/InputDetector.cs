using UnityEngine;

namespace ADV_2D
{
    public class InputDetector
    {
        private const string XAxisName = "Horizontal";
        private const KeyCode JumpKey = KeyCode.Space;

        public float HorizontalInput { get; private set; }
        public bool IsJumpPressed;

        public void Update()
        {
            HorizontalInput = Input.GetAxisRaw(XAxisName);
            IsJumpPressed = Input.GetKeyDown(JumpKey);
        }
    }
}