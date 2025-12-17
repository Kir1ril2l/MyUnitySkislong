using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public abstract int MaxHealth { get; set; }
    public abstract int Damage { get; set; }

    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _damage;
    protected int _hp;

    public abstract void Attack(IDamageable target, int damage);

    public abstract void GetDamage(int damage);

    private void Awake()
    {
        _hp = _maxHealth;
    }
}
