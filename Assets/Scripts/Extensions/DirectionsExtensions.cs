using UnityEngine;

public static class DirectionsExtensions
{
    // 타겟과의 방향에 따라 Directions의 Enum 값이 리턴된다.
    public static Directions GetDirection(this Point p1, Point p2)
    {
        if (p1.y < p2.y)
            return Directions.North;
        if (p1.x < p2.x)
            return Directions.East;
        if (p1.y > p2.y)
            return Directions.South;
        return Directions.West;
    }

    // 방향을 오일러 각도로 반환한다.
    public static Vector3 ToEuler(this Directions d)
    {
        return new Vector3(0, (int)d * 90, 0);
    }
}