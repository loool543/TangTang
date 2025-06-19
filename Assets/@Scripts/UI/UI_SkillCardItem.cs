using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCardItem : UI_Base
{
    //어떤 스킬?
    //몇 레벨?
    //데이터시트?

    int _templateId;
    Data.SkillData _skillData;

    public void SetInfo(int templateId)
    {
        _templateId = templateId;

        Managers.Data.SkillDic.TryGetValue(templateId, out _skillData);
    }

    public void OnClickItem()
    {
        //스킬 레벨 업그레이드
        Debug.Log("OnClickItem");
        Managers.UI.ClosePopup();
    }
}
