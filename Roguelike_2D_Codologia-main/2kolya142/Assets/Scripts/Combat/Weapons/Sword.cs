using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private PlayerCombatSystem _playerCombatSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.GetComponent<IDamageable>().GetDamage(_playerCombatSystem.Damage);
        }
    }
}
