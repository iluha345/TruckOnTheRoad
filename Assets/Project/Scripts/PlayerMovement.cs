using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.GameStateEnum.Paused)
            return;
        Movement();
    }
    private void Movement()
    {
        Vector3 move = transform.forward * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + move);
    }
}