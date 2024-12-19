using UnityEngine;

public class ColliderSync : MonoBehaviour
{
    public BoxCollider2D targetCollider; // Elle atanacak hedef BoxCollider2D
    private BoxCollider2D sourceCollider; // Kendi objenizin BoxCollider2D'si

    void Start()
    {
        // Kendi collider'ýnýzý al
        sourceCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Hedef collider ile kendi collider'ýnýzý karþýlaþtýr
        if (targetCollider != null &&
            (targetCollider.bounds.size != sourceCollider.bounds.size || targetCollider.offset != sourceCollider.offset))
        {
            // Deðiþiklik olduðunda kendi collider'ýnýzý güncelle
            sourceCollider.size = targetCollider.size;
            sourceCollider.offset = targetCollider.offset;
        }
    }
}
