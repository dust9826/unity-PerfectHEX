/// <summary>
/// 좌표를 표시하는 구조체.
/// </summary>
[System.Serializable]
public struct Point3
{
    public int x;
    public int y;
    public int z;

    public Point3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Point3 operator +(Point3 a, Point3 b)
    {
        return new Point3(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static Point3 operator -(Point3 a, Point3 b)
    {
        return new Point3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    public static bool operator ==(Point3 a, Point3 b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }
    public static bool operator !=(Point3 a, Point3 b)
    {
        return !(a == b);
    }

    public static Point3 operator +(Point3 a, Point b)
    {
        return new Point3(a.x + b.x, a.y, a.z + b.y);
    }

    public override bool Equals(object obj)
    {
        if (obj is Point3)
        {
            Point3 p = (Point3)obj;
            return x == p.x && y == p.y && z == p.z;
        }
        return false;
    }
    public bool Equals(Point3 p)
    {
        return x == p.x && y == p.y && z == p.z;
    }
    public override int GetHashCode()
    {
        return x ^ y;
    }

    public override string ToString()
    {
        return string.Format("({0},{1},{2})", x, y, z);
    }
}
