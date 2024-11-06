using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class GetAnimationLength : MonoBehaviour
    {
        public float GetCurrentAnimationLength(int layerIndex = 0)
        {
            if (TryGetComponent<Animator>(out Animator animator))
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);

                // ���������� ����� ��������, ���� ���� ��� �� �������
                return stateInfo.length;
            }
            Debug.LogWarning("Animator �� ������");
            return 0f; 
        }
    }
}