using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GridCell
{
    public bool IsEmpty { get; set; }
}

public class GridManager : Singleton<GridManager> 
{
    public int _width;
    public int _height;
    public int _size;

    private Vector3 _min;
    private Vector3 _max;
    private GridCell[] _grid;

    void Start()
    {
        _min = transform.position;
        _min.x -= (_width / 2.0f) * _size;
        _min.z -= (_height / 2.0f) * _size;
        _max = transform.position;
        _max.x += (_width / 2.0f) * _size + _size;
        _max.z += (_height / 2.0f) * _size + _size;
        _grid = new GridCell[_width * _height];
        for (int i = 0; i < _grid.Length; i++)
        {
            _grid[i] = new GridCell();
            _grid[i].IsEmpty = true;
        }
    }

    public bool IsEmpty(int x, int y)
    {
        if (x >= 0 && x < _width && y >= 0 && y < _height)
        {
            return _grid[y * _width + x].IsEmpty;
        }
        return false;
    }

    public void SetEmpty(int x, int y, bool empty)
    {
        if (x >= 0 && x < _width && y >= 0 && y < _height)
        {
            _grid[y * _width + x].IsEmpty = empty;
        }
    }

    public Vector2Int GetCoordFromPosition(Vector3 position)
    {
        Vector2Int coord = Vector2Int.zero;
        coord.x = position.x < _min.x ? 0 : (position.x > _max.x ? _width : (int)((position.x - _min.x) / _size));
        coord.y = position.z < _min.z ? 0 : (position.z > _max.z ? _width : (int)((position.z - _min.z) / _size));
        return coord;
    }

    public Vector3 GetCellCenterFromPosition(Vector3 position)
    {
        Vector2Int coord = GetCoordFromPosition(position);
        Vector3 cellCenterPos = position;
        cellCenterPos.x = _min.x + coord.x * _size + _size * 0.5f;
        cellCenterPos.z = _min.z + coord.y * _size + _size * 0.5f;
        return cellCenterPos;
    }

    public bool CanPlaceObject(GameObject gameObject)
    {
        var guo = new GraphUpdateObject(gameObject.GetComponentInChildren<Collider>().bounds);
        var start = AstarPath.active.GetNearest(GameObject.FindWithTag("SpawnStart").transform.position).node;
        var end = AstarPath.active.GetNearest(GameObject.FindWithTag("SpawnEnd").transform.position).node;

        return GraphUpdateUtilities.UpdateGraphsNoBlock(guo, start, end, false);
    }

    void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        position.x -= (_width / 2.0f) * _size;
        position.y += 0.1f;
        position.z -= (_height / 2.0f) * _size;

        Gizmos.color = Color.red;
        for (int i = 0; i < _width + 1; i++)
        {
            Vector3 start = new Vector3(i * _size + position.x, position.y, position.z);
            Vector3 end = new Vector3(i * _size + position.x, position.y, _height * _size + position.z);

            Gizmos.DrawLine(start, end);
        }
        for (int i = 0; i < _height + 1; i++)
        {
            Vector3 start = new Vector3(position.x, position.y, i * _size + position.z);
            Vector3 end = new Vector3(_width * _size + position.x, position.y, i * _size + position.z);

            Gizmos.DrawLine(start, end);
        }
    }
}