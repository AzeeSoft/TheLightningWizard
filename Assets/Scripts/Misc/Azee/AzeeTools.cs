using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzeeTools : MonoBehaviour
{

    public delegate void WaitedExecution();

    public static IEnumerator executeAfter(WaitedExecution waitedExecution, float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        waitedExecution();
    }
    
}
