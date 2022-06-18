using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DefenceArmy : MonoBehaviour
{

    public GameObject target = null;

    public KingdomController parentKingdom;

    public bool isAttacking = false;

    float timerInterval = 0.1f;
    float timer;

    float attackInterval = 1f;
    float timer2;

    public int dmg;

    public int health;
    float prevHealth;
    float dmgTimer;

    public Vector3 startPos;

    public GameObject globalManager;

    GameObject soundManager;

    GameObject damageModel;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");

        timerInterval = 0.0001f;
        attackInterval = 0.1f;
        timer2 = attackInterval;
        isAttacking = false;

        health = 100;

        damageModel = GameObject.Find(this.name + "/DamageModel");
        if (damageModel != null)
            damageModel.SetActive(false);
        dmgTimer = 0f;

        parentKingdom = this.GetComponentInParent<KingdomController>();

        if (target != null)
        {
            if (target.GetComponent<AttackArmy>().dmg != 0)
                dmg = GetComponentInParent<KingdomController>().defenceKnights / (target.GetComponent<AttackArmy>().dmg / 4);
            else
                dmg = GetComponentInParent<KingdomController>().defenceKnights;
        } else
        {
            dmg = GetComponentInParent<KingdomController>().defenceKnights;
        }
        startPos = parentKingdom.transform.position;

        soundManager = GameObject.Find("SoundManager");

    }

    // Update is called once per frame
    void Update()
    {
        if (prevHealth > health)
        {
            dmgTimer = 1f;
        }

        if (dmgTimer >= 0f)
        {
            dmgTimer -= 1f * Time.deltaTime;
            damageModel.SetActive(true);
        }
        else
        {
            damageModel.SetActive(false);
            prevHealth = health;
        }

        if (isAttacking == true)
        {
            timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
            if (timer <= 0)
            {
                timer = timerInterval;
                Vector3 pos = transform.position;
                if (target.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x)
                {
                    pos.x += 0.05f;
                }
                else if (target.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x)
                {
                    pos.x -= 0.05f;
                }

                if (target.GetComponent<Transform>().position.z > this.GetComponent<Transform>().position.z)
                {
                    pos.z += 0.05f;
                }
                else if (target.GetComponent<Transform>().position.z < this.GetComponent<Transform>().position.z)
                {
                    pos.z -= 0.05f;
                }

                transform.position = pos;

                if (transform.position.x <= target.GetComponent<Transform>().position.x + 1f &&
                        transform.position.x >= target.GetComponent<Transform>().position.x - 1f &&
                        transform.position.z <= target.GetComponent<Transform>().position.z + 1f &&
                        transform.position.z >= target.GetComponent<Transform>().position.z - 1f)
                {
                    timer2 -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
                    if (timer2 <= 0)
                    {
                        timer2 = attackInterval;
                        if (target.GetComponent<AttackArmy>().health - dmg <= 0)
                        {
                            target.GetComponent<AttackArmy>().health -= dmg;
                            target = null;
                            isAttacking = false;
                        }
                        else
                        {
                            target.GetComponent<AttackArmy>().health -= dmg;
                            health -= target.GetComponent<AttackArmy>().dmg / 2;
                        }
                    }
                
                }
            }
        } else
        {
            if (transform.position == startPos)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
                if (timer <= 0f)
                {
                    timer = timerInterval;
                    Vector3 pos = transform.position;
                    if (transform.position.x < startPos.x)
                        pos.x += 0.05f;
                    else if (transform.position.x > startPos.x)
                        pos.x -= 0.05f;

                    if (transform.position.z < startPos.z)
                        pos.z += 0.05f;
                    else if (transform.position.z > startPos.z)
                        pos.z -= 0.05f;
                    transform.position = pos;
                }
            }
        }

        if (health <= 0)
        {
            soundManager.GetComponent<SoundManager>().PlaySound(5);
            parentKingdom.defenceKnights = (int)(parentKingdom.defenceKnights / 10);
            timerInterval = 0.0001f;
            attackInterval = 0.1f;
            timer2 = attackInterval;
            transform.position = startPos;
            health = 100;
            isAttacking = false;
            this.gameObject.SetActive(false);
        }
    }

    public void Attack(GameObject k)
    {
      //  Debug.Log(k);
        isAttacking = true;

        startPos = parentKingdom.transform.position;

        timer = timerInterval;
        target = k;

        if (target.GetComponent<AttackArmy>().dmg >= 4)
            dmg = GetComponentInParent<KingdomController>().defenceKnights / ((int)(target.GetComponent<AttackArmy>().dmg / 4));
        else
            dmg = GetComponentInParent<KingdomController>().defenceKnights;
    }
}
