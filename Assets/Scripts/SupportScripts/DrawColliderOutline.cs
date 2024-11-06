using UnityEngine;

[ExecuteInEditMode]
public class DrawColliderOutline : MonoBehaviour
{
    [SerializeField] private Color _outlineColor = Color.red;

    void OnDrawGizmos()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Gizmos.color = _outlineColor;
            if (collider is BoxCollider2D)
            {
                BoxCollider2D boxCollider = (BoxCollider2D)collider;
                Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
            }
            else if (collider is CircleCollider2D)
            {
                CircleCollider2D circleCollider = (CircleCollider2D)collider;
                Gizmos.DrawWireSphere(circleCollider.bounds.center, circleCollider.radius);
            }
        }
    }
}
