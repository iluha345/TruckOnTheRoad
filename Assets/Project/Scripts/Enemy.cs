using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int maxHealth;
    private int health;

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Player.Instance.GetDamage(_damage);
            Die();
        }
    }
}
