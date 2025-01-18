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
        lastAttackTime = -attackCooldown; // Ustawienie, aby gracz m�g� zaatakowa� od razu
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
        // Wykrywamy wszystkich przeciwnik�w w obr�bie zasi�gu ataku
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            // Je�li trafimy w stworka, wywo�ujemy funkcj� przyjmuj�c� obra�enia w stworku
            FlyingCreature creature = enemy.GetComponent<FlyingCreature>();
            if (creature != null)
            {
                creature.TakeDamageFromPlayer(damage); // Stworek pada od razu po trafieniu
            }
            else
            {
                // Zadajemy obra�enia innym przeciwnikom
                enemy.GetComponent<Enemy>().takeDamage(damage, transform.position);
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}