using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class ApplyDamageGlobal : MonoBehaviour
    {
        public static void ApplyDamage(Transform target, float damage)
        {
            target.TryGetComponent<Health>(out Health health);

            if (health != null)
                health.TakeDamage(damage);
        }
    }
}