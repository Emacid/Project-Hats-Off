using UnityEngine;

public class ColliderSync : MonoBehaviour
{
    public BoxCollider2D targetCollider; // Elle atanacak hedef BoxCollider2D
    private BoxCollider2D sourceCollider; // Kendi objenizin BoxCollider2D'si

    void Start()
    {
        // Kendi collider'�n�z� al
        sourceCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Hedef collider ile kendi collider'�n�z� kar��la�t�r
        if (targetCollider != null &&
            (targetCollider.bounds.size != sourceCollider.bounds.size || targetCollider.offset != sourceCollider.offset))
        {
            // De�i�iklik oldu�unda kendi collider'�n�z� g�ncelle
            sourceCollider.size = targetCollider.size;
            sourceCollider.offset = targetCollider.offset;
        }
    }
}
