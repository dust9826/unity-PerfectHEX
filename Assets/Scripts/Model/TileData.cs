using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타일의 크기, 충돌 정보르 저장하는 값
/// </summary>
public class TileData : MonoBehaviour
{
    public Vector2 size;
    public List<Point> nonColiders;
}
