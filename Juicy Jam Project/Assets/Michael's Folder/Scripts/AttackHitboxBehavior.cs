using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxBehavior : MonoBehaviour
{
    //Variables
    private Vector3 relativePosToPlayer;
    public float attackTime;
    public float attackRadius;
    public float attackDamage;

    //List that contains all already damaged enemies
    private List<int> HitEnemies { get; set; }
    //Components
    private Transform attackHitboxTransform;

    //GameObjects
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        attackHitboxTransform = GetComponent<Transform>();
        HitEnemies = new List<int>();
        Invoke(nameof(FinishAttack), attackTime);
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        //set position
        attackHitboxTransform.position = player.transform.position + relativePosToPlayer;

        //Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitboxTransform.position, 0.5f);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.attachedRigidbody.gameObject.tag == "Enemy")
            {
                if (!HitEnemies.Contains(enemy.gameObject.GetInstanceID()))
                {
                    HitEnemies.Add(enemy.gameObject.GetInstanceID());
                    enemy.GetComponent<EnemyHealthHandler>().healthSystem.Damage(attackDamage);
                }
            };
        }
    }

    public void SetRelativePos(Vector3 relativePos)
    {
        relativePosToPlayer = relativePos;
    }

    public void FinishAttack()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if(attackHitboxTransform.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackHitboxTransform.position, attackRadius);
    }
}
