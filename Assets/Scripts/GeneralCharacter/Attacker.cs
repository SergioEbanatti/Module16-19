using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class Attacker : MonoBehaviour
    {
        [SerializeField] private GameObject _damageDealer;
        [SerializeField] private Transform _damagePoint;

        public void Attack()
        {
            Instantiate(_damageDealer, _damagePoint.position, Quaternion.identity);
        }

    }
}