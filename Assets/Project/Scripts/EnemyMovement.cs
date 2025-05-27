using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _speed;

    private bool playerInRange = false;

    private Animator _anim;
    private Rigidbody _rb;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.GameStateEnum.Paused)
            return;

        if (playerInRange && _target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            direction.y = 0;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * 5f);

            Vector3 newPosition = _rb.position + direction * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            _anim.SetBool("isRunning",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            _anim.SetBool("isRunning", false);
        }
    }
}
