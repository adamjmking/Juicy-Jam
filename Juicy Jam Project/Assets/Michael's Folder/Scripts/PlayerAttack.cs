using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Variables
    private bool attacking;
    public bool ableToAttack;
    public float attackCooldown;

    //GameObjects
    public GameObject attackHitbox, club, clubPivot;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        ableToAttack = true;

        club.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !attacking && ableToAttack)
        {
            //create Hitbox
            attacking = true;
            Vector2 vectorToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
            vectorToMouse.Normalize();
            vectorToMouse.y = vectorToMouse.y * 2f;

            GameObject newattackHitbox = Instantiate(this.attackHitbox);
            AttackHitboxBehavior newAttackHitboxBehavior = newattackHitbox.GetComponent<AttackHitboxBehavior>();
            newAttackHitboxBehavior.SetRelativePos(vectorToMouse);

            //Attack swing
            club.SetActive(true);
            clubPivot.transform.localPosition = vectorToMouse;
            clubPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vectorToMouse.y, vectorToMouse.x) * Mathf.Rad2Deg - 120);

            Invoke(nameof(resetAttacking), attackCooldown);
        }
        if (attacking)
        {
            clubPivot.transform.Rotate(0, 0, 270 * Time.deltaTime);
        }
    }

    private void resetAttacking()
    {
        attacking = false;
        club.SetActive(false);
    }
}
