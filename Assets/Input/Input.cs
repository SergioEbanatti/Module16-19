using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Platformer
{


    public class Input : MonoBehaviour
    {
        public UnityEvent attackEvent = new();
        public UnityEvent jumpEvent = new();
        private Vector2 _move;

        public Vector2 Move
        {
            get { return _move; }
            set { _move = value; }
        }


        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }

        public void OnAttack()
        {
            attackEvent?.Invoke();
        }

        public void OnJump()
        {
            jumpEvent?.Invoke();
        }

    }
}