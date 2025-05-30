using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EgoSword : ��Ÿ
// FireProjectile : ����ü
// PoisonField_AOE : �ٴ�

public class SkillController : BaseController
{
    public Define.SkillType SkillType { get; set; }
    public Data.SkillData SkillData { get; protected set; }


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
