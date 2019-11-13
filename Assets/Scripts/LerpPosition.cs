using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    public IEnumerator lerpPosition(Vector3 StartPos, Vector3 EndPos, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            print("moveing");
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            transform.position = Vector3.Lerp(StartPos, EndPos, timeProgressed);

            yield return new WaitForFixedUpdate();
        }

    }
}
