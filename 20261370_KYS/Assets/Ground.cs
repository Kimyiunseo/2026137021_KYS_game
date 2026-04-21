using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ground : MonoBehaviour
{
    private void Awake()
    {
        int groundLayer = LayerMask.NameToLayer("Ground");

        gameObject.layer = groundLayer;
    }
}