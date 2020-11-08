using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum AttackStatus
    {
        ATTACK_TYPE,
        ATTACK_TYPE_MENTAL,
        ATTACK_TYPE_PHYSICAL,
        ATTACK,
        ATTACKING,
        ATTACK_CHECK,
        ATTACK_ENEMY,
        ATTACK_ENEMY_CHECK,
    }

    public AttackStatus status;
    public GameObject statusText;
    public GameObject attackTypeText;
    public GameObject attackTargetText;
    public GameObject UIAttackType;
    public GameObject UIAttackMentalTarget;
    public GameObject UIAttackPhysicalTarget;
    public GameObject UIAttack;

    public GameObject enemy;
    public GameObject player;

    public float enemyAttackWaitTime = 2.0f;
    public float playerAttackWaitTime = 2.0f;

    public string AttackTypeSelectString;
    public string MentalTargetString;
    public string PhysicalTargetString;
    public string AttackingString;
    public string[] AttackTargetTypeString;
    public string[] MentalTargetTypeString;
    public string[] PhysicalTargetTypeString;

    public enum AttackType
    {
        MENTAL,
        PHYSICAL,
        NONE,
    }

    public enum AttackTypeMental
    {
        DIS,
        TRAUMA,
        NONE,
    }

    public enum AttackTypePhysical
    {
        HEAD,
        BODY,
        LEG,
        NONE,
    }

    public AttackType attackType = AttackType.NONE;
    public AttackTypeMental attackTypeMental = AttackTypeMental.NONE;
    public AttackTypePhysical attackTypePhysical = AttackTypePhysical.NONE;

    // Start is called before the first frame update
    void Start()
    {
        setStatusToAttackType();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStatusToAttackType()
    {
        status = AttackStatus.ATTACK_TYPE;
        UIAttackType.SetActive(true);
        UIAttackMentalTarget.SetActive(false);
        UIAttackPhysicalTarget.SetActive(false);
        UIAttack.SetActive(false);

        statusText.GetComponent<Text>().text = AttackTypeSelectString;
        attackTypeText.GetComponent<Text>().text = "";
        attackTargetText.GetComponent<Text>().text = "";

        attackType = AttackType.NONE;
        attackTypeMental = AttackTypeMental.NONE;
        attackTypePhysical = AttackTypePhysical.NONE;
    }

    public void setStatusToAttackTypeMental()
    {
        status = AttackStatus.ATTACK_TYPE_MENTAL;
        UIAttackType.SetActive(false);
        UIAttackMentalTarget.SetActive(true);
        UIAttackPhysicalTarget.SetActive(false);
        UIAttack.SetActive(false);

        statusText.GetComponent<Text>().text = MentalTargetString;
    }

    public void setStatusToAttackTypePhysical()
    {
        status = AttackStatus.ATTACK_TYPE_PHYSICAL;
        UIAttackType.SetActive(false);
        UIAttackMentalTarget.SetActive(false);
        UIAttackPhysicalTarget.SetActive(true);
        UIAttack.SetActive(false);

        statusText.GetComponent<Text>().text = PhysicalTargetString;
    }

    public void setStatusToAttackYesNo()
    {
        status = AttackStatus.ATTACK;
        UIAttackType.SetActive(false);
        UIAttackMentalTarget.SetActive(false);
        UIAttackPhysicalTarget.SetActive(false);
        UIAttack.SetActive(true);

        statusText.GetComponent<Text>().text = PhysicalTargetString;
    }

    public void setStatusToAttacking()
    {
        status = AttackStatus.ATTACKING;
        UIAttackType.SetActive(false);
        UIAttackMentalTarget.SetActive(false);
        UIAttackPhysicalTarget.SetActive(false);
        UIAttack.SetActive(false);

        statusText.GetComponent<Text>().text = AttackingString;
    }

    public void setAttackType(int type)
    {
        attackType = (AttackType)type;
        attackTypeText.GetComponent<Text>().text = AttackTargetTypeString[(int)attackType];


        switch (attackType)
        {
            case AttackType.MENTAL:
                setStatusToAttackTypeMental();
                break;
            case AttackType.PHYSICAL:
                setStatusToAttackTypePhysical();
                break;
        }
    }

    public void setAttackTypeMental(int type)
    {
        attackTypeMental = (AttackTypeMental)type;
        attackTargetText.GetComponent<Text>().text = MentalTargetTypeString[(int)attackTypeMental];
        setStatusToAttackYesNo();
    }

    public void setAttackTypePhysical(int type)
    {
        attackTypePhysical = (AttackTypePhysical)type;
        attackTargetText.GetComponent<Text>().text = PhysicalTargetTypeString[(int)attackTypePhysical];
        setStatusToAttackYesNo();
    }

    public void setAttackYesNo(int type)
    {
        if ( type == 0)
        {
            setStatusToAttacking();
            AttackToEnemy();
        }
        else if ( type == 1)
        {
            setStatusToAttackType();
        }
    }

    public void AttackToEnemy()
    {
        StartCoroutine("callAttackEnemy");
    }

    IEnumerator callAttackEnemy()
    {
        yield return new WaitForSeconds(enemyAttackWaitTime);
        string result = enemy.GetComponent<Enemy>().calcDamege(attackType, attackTypeMental, attackTypePhysical);

        statusText.GetComponent<Text>().text = result;
    }

    public void AttackToPlayer()
    {
        StartCoroutine("callAttackPlayer");
    }


    IEnumerator callAttackPlayer()
    {
        yield return new WaitForSeconds(playerAttackWaitTime);

        AttackType attackTypeFromEnemy = (AttackType)Random.Range(0, (int)AttackType.NONE);
        AttackTypeMental attackTypeMentalFromEnemy = (AttackTypeMental)Random.Range(0, (int)AttackTypeMental.NONE);
        AttackTypePhysical attackTypePhysicalFromEnemy = (AttackTypePhysical)Random.Range(0, (int)AttackTypePhysical.NONE);

        string result = player.GetComponent<Player>().calcDamege(attackType, attackTypeMental, attackTypePhysical);

        statusText.GetComponent<Text>().text = result;

        if ( player.GetComponent<Player>().mentalPower <= 0 || player.GetComponent<Player>().mentalPower <= 0)
        {
            yield return new WaitForSeconds(playerAttackWaitTime);
        }
        yield return new WaitForSeconds(playerAttackWaitTime);

        setStatusToAttackType();
    }
}
