﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int mentalPower;
    public int physicalPower;
    public int[] mentalWeekPoints;
    public int[] physicalWeekPoints;

    public GameObject mentalPowerText;
    public GameObject physicalPowerText;

    GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        mentalPowerText.GetComponent<Text>().text = mentalPower.ToString();
        physicalPowerText.GetComponent<Text>().text = physicalPower.ToString();

        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string calcDamege( GameManager.AttackType attackType, GameManager.AttackTypeMental attackTypeMental, GameManager.AttackTypePhysical attackTypePhysical)
    {
        string result = "敵は";

        if (attackType == GameManager.AttackType.MENTAL)
        {
            mentalPower -= mentalWeekPoints[(int)attackTypeMental];
            mentalPowerText.GetComponent<Text>().text = mentalPower.ToString();

            result += "精神に" + mentalWeekPoints[(int)attackTypeMental].ToString() + "のダメージを受けた";
        }

        if ( attackType == GameManager.AttackType.PHYSICAL)
        {
            physicalPower -= physicalWeekPoints[(int)attackTypePhysical];
            physicalPowerText.GetComponent<Text>().text = physicalPower.ToString();
            result += "体に" + physicalWeekPoints[(int)attackTypePhysical].ToString() + "のダメージを受けた";
        }

        if ( mentalPower <= 0 || physicalPower <= 0)
        {
            result += "死んだ";
        }
        else
        {
            gameController.GetComponent<GameManager>().AttackToPlayer();
        }

        

        return result;
    }
}
