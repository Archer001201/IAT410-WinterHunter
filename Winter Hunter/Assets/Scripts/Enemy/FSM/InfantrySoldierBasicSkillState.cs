using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Enemy.FSM
{
    public class InfantrySoldierBasicSkillState : BaseState
    {
        private IEnumerator _attackCoroutine;
        
        public override void OnEnter(BaseEnemy enemy)
        {
            // throw new System.NotImplementedException();
            Debug.Log("Basic Skill");
            CurrentEnemy = enemy;
            CurrentEnemy.StartCurrentCoroutine(CurrentEnemy.BasicSkillCoroutine, CurrentEnemy.BasicSkill);
            // StartAttackCoroutine();
        }

        public override void OnUpdate()
        {
            // throw new System.NotImplementedException();
            // if (CurrentEnemy is InfantrySoldier { isThrustReady: true } infantrySoldier)
            //     infantrySoldier.SwitchAttackingState(AttackingState.BasicAttack);
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
            // if (CurrentEnemy is InfantrySoldier infantrySoldier)
            //     infantrySoldier.isLungeReady = false;
            CurrentEnemy.StopCurrentCoroutine(CurrentEnemy.BasicSkillCoroutine);
            CurrentEnemy.basicSkillTimer = CurrentEnemy.basicSkillCooldown;
        }
        
        // private async Task AttackCoroutine()
        // {
        //     // if (CurrentEnemy is InfantrySoldier infantrySoldier)
        //     // {
        //     //     var thrustVfx = infantrySoldier.spearVfx;
        //     //     if (!thrustVfx.activeSelf)
        //     //     {
        //     //         thrustVfx.SetActive(true);
        //     //         Debug.Log("thrust");
        //     //     }
        //     //
        //     //     yield return new WaitForSeconds(2f);
        //     //    
        //     //     Debug.Log("switch to non attack");
        //     //     infantrySoldier.SwitchAttackingState(AttackingState.NonAttack);
        //     // }
        //     // var acc = 8;
        //     // CurrentEnemy.agent.acceleration = acc * 10f;
        //     // Debug.Log("Agent: " + CurrentEnemy.agent.speed + ", Enemy: " + CurrentEnemy.speed);
        //     if (CurrentEnemy is InfantrySoldier infantrySoldier)
        //     {
        //         infantrySoldier.isLunging = true;
        //         Debug.Log("lunge start");
        //         // yield return new WaitForSeconds(10f);
        //         await Task.Delay(2);
        //         infantrySoldier.isLunging = false;
        //         Debug.Log("lunge end");
        //         CurrentEnemy.SwitchAttackingState(AttackingState.NonAttack);
        //     }
        //     
        // }
        //
        // private void StartAttackCoroutine()
        // {
        //     _attackCoroutine = AttackCoroutine();
        // }
    }
}
