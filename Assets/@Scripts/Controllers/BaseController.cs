using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour
{
    public ObjectType ObjectType { get; protected set; }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    bool _init = false;
    public virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return _init;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
