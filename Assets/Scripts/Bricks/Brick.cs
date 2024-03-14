using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : BrickParent
{

    [SerializeField] Transform explosion;

    public override void TakeDamage(int damageAmount)
    {
        print("Taking damage in child");

        hitPoints -= damageAmount;

        if(hitPoints <= 0)
        {
            DestroyBrick();
        }
        else
        {
            DamageBrick();
        }
        
        base.TakeDamage(damageAmount);
    }

    void DestroyBrick()
    {
        GameManager.i.UpdateNumberOfBricks();
        GameManager.i.UpdateScore(pointValue);

        var go = Instantiate(explosion, transform.position, transform.rotation);

        Destroy(go.gameObject, 2.25f);

        Destroy(gameObject);

        /*if (Random.Range(1, 10) == 1)
        {
            
        }*/
    }
}
