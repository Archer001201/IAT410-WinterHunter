using Snowball;
using UnityEngine;

namespace Enemy
{
    public class DummyEnemy : BasicEnemy
    {
        // private void Update()
        // {
        //     if (health <= 0) Destroy(gameObject);
        // }
        //
        // private void OnCollisionEnter(Collision other)
        // {
        //     var otherGO = other.gameObject;
        //    
        //     if (otherGO.CompareTag("Projectile"))
        //     {
        //         var snowballScript = otherGO.GetComponent<BasicSnowball>();
        //         var damage = snowballScript.damage;
        //         if (shield > 0)
        //         {
        //             if (snowballScript.type == SnowballType.ThrowingSnowball)
        //             {
        //                 damage *= resistance;
        //             }
        //
        //             if (damage > shield)
        //             {
        //                 var overflowDamage = damage - shield;
        //                 shield = 0;
        //                 health -= overflowDamage;
        //             }
        //             else
        //             {
        //                 shield -= damage;
        //             }
        //         }
        //         else
        //         {
        //             health -= damage;
        //         }
        //     }
        // }
    }
}
