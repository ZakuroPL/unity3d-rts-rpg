using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan :  Unit, ISelectable
{
    public GameObject attackObject;
  
    protected override void Awake()
    {
        base.Awake();
        attackObject.SetActive(false);
    }
    public void SetSelected(bool selected)
    {
        selectBall.gameObject.SetActive(selected);
    }

    void Command (Vector3 destination)
    {
        agent.SetDestination(destination);
        task = Task.move;
        target = null;
    }
    void Command (GameObject enemyToFollow)
    {
        target = enemyToFollow.transform;
        task = Task.follow;
    }

    void activeAttackScript()
    {
        attackObject.SetActive(true);
        StartCoroutine(disactive());
        IEnumerator disactive()
        {
            yield return new WaitForSeconds(0.5f * Time.deltaTime);
            attackObject.SetActive(false);
        }
    }

}//end


