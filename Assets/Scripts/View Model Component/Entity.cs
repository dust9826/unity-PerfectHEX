using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Point pos { get; protected set; }
    public Directions dir;

    public bool isPlayer = false;

    public void Awake()
    {
        Vector3 position = transform.position - new Vector3(0.5f, 0, 0.5f);
        pos = new Point((int)position.x, (int)position.z);
        Match();
    }

    public void Place(Point target)
    {
        pos = target;
        Match();
    }

    // 해당 게임 오브젝트의 Position 과 EulerAngles 값을 변경합니다.
    public void Match()
    {
        transform.localPosition = new Vector3(pos.x, 0, pos.y) + new Vector3(0.5f, 0, 0.5f);

        // Vector3(x, y, z)로 Rotation을 구하는 겁니다.
        transform.localEulerAngles = dir.ToEuler();
    }
}
