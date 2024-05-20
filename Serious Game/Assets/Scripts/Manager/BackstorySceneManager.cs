using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class BackstorySceneManager : MonoBehaviour
{
    public TMP_Text loreText;
    public GameObject playTutorialButton;
    public GameObject nextButton;
    public GameObject exitButton;
    public GameObject skipButton;
    public GameManager gameManager;
    public float timeBetweenTypingChars = 3f;

    public InputManager inputManager;

    public string[] loreTextEntries = {
        "Je bent een docent in een verlaten school, nadat deze door een aantal ongemotiveerde studenten overgenomen is...\n \nen het is jouw taak om de studenten te bereiken, en onderweg te motiveren!",
        "Het drietal studenten representeren ieder een van de motivatie basisbehoeften die zij missen, namelijk: \n \n \t Autonomie, Competentie en Verbondenheid.",
        "Onderweg zul je beslissingskamers en uitdagingskamers tegenkomen. \n \nBinnen beslissingskamers zul je geconfronteerd worden met een scenario omtrent motivatie bij studenten en een aantal keuzes van mogelijke reacties hierop. \n \nKies hierbij de meest passende keuze om de studenten te motiveren. De keuzes hebben allemaal een bepaalde impact op ieder van de basisbehoeften, en dus ook de studenten!",
        "Na een aantal beslissingskamers zul je in een uitdagingskamer terechtkomen, waarbij je de uitgang moet bereiken terwijl de ongemotiveerde studenten objecten naar je toe gooien. \n \nHoe ongemotiveerder de studenten, hoe moeilijker dit zal zijn. Probeer de vragen vooraf dus zo goed mogelijk te beantwoorden!",
        "Bij het bereiken van de studenten hangt de toekomst van de school af van jouw pogingen tot motivatie onderweg...\n \n \t Veel succes!",
    };
    public int loreEntryIndexWithTutorial = 4;
    private int _currentIndex = -1;

    private CoroutineUtility _coroutineUtility;

    void Start()
    {
        _coroutineUtility = CoroutineUtility.Instance;

        playTutorialButton.SetActive(false);
        nextButton.SetActive(false);
        exitButton.SetActive(false);
        skipButton.SetActive(true);

        PlayNextTextEntry();
    }

    private void OnEnable()
    {
        inputManager.OnKeyPress += HandleKeyPress;
    }

    private void OnDisable()
    {
        inputManager.OnKeyPress -= HandleKeyPress;
    }

    private void HandleKeyPress(KeyCode key)
    {        
        if (key.ToString() == KeyCode.Return.ToString())
        {
            if (!nextButton.activeSelf && exitButton.activeSelf) 
            {
                gameManager.OnExitBackstory();    
            } else
            {
                PlayNextTextEntry();
            }
        }
    }

    public async void PlayNextTextEntry()
    {
        _currentIndex++;

        if (_currentIndex < loreTextEntries.Length)
        {            
            nextButton.SetActive(false);

            StopAllCoroutines();
            await _coroutineUtility.RunCoroutineAndWait(this, () => TypeText(loreTextEntries[_currentIndex]));

            nextButton.SetActive(true);
        }

        playTutorialButton.SetActive(_currentIndex == loreEntryIndexWithTutorial);

        if (_currentIndex == loreTextEntries.Length)
        {
            nextButton.SetActive(false);
            exitButton.SetActive(true);
            skipButton.SetActive(false);
        }
    }

    private IEnumerator TypeText(string text)
    {
        loreText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            loreText.text += letter;
            yield return new WaitForSeconds(timeBetweenTypingChars * Time.deltaTime);
        }
    }

    public void SkipLore()
    {
        if (gameManager == null)
        {
            return;
        }

        gameManager.OnExitBackstory();
    }
}
