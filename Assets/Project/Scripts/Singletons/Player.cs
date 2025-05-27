using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private int _maxHealth;
    private int _health;

    [SerializeField] private Image _healthBar;
    private void Start()
    {
        Instance = this;
        _health = _maxHealth;
    }
    public void GetDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            GameManager.Instance.EndGame();
        }
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        _healthBar.fillAmount = (float)_health / (float)_maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinalCollider"))
        {
            GameManager.Instance.EndGame(true);
        }
    }
}
