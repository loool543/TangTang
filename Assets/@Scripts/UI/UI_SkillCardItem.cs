using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCardItem : UI_Base
{
    //� ��ų?
    //�� ����?
    //�����ͽ�Ʈ?

    int _templateId;
    Data.SkillData _skillData;

    public void SetInfo(int templateId)
    {
        _templateId = templateId;

        Managers.Data.SkillDic.TryGetValue(templateId, out _skillData);
    }

    public void OnClickItem()
    {
        //��ų ���� ���׷��̵�
        Debug.Log("OnClickItem");
        Managers.UI.ClosePopup();
    }
}
