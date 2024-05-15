using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class BackstorySceneManager : MonoBehaviour
{
    public TMP_Text loreText;
    public GameObject nextButton;
    public GameObject exitButton;

    private string[] loreTextEntries = {
        "Je bent een docent in een verlaten school, nadat deze door een aantal ongemotiveerde studenten overgenomen is...\n \nen het is jouw taak om de studenten te bereiken, en onderweg te motiveren!",
        "Het drietal studenten representeren ieder een van de motivatie basisbehoeften die zij missen, namelijk: \n \n \t Autonomie, Competentie en Verbondenheid.",
        "Onderweg zul je beslissingskamers en uitdagingskamers tegenkomen. \n \nBinnen beslissingskamers zul je geconfronteerd worden met een scenario omtrent motivatie bij studenten en een aantal keuzes van mogelijke reacties hierop. \n \nKies hierbij de meest passende keuze om de studenten te motiveren. De keuzes hebben allemaal een bepaalde impact op ieder van de basisbehoeften, en dus ook de studenten!",
        "Na een aantal beslissingskamers zul je in een uitdagingskamer terechtkomen, waarbij je de uitgang moet bereiken terwijl de ongemotiveerde studenten objecten naar je toe gooien. \n \nHoe ongemotiveerder de studenten, hoe moeilijker dit zal zijn. Probeer de vragen vooraf dus zo goed mogelijk te beantwoorden!",
        "Bij het bereiken van de studenten hangt de toekomst van de school af van jouw pogingen tot motivatie onderweg...\n \n \t Veel succes! \n \n(Het is aan te raden om eerst de tutorial te spelen om comfortabel te raken met de besturing en knoppen.)",
    };

    private int currentIndex = 0;
    public float typingSpeed = 0.02f; // Typing speed

    void Start()
    {
        StartCoroutine(TypeText(loreTextEntries[currentIndex]));
    }

    // Update lore text on click
    public void NextTextEntry()
    {
        currentIndex++;

        if (currentIndex < loreTextEntries.Length)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText(loreTextEntries[currentIndex]));
        } else
        {
            nextButton.SetActive(false);
            nextButton.transform.GetChild(0).gameObject.SetActive(false);

            exitButton.SetActive(true);
            exitButton.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // Write text with typewriter effect
    IEnumerator TypeText(string text)
    {
        loreText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            loreText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
