using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 0.25f; // Czas cooldownu ataku
    private float lastAttackTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        lastAttackTime = -attackCooldown; // Ustawienie, aby gracz móg³ zaatakowaæ od razu
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
        {
            anim.SetTrigger("Attack");
            AttackHit();
            lastAttackTime = Time.time;
        }
    }

    public void AttackHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(damage, transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}