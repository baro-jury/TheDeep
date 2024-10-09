using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    public abstract void Trigger2D(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger2D(collision);
    }
}
