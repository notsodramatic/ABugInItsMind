using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MemoTextCanvas : MonoBehaviour
{
    public MemoTextSO memoTextSO;
    public float runningTextDelay;

    public Text memoTextDisplay;
    public Button nextButton;

    public string nextSceneName;

    public GameObject runningTextGroup, allTextGroup;

    string[] memoTextLines;

    int currentLineIndex;

    char[] lineCharacters;
    int currentChar;
    bool doneRunning;

    private void Awake()
    {
        memoTextLines = memoTextSO.memoTextLines;
    }
    private void Start()
    {
        nextButton.gameObject.SetActive(false);

        memoTextDisplay.text = "";

        Invoke("InitializeMemoText", 0.5f);
    }

    IEnumerator RunText()
    {
        while (!doneRunning)
        {
            yield return new WaitForSeconds(runningTextDelay);

            currentChar++;

            if (currentChar < lineCharacters.Length)
            {
                memoTextDisplay.text += lineCharacters[currentChar];
            }
            else
            {
                currentChar = 0;
                doneRunning = true;
                
                nextButton.gameObject.SetActive(doneRunning);
            }
        }
        
    }

    //display next passage of memo 
    public void OnNextButtonClick()
    {
        currentLineIndex++;

        if (currentLineIndex < memoTextLines.Length)
        {
            InitializeMemoText();
        }
        else
        {
            SceneLoader.Instance.TransitionToScene(nextSceneName);
        } 
    }

    private void InitializeMemoText()
    {
        lineCharacters = memoTextLines[currentLineIndex].ToCharArray();

        memoTextDisplay.text = "" + lineCharacters[currentChar];

        doneRunning = false;

        nextButton.gameObject.SetActive(doneRunning);

        StartCoroutine(RunText());
    }

    public void ShowAllTexts()
    {
        runningTextGroup.SetActive(false);
        allTextGroup.SetActive(true);
    }
}
