using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Diagnostics;
using TMPro;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Hangman : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI wordDisplay;
    public TextMeshProUGUI debugText;
    public Image head;
    public RawImage body;
    public RawImage leftArm;
    public RawImage rightArm;
    public RawImage leftLeg;
    public RawImage rightLeg;

    public int Man = 0;

    private bool gameOver = false;
    private string gameOverMessage = "";

    void Start()
    {
        

        inputField.onEndEdit.AddListener(HandleInput);

        if (wordDisplay != null)
            wordDisplay.text = "Waiting for input...";

        if (debugText != null)
            debugText.text = "Type a letter and press Enter to start.";
    }

    void HandleInput(string text)
    {
        if (gameOver) return;
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Man = 0;
            if (wordDisplay != null)
                wordDisplay.text = "You typed: " + text;
            inputField.text = "";
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            inputField.ActivateInputField();
            RunJavaMinigame(text);
        }
    }

void RunJavaMinigame(string userInput)
{
    string jarPath = Path.Combine(Application.streamingAssetsPath, "minigame.jar");

    var process = new Process();
    process.StartInfo.FileName = "java";
    process.StartInfo.Arguments = $"-jar \"{jarPath}\" {userInput}";

    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardError = true;
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.CreateNoWindow = true;

    process.StartInfo.WorkingDirectory = Application.streamingAssetsPath;

    try
    {
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (!string.IsNullOrEmpty(error))
        {
            if (debugText != null)
                debugText.text = "Java Error: " + error;
        }
        else
        {
            Man = 6 - int.Parse(Regex.Match(output.Trim(), @"\d+").Value);
            UnityEngine.Debug.Log(Man);
            if (debugText != null)
                debugText.text = output.Trim();

            if (output.ToLower().Contains("win"))
            {
                gameOver = true;
                gameOverMessage = "You won! Press Enter to continue.";
            }
            else if (output.ToLower().Contains("lose") || Man >= 6)
            {
                gameOver = true;
                gameOverMessage = "You lost! Press Enter to continue.";
            }
        }
    }
    catch (Exception ex)
    {
        if (debugText != null)
            debugText.text = "Java start error: " + ex.Message;
    }
}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
        if (gameOver)
        {
            if (debugText != null)
                debugText.text = gameOverMessage;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("MainMenu");
            }
            return;
        }

        if (head == null || body == null || leftArm == null || rightArm == null || leftLeg == null || rightLeg == null)
        {
            return;
        }

        switch (Man)
        {
            case 0:
                head.enabled = false;
                body.enabled = false;
                leftArm.enabled = false;
                rightArm.enabled = false;
                leftLeg.enabled = false;
                rightLeg.enabled = false;
                break;
            case 1:
                head.enabled = true;
                body.enabled = false;
                leftArm.enabled = false;
                rightArm.enabled = false;
                leftLeg.enabled = false;
                rightLeg.enabled = false;
                break;
            case 2:
                head.enabled = true;
                body.enabled = true;
                leftArm.enabled = false;
                rightArm.enabled = false;
                leftLeg.enabled = false;
                rightLeg.enabled = false;
                break;
            case 3:
                head.enabled = true;
                body.enabled = true;
                leftArm.enabled = true;
                rightArm.enabled = false;
                leftLeg.enabled = false;
                rightLeg.enabled = false;
                break;
            case 4:
                head.enabled = true;
                body.enabled = true;
                leftArm.enabled = true;
                rightArm.enabled = true;
                leftLeg.enabled = false;
                rightLeg.enabled = false;
                break;
            case 5:
                head.enabled = true;
                body.enabled = true;
                leftArm.enabled = true;
                rightArm.enabled = true;
                leftLeg.enabled = true;
                rightLeg.enabled = false;
                break;
            case 6:
                head.enabled = true;
                body.enabled = true;
                leftArm.enabled = true;
                rightArm.enabled = true;
                leftLeg.enabled = true;
                rightLeg.enabled = true;
                break;
        }
    }
}
