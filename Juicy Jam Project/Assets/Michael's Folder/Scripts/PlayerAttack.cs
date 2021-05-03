using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Variables
    private bool attacking;
    public float attackCooldown;

    //GameObjects
    private GameObject crosshair;
    public GameObject attackHitbox;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        crosshair = GameObject.Find("Crosshairs");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !attacking)
        {
            //create Hitbox
            attacking = true;
            Vector3 vectorToMouse = crosshair.transform.position - gameObject.transform.position;
            vectorToMouse.Normalize();

            GameObject newattackHitbox = Instantiate(this.attackHitbox);
            AttackHitboxBehavior newAttackHitboxBehavior = newattackHitbox.GetComponent<AttackHitboxBehavior>();
            newAttackHitboxBehavior.SetRelativePos(vectorToMouse);

            Invoke(nameof(resetAttacking), attackCooldown);
        }
    }

    private void resetAttacking()
    {
        attacking = false;
    }
}
