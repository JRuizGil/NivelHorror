using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;  // Da�o que inflige la bala

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la bala impacta contra el enemigo
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);  // Hacer da�o al enemigo
        }

        // Destruir la bala tras el impacto
        Destroy(gameObject);
    }
}