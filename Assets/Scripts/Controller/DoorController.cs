using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Transform door;
    [SerializeField]
    float openAngle;

    public void Open()
    {
        StartCoroutine(OpenAni());
    }

    IEnumerator OpenAni()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.2f / 30);
        for (int i = 0; i < 30; i++) 
        {
            door.eulerAngles += new Vector3(0, openAngle, 0) / 30;
            yield return seconds;
        }
    }
}
