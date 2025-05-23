using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

class cell
{
    public HashSet<GameObject> Objects { get; } = new HashSet<GameObject>();
}

public class GridController : BaseController
{
    UnityEngine.Grid _grid;
    Dictionary<Vector3Int, cell> _cells = new Dictionary<Vector3Int, cell>();

    public override bool Init()
    {
        base.Init();

        _grid = gameObject.GetOrAddComponent<UnityEngine.Grid>();
        return true;
    }
    public void Add(GameObject go)
    {
        Vector3Int cellPos = _grid.WorldToCell(go.transform.position);
        
        cell cell = GetCell(cellPos);
        if (cell == null)   //이버전에서는 반드시 리턴해주기 때문에 이게 사실 필요가 없음
            return;

        cell.Objects.Add(go);
    }

    public void Remove(GameObject go)
    {
        Vector3Int cellPos = _grid.WorldToCell(go.transform.position);

        cell cell = GetCell(cellPos);
        if (cell == null)   //이버전에서는 반드시 리턴해주기 때문에 이게 사실 필요가 없음
            return;
        cell.Objects.Remove(go);
    }

    cell GetCell(Vector3Int cellPos)
    {
        cell cell = null;

        if (_cells.TryGetValue(cellPos, out cell) == false)
        {
            cell = new cell();
            _cells.Add(cellPos, cell); 
        }
        return cell;
    }

    public List<GameObject> GatherObjects(Vector3 pos, float range)
    {
        List<GameObject> objects = new List<GameObject>();

        Vector3Int left = _grid.WorldToCell(pos + new Vector3(-range, 0));
        Vector3Int right = _grid.WorldToCell(pos + new Vector3(+range, 0));
        Vector3Int bottom = _grid.WorldToCell(pos + new Vector3(0, -range));
        Vector3Int top = _grid.WorldToCell(pos + new Vector3(0, +range));

        int minX = left.x;
        int maxX = right.x;
        int minY = bottom.y;
        int maxY = top.y;

        for(int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if(_cells.ContainsKey(new Vector3Int(x, y, 0)) == false)
                    continue;

                objects.AddRange(_cells[new Vector3Int(x, y, 0)].Objects);

            }
        }

        return objects;
    }

}
