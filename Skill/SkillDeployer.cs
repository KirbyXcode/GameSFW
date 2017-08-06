using UnityEngine;
using System.Collections;
using ARPGSimpleDemo.Character;
using ARPGSimpleDemo.Common;

namespace ARPGSimpleDemo.Skill
{
    //技能释放类
    public class SkillDeployer : MonoBehaviour
    {
        private SkillData m_skillData;
        ///<summary>敌人选区，选择目标的算法</summary>
        public IAttackSelector attackTargetSelector;
        //发出者
        private CharacterStatus status;

        /// <summary> 要释放的技能 </summary>
        public SkillData skillData
        {
            set
            {
                m_skillData = value;
                attackTargetSelector = SelectorFactory.CreateSelector(value.damageMode);
                status = value.Owner.GetComponent<CharacterStatus>();
            }
            get
            {
                return m_skillData;
            }
        }



        /// <summary>技能释放</summary>
        public virtual void DeploySkill()
        {
            if (m_skillData == null) return;
            //对自身的影响
            SelfImpact(m_skillData.Owner);
            //执行伤害的计算
            StartCoroutine(ExecuteDamage());
        }

        //执行伤害的计算
        protected virtual IEnumerator ExecuteDamage()
        {
            //按持续时间及，两次伤害间隔，
            float attackTimer = 0;//已持续攻击的时间

            do
            {
                //通过选择器选好攻击目标
                ResetTargets();
                if (skillData.attackTargets != null && skillData.attackTargets.Length > 0)
                {
                    foreach (var item in skillData.attackTargets)
                    {
                        //对敌人的影响
                        TargetImpact(item);
                    }
                }
                yield return new WaitForSeconds(skillData.damageInterval);
                attackTimer += skillData.damageInterval;
                //做伤害数值的计算
            } while (skillData.durationTime > attackTimer);

        }

        private void ResetTargets()
        {
            if (m_skillData == null) return;

            m_skillData.attackTargets = attackTargetSelector.SelectTarget(m_skillData, transform);

        }

        ///对敌人的影响
        public virtual void TargetImpact(GameObject goTarget)
        {
            //受伤
            var damageVal = status.damage * m_skillData.damage;
            var targetStatus = goTarget.GetComponent<CharacterStatus>();

            targetStatus.OnDamage((int)damageVal, skillData.Owner);
            //出受伤特效
            if (skillData.hitFxPrefab != null)
            {
                //找到受击特效的挂点
                Transform hitFxPos = goTarget.GetComponent<CharacterStatus>().HitFxPos;
                if (hitFxPos != null)
                {
                    var go = GameObjectPool.instance.CreateObject(
                        skillData.hitFxName,
                        skillData.hitFxPrefab,
                        hitFxPos.position,
                        hitFxPos.rotation);
                    GameObjectPool.instance.MyDestory(go, 0.2f);
                }
            }
        }

        ///对自身的影响
        public virtual void SelfImpact(GameObject goSelf)
        {
            //释放者: 消耗SP
            var chStaus = goSelf.GetComponent<CharacterStatus>();
            chStaus.SP -= m_skillData.costSP;
			//add+2 魔法条更新
			if(goSelf.tag == "Player")
				UIBattleHead.instance.SetMp(status.SP,status.MaxSP);

        }
    }
}
