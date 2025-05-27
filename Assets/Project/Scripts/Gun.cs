using System.Collections;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _shootTransform;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _reloadTime;
    [SerializeField] private LayerMask _layerMask;

    private bool canShoot = true;

    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStateEnum.Paused)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _layerMask))
            {
                Vector3 lookPoint = hit.point;
                Vector3 direction = (lookPoint - transform.position);
                direction = direction.normalized;
                direction.y = 180f;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                
                transform.rotation = targetRotation;
                direction.y = 0f;
                targetRotation = Quaternion.LookRotation(direction);
                Shoot(targetRotation);
                
            }
        }
    }

    private void Shoot(Quaternion direction)
    {
        if (!canShoot) return;

        Bullet bullet = Instantiate(_bulletPrefab, _shootTransform.position, Quaternion.identity);
        bullet.transform.rotation = direction;
        canShoot = false;

        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_reloadTime);
        canShoot = true;
    }
}
