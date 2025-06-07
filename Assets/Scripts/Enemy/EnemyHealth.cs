using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Salud y Daño")]
    public int maxHealth = 3;
    private int currentHealth;
    public float health = 50;
    public int damageAmount = 10;

    [Header("Ataque")]
    public float hitPauseTime = 1f;
    public float attackRange = 5f;

    [Header("Detección de Suelo")]
    public float groundCheckDistance = 2f;
    public LayerMask groundLayer;

    public bool isPaused = false;
    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Ray ray = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(ray, groundCheckDistance, groundLayer))
        {
            Debug.Log("Enemigo sin suelo. Eliminado.");
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("¡Impacto en el enemigo! Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Debug.Log("Enemigo eliminado");
        Destroy(gameObject);
    }

    private IEnumerator HitPause()
    {
        isPaused = true;
        Debug.Log("Enemigo en pausa después del golpe.");
        yield return new WaitForSeconds(hitPauseTime);
        isPaused = false;
        Debug.Log("El enemigo vuelve a perseguir.");
    }

    // Método para reiniciar la pausa y corutinas
    public void ResetPause()
    {
        isPaused = false;
        StopAllCoroutines();
        Debug.Log("Enemigo pausa reiniciada.");
    }
}
