using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    GameObject target;
    public Transform borderCheck;
    public int enemyHP = 100;
    public Animator animator;
    public Slider enemyHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar.value = enemyHP;
        target = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(0.45f, 0.45f);
        }
        else
        {
            transform.localScale = new Vector2(-0.45f, 0.45f);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        enemyHealthBar.value = enemyHP;
        if(enemyHP > 0)
        {
            //Play Hit Animation
            animator.SetTrigger("damage");
        }
        else
        {
            //Play Death Animation
            animator.SetTrigger("death");
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }
    
    public void PlayerDamage()
    {
        if (!target.GetComponent<PlayerCollision>().isInvincible)
        {
            target.GetComponent<PlayerCollision>().TakeDamage();
        }
    }
}
