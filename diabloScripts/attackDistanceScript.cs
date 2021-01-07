using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class attackDistanceScript : MonoBehaviour
{

    public static float attackDistancePlus;
    public static bool isMagicBallInGame;
    private void Awake()
    {
        attackDistancePlus = 15f;
    }
    void Update()
    {
        if (playerScript.attackingMode)
        {
            attackDistancePlus = 35f;
            StartCoroutine(changeAttackDistance());
            IEnumerator changeAttackDistance()
            {
                yield return new WaitForSeconds(3f);
                playerScript.attackingMode = false;
            }
        }
        else
        {
            attackDistancePlus = 15f;
        }
        
    }
}
