using UnityEngine;

public class Sqare : Enemy
{
    public override int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public override int Damage { get => _damage; set => _damage = value; }    

    public override void Attack(IDamageable target, int damage)
    {
        target.GetDamage(damage);
    }

    public override void GetDamage(int damage)
    {
        Debug.Log("Кубику болльно и обидно :(");
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.GetComponent<IDamageable>().GetDamage(_damage);
        }
    }
}
