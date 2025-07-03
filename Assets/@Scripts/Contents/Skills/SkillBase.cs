using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EgoSword : ��Ÿ
// FireProjectile : ����ü
// PoisonField_AOE : �ٴ�

public class SkillBase : BaseController
{
    public CreatureController Owner { get; set; }
    public Define.SkillType SkillType { get; set; }
    public Data.SkillData SkillData { get; protected set; }

    public int SkillLevel { get; set; } = 0;

    public bool IsLearnedSkill
    {
        get { return SkillLevel > 0; }
    }

    public int Damage { get; set; } = 100;

    public SkillBase(Define.SkillType skillType)
    {
        SkillType = skillType;
    }

    public virtual void ActivateSkill(){}

    protected virtual void GenerateProjectile(int templateId, CreatureController owner, Vector3 startPos, Vector3 dir, Vector3 targetPos)
    {
        ProjectileController pc = Managers.Object.Spawn<ProjectileController>(startPos, templateId);
        pc.SetInfo(templateId, owner, dir);

    }

    #region Destroy  // �����ð��� ������ ��Ŭ�� ������ �ı��ǵ��� ���ֱ�!
    Coroutine _coDestroy;

    public void StartDestroy (float delaySeconds)
    {
        StopDestroy(); // ������ �������� �ڷ�ƾ�� �ִٸ� ����
        _coDestroy = StartCoroutine(CoDestroy(delaySeconds));
    }

    public void StopDestroy()
    {
        if (_coDestroy != null)
        {
            StopCoroutine(_coDestroy);
            _coDestroy = null;
        }
    }

    IEnumerator CoDestroy(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        if (this.IsValid())
        {
            Managers.Object.Despawn(this);
        }
    }
    #endregion


}
