using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CoroutineUtility : GenericSingleton<CoroutineUtility>
{
    public Task RunCoroutineAndWait(MonoBehaviour target, string coroutineName)
    {
        var tcs = new TaskCompletionSource<bool>();
        target.StartCoroutine(WaitForCoroutine(target, coroutineName, tcs));
        return tcs.Task;
    }

    private IEnumerator WaitForCoroutine(MonoBehaviour target, string coroutineName, TaskCompletionSource<bool> tcs)
    {
        yield return target.StartCoroutine(coroutineName);
        tcs.SetResult(true);
    }
}
