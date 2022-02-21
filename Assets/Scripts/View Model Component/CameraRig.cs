using UnityEngine;
using System.Collections;

public class CameraRig : MonoBehaviour
{
    public float speed = 3f;
    public Transform follow;
    Transform _transform;

    Directions dir;

    void Awake()
    {
        _transform = transform;
        dir = Directions.North;
    }

    void Update()
    {
        if (follow)
            _transform.position = Vector3.Lerp(_transform.position, follow.position, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dir = (Directions)(((int)dir + 1) % 4);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            dir = (Directions)(((int)dir + 3) % 4);
        }

        _transform.eulerAngles = Vector3.Lerp(_transform.eulerAngles, dir.ToEuler(), speed * Time.deltaTime);
    }

    public Point GetRotatePoint(Point pos)
    {
        int diff = dir - Directions.North;
        diff = (diff + 4) % 4;
        Point ret = new Point();
        switch (diff)
        {
            case 0:
                ret = new Point(pos.x, pos.y);
                break;
            case 1:
                ret = new Point(pos.y, -pos.x);
                break;
            case 2:
                ret = new Point(-pos.x, -pos.y);
                break;
            case 3:
                ret = new Point(-pos.y, pos.x);
                break;
        }
        return ret;
    }
}
