using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ExplosiveBarrel : MonoBehaviour
    {
        [SerializeField] private GameObject _parentObject;          // ������, ������� ����� ��������� ����� ������ (������������ ������)
        [SerializeField] private GameObject _explosionEffect;       // �������� ������
        [SerializeField] private float _explosionOffDelay = 0.2f;   // �������� ���������� ������� ������

        private Renderer _renderer;
        private Rigidbody2D _rigidbody2D;
        private DelayedDestroy _delayedDestroy;

        private void Awake()
        {
            // �������� ������ �� ���������� ��� �������������, ����� �������� ���������� ������
            _renderer = GetComponent<Renderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _delayedDestroy = GetComponent<DelayedDestroy>();

            if (_delayedDestroy == null)
                Debug.LogWarning("DelayedDestroy ����������� � " + gameObject.name);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // ���������, ��� ����������� �� � ������ (����� �������� �� �����)
            if (!collision.gameObject.CompareTag("Ground"))
            {
                if (_renderer != null)
                    _renderer.enabled = false;

                if (_rigidbody2D != null)
                    _rigidbody2D.simulated = false;

                if (_delayedDestroy != null)
                {
                    _delayedDestroy.ChangeObjectState(_explosionEffect);
                    StartCoroutine(DelayedExplosion());
                }
            }
        }

        private IEnumerator DelayedExplosion()
        {
            yield return new WaitForSeconds(_explosionOffDelay);
            Destroy(_parentObject);
        }

    }
}