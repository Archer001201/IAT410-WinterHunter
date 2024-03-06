using System.Collections;
using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class InfantrySoldierBasicAttackState : BaseState
    {
        // private IEnumerator _attackCoroutine;
        
        public override void OnEnter(BaseEnemy enemy)
        {
            // throw new System.NotImplementedException();
            Debug.Log("Basic Attack");
            CurrentEnemy = enemy;
            // StartAttackCoroutine();
            // _attackCoroutine ??= 
            CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.BasicAttackCoroutine, CurrentEnemy.BasicAttack);
        }

        public override void OnUpdate()
        {
            // // throw new System.NotImplementedException();
            // if (CurrentEnemy is InfantrySoldier infantrySoldier)
            // {
            //     var thrustVfx = infantrySoldier.spearVfx;
            //     if (!thrustVfx.activeSelf)
            //     {
            //         thrustVfx.SetActive(true);
            //         Debug.Log("thrust");
            //     }
            //    
            //     if (infantrySoldier.isThrustStopped)
            //     {
            //         Debug.Log("switch to non attack");
            //         infantrySoldier.SwitchAttackingState(AttackingState.NonAttack);
            //     }
            // }
            // _attackCoroutine ??= StartCoroutine();
            // // StartCoroutine()
            // if (_attackCoroutine != null && _attackCoroutine.MoveNext() == false)
            // {
            //     _attackCoroutine = null;
            // }
        }

        public override void OnFixedUpdate()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnExist()
        {
            // throw new System.NotImplementedException();
            CurrentEnemy.StopCurrentCoroutine(CurrentEnemy.BasicAttackCoroutine);
            CurrentEnemy.basicAttackTimer = CurrentEnemy.basicAttackCooldown;
        }

        // private IEnumerator AttackCoroutine()
        // {
        //     if (CurrentEnemy is InfantrySoldier infantrySoldier)
        //     {
        //         var thrustVfx = infantrySoldier.spearVfx;
        //         if (!thrustVfx.activeSelf)
        //         {
        //             thrustVfx.SetActive(true);
        //             Debug.Log("thrust");
        //         }
        //
        //         yield return new WaitForSeconds(2f);
        //        
        //         Debug.Log("switch to non attack");
        //         infantrySoldier.SwitchAttackingState(AttackingState.NonAttack);
        //     }
        // }

        // private void StartAttackCoroutine()
        // {
        //     _attackCoroutine = AttackCoroutine();
        // }
    }
}
