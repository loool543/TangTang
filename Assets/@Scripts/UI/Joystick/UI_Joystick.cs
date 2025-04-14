using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{   
    [SerializeField]
    Image _background;

    [SerializeField]
    Image _handler;

    float _joystickRadius;
    Vector2 _touchPosition;

    Vector2 _moveDir;

    PlayerController _player;

    void Start()
    {
        _joystickRadius = _background.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;

    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _handler.transform.position = _touchPosition;
        _moveDir = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _background.transform.position = eventData.position;
        _handler.transform.position = eventData.position;
        _touchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _handler.transform.position = _touchPosition;
        _moveDir = Vector2.zero;

        //Temp2
        Managers.Game.MoveDir = _moveDir;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //drag�� �������� ���� �ΰ� ���� ���ϰ� �� ���� normalized �ؼ� ���ⱸ�ϱ�
        //�ش� �������� �̵� �̷��� �ϸ� ���� ������??!
        Vector2 touchDir = (eventData.position - _touchPosition);

        float moveDist = Mathf.Min(touchDir.magnitude, _joystickRadius);
        _moveDir = touchDir.normalized;

         _handler.transform.position = _touchPosition + _moveDir * moveDist;


        //Temp2
        Managers.Game.MoveDir = _moveDir;

    }
}

