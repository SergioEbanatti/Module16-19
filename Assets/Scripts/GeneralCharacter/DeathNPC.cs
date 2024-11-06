using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(DelayedDestroy))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(GetAnimationLength))]

    public class DeathNPC : MonoBehaviour
    {
        private GetAnimationLength _animationLengthComponent;
        private DelayedDestroy _delayedDestroy;

        private void Awake()
        {
            _animationLengthComponent = GetComponent<GetAnimationLength>();
            _delayedDestroy = GetComponent<DelayedDestroy>();
        }

        public void Death()
        {
            var animationLength = _animationLengthComponent?.GetCurrentAnimationLength() ?? 0f;

            if (!TryGetComponent<PlayerMovement>(out _) && _delayedDestroy != null)
                _ = StartCoroutine(_delayedDestroy.Delay(gameObject, animationLength, _ => Destroy(gameObject)));
        }

    }
}