using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Entity entity;

    const float time = 0.2f;
    
    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public IEnumerator Traverse(List<Directions> directions)
    {
        foreach (Directions dir in directions)
        {
            if (entity.dir != dir)
            {
                yield return StartCoroutine(Turn(dir));
            }
            yield return StartCoroutine(Walk(dir));
        }
        yield return null;
    }
    
    public virtual IEnumerator Turn(Directions dir)
    {
        WaitForSeconds seconds = new WaitForSeconds(time / 30);

        int diff = dir - entity.dir;
        diff = (diff + 4) % 4;

        if(diff <= 2)
        {
            for (int i = 0; i < 30 * diff; i++) 
            {
                transform.localEulerAngles += new Vector3(0, 3, 0);
                yield return seconds;
            }
        }
        else
        {
            diff = 4 - diff;
            for (int i = 0; i < 30 * diff; i++)
            {
                transform.localEulerAngles += new Vector3(0, -3, 0);
                yield return seconds;
            }
        }

        entity.dir = dir;

        yield return null;
    }

    List<Point> dir2pos = new List<Point>() {
        new Point(0, 1), new Point(1, 0),
        new Point(0, -1), new Point(-1, 0) };

    IEnumerator Walk(Directions dir)
    {
        WaitForSeconds seconds = new WaitForSeconds(time / 30);

        Vector3 pos = new Vector3(dir2pos[(int)dir].x, 0, dir2pos[(int)dir].y);
        for (int i=0;i<30;i++)
        {
            transform.position += pos / 30;
            yield return seconds;
        }
        
        yield return null;
    }
}
