using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(SliderJoint2D))]
    /// <summary>
    /// Движение вертикальной платформы
    /// </summary>
    public class VerticalSliderPlatform : MonoBehaviour
    {
        [SerializeField] private float _delayTime = 2f;
        private SliderJoint2D _sliderJoint;
        private bool _isLimitReached;


        private void Start()
        {
            _sliderJoint = GetComponent<SliderJoint2D>();
        }

        private void FixedUpdate()
        {
            if (!_isLimitReached &&
                (_sliderJoint.limitState == JointLimitState2D.LowerLimit ||
                 _sliderJoint.limitState == JointLimitState2D.UpperLimit))
            {
                ReachingFlagChange();
                StartCoroutine(Delay());
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(_delayTime);
            ChangeMoveDirection();
        }

        private void ReachingFlagChange()
        {
            _isLimitReached = !_isLimitReached;
        }

        private void ChangeMoveDirection()
        {
            var motor = _sliderJoint.motor;

            motor.motorSpeed = -motor.motorSpeed;
            _sliderJoint.motor = motor;
            ReachingFlagChange();
        }
    }
}