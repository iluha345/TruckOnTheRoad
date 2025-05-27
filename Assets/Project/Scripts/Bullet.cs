using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(DelayDie());
    }
    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().GetDamage(_damage);
        }
        Die();
    }
    IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(_lifeTime);
        Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
