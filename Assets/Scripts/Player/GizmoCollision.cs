using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(PlayerMovement))]

    public class GizmoCollision : MonoBehaviour
    {
        [SerializeField] private bool _drawGizmo = true;
        [SerializeField] private Color _gizmoColor = Color.yellow;
        [SerializeField] private PlayerMovement _playerMovement;

        private void OnDrawGizmos()
        {
            if (_drawGizmo && _playerMovement != null)
            {
                Gizmos.color = _gizmoColor;
                Vector3 position = _playerMovement.ColliderTransform.position;
                Gizmos.DrawWireSphere(position, _playerMovement.CollisionOffset);
            }
        }
    }
}