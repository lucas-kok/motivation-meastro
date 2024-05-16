using UnityEngine;

public class ChallengeRoomManager : MonoBehaviour
{
    public GameObject layoutContainer;

    private void Start()
    {
        if (layoutContainer == null || layoutContainer.transform.childCount <= 0)
        {
            return;
        }


        var layoutsCount = layoutContainer.transform.childCount;
        for (int i = 0; i < layoutsCount; i++)
        {
            layoutContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        var randomIndex = Random.Range(0, layoutsCount);
        layoutContainer.transform.GetChild(randomIndex).gameObject.SetActive(true);
    }
}
