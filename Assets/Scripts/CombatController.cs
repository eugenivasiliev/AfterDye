using UnityEngine;

public class CombatController : MonoBehaviour
{
    public enum Team
    {
        FRIENDLY,
        ENEMY
    }

    [SerializeField] private Team team;
    [SerializeField] private int health;
    public int Health => health;
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    public bool isDead => health <= 0;

    public void Damage(int amount, out bool isDead)
    {
        health -= amount;
        isDead = this.isDead;
    }

    public void Damage(int amount) => Damage(amount, out _);

    public void Heal(int amount) => health = Mathf.Min(health + amount, maxHealth);
    public void FullHeal() => health = maxHealth;
}
