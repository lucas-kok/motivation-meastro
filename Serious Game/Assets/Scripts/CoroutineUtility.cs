using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CoroutineUtility : GenericSingleton<CoroutineUtility>
{
    public Task RunCoroutineAndWait(MonoBehaviour target, Func<IEnumerator> coroutineFunc)
    {
        var tcs = new TaskCompletionSource<bool>();
        target.StartCoroutine(WaitForCoroutine(coroutineFunc, tcs));
        return tcs.Task;
    }

    private IEnumerator WaitForCoroutine(Func<IEnumerator> coroutineFunc, TaskCompletionSource<bool> tcs)
    {
        yield return coroutineFunc();
        tcs.SetResult(true);
    }
}
