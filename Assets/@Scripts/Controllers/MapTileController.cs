using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Camera camera = collision.gameObject.GetComponent<Camera>(); //카메라만 체크할 수 있도록 일단 해준다
        if (camera == null)
            return;

        Vector3 dir = camera.transform.position - transform.position;

        float dirX = dir.x < 0 ? -1 : 1;
        float dirY = dir.y < 0 ? -1 : 1;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            transform.Translate(Vector3.right * dirX * 200);
        else
            transform.Translate(Vector3.up * dirY * 200);
    }

}
