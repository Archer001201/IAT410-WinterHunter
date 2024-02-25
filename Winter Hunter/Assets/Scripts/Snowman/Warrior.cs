using System.Collections;
using Snowman.Skills;
using UnityEngine;
using Utilities;

namespace Snowman
{
    public class Warrior : BaseSnowman
    {
        public GameObject slashPrefab;
        public float attackRange;
        private Coroutine _attackCoroutine;

        protected override void Update()
        {
            base.Update();
            if (TargetTrans != null && Vector3.Distance(TargetTrans.position, transform.position) <= attackRange)
                StartAttacking();
            else
                StopAttacking();
        }

        private IEnumerator AttackCoroutine()
        {
            while (Vector3.Distance(TargetTrans.position, transform.position) <= attackRange)
            {
                yield return new WaitForSeconds(MySnowmanSO.attackSpeed);
                var slashGO = Instantiate(slashPrefab, transform.position, Quaternion.identity, transform);
                slashGO.GetComponent<SnowmanSlash>().SetAttack(MySnowmanSO.attack, level == SnowmanLevel.Advanced);
            }
        }
        
        private void StartAttacking()
        {
            _attackCoroutine ??= StartCoroutine(AttackCoroutine());
        }
        
        private void StopAttacking()
        {
            if (_attackCoroutine == null) return;
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
}
