using UnityEngine;

namespace KeystrokesController
{
    public class KeystrokesController : MonoBehaviour
    {
        protected static Keystrokes _keystrokes;
        protected Rigidbody _rigidbody;

        private void Awake()
        {
            _keystrokes = new Keystrokes();
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        public static void DisableInput() => _keystrokes.Disable();
        public static void EnableInput() => _keystrokes.Enable();
    }
}