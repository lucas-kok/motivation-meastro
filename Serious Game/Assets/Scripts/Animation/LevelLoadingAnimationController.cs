using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelLoadingAnimationController : MonoBehaviour
{
    public float vignetteMoveSeed = 0.1f;
    public float vignetteIntensitySpeed = 0.5f;
    public Volume volume;
    
    private Vignette _vignette;
    private AppLogger _logger;

    private void Start()
    {
        
    }

    public void Initialize()
    {
        if (volume != null && volume.profile.TryGet(out _vignette))
        {
            _vignette.intensity.value = 1f;
            _vignette.center.value = new Vector2(0.5f, 1.5f);
        }
        else
        {
            _logger.LogError("Vignette component not found in the PostProcessVolume's profile.", this);
        }
    }

    public IEnumerator PlayLoadLevelAnimation()
    {
        StopCoroutine("PlayExitLevelAnimation");
        if (_vignette == null) yield break;

        while (_vignette.center.value.y > 0.5f || _vignette.intensity.value > 0.25)
        {
            if (_vignette.center.value.y > 0.5f)
            {
                _vignette.center.value -= new Vector2(0, vignetteMoveSeed * Time.deltaTime);
            }

            if (_vignette.center.value.y <= 0.5f && _vignette.intensity.value > 0.25)
            {
                _vignette.intensity.value -= vignetteIntensitySpeed * Time.deltaTime;
            }

            yield return null;
        }
    }

    public IEnumerator PlayExitLevelAnimation()
    {
        StopCoroutine("PlayLoadLevelAnimation");
        if (_vignette == null) yield break;

        while (_vignette.intensity.value < 1f || _vignette.center.value.y < 1.5f)
        {
            if (_vignette.intensity.value < 1f)
            {
                _vignette.intensity.value += vignetteIntensitySpeed * Time.deltaTime;
            }

            if (_vignette.intensity.value >= 1f && _vignette.center.value.y < 1.5f)
            {
                _vignette.center.value += new Vector2(0, vignetteMoveSeed * Time.deltaTime);
            }

            yield return null;
        }
    }
}
