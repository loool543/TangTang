using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EgoSword : 평타
// FireProjectile : 투사체
// PoisonField_AOE : 바닥

public class SkillController : BaseController
{
    public Define.SkillType SkillType { get; set; }
    public Data.SkillData SkillData { get; protected set; }


    #region Destroy  // 일정시간이 지나면 스클이 저절로 파괴되도록 해주기!
    Coroutine _coDestroy;

    public void StartDestroy (float delaySeconds)
    {
        StopDestroy(); // 기존에 실행중인 코루틴이 있다면 중지
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
