using UnityEngine;

public class PlayerCombatSystem : MonoBehaviour, IDamageable
{
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Damage { get => _damage; set => _damage = value; }

    [SerializeField] private Sword _sword;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    private int _hp;

    private void Awake()
    {
        _hp = MaxHealth;
    }

    public void Attack(IDamageable target, int damage)
    {
        
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        Debug.Log(_hp);
        if (_hp <= 0)
        {
            Debug.Log("Player is dead!");
        }
    }

    public void ActivateSword()
    {
        _sword.gameObject.SetActive(true);
    }

    public void DeactivateSword()
    {
        _sword.gameObject.SetActive(false);
    }
}
