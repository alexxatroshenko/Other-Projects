using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PaintChar : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    Vector2 mousePos;
    Vector2 worldMousePos;
    Vector2 gridPos, startGridPos;

    //states
    private bool isClicked = false;

    //serialized
    [SerializeField] private Char[] chars;
    [SerializeField] private List<Char> inputedChars; //serializing for debugg
    [SerializeField] private List<string> inputedString; //serializing for debugg
    [SerializeField] private AudioClip CharClickClip;

    //cached
    private WordsArray wordsArr;
    private AudioSource audioSource;
    private Char[,] matrixChars;

    private void Start()
    {
        wordsArr = GetComponent<WordsArray>();
        matrixChars = SetCharMatrix(chars);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ColibrateMouse();
    }

    private void ColibrateMouse()
    {
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        gridPos = SnapToGrid(worldMousePos);
        //Touch touch = Input.GetTouch(0);
        //touchCoords.text = touch.position.x + " " + touch.position.y;
        //mousePos = new Vector2(touch.position.x, touch.position.y);
        //worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //gridPos = SnapToGrid(worldMousePos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isClicked && startGridPos != gridPos)
        {
            if (matrixChars[(int)gridPos.x, (int)gridPos.y].GetComponent<SpriteRenderer>().color == new Color32(229, 190, 68, 255))
            {
                matrixChars[(int)startGridPos.x, (int)startGridPos.y].GetComponent<SpriteRenderer>().color = new Color32(137, 130, 108, 255);
                //Debug.Log(inputedChars.IndexOf(matrixChars[(int)gridPos.x, (int)gridPos.y]));
                //inputedChars.RemoveRange(inputedChars.IndexOf(matrixChars[(int)gridPos.x, (int)gridPos.y]), inputedChars.IndexOf(matrixChars[(int)startGridPos.x, (int)startGridPos.y]) + 1);
                //перевести matrixchars[] в стринг и тоже удалять
                inputedChars.Remove(inputedChars[inputedString.Count - 1]);
                inputedString.Remove(inputedString[inputedString.Count - 1]);
                if(wordsArr.GetRightWords().Contains(matrixChars[(int)gridPos.x, (int)gridPos.y]))
                {
                    isClicked = false;
                    ClearAll();
                    return;
                }


            }
            matrixChars[(int)gridPos.x, (int)gridPos.y].GetComponent<SpriteRenderer>().color =
            new Color32(229, 190, 68, 255);
            InputChars(gridPos);
            startGridPos = gridPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(wordsArr.GetRightWords().Contains(matrixChars[(int)gridPos.x, (int)gridPos.y])) return;
        matrixChars[(int)gridPos.x, (int)gridPos.y].GetComponent<SpriteRenderer>().color = new Color32(229, 190, 68, 255);
        InputChars(gridPos);
        startGridPos = gridPos;
        isClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ClearWrong();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClearAll();
    }

    private void InputChars(Vector2 gridPos)
    {
        audioSource.PlayOneShot(CharClickClip, 0.5f);
        if (!inputedChars.Contains(matrixChars[(int)gridPos.x, (int)gridPos.y]))
        {
            inputedString.Add(matrixChars[(int)gridPos.x, (int)gridPos.y]
                .GetComponentInChildren<TextMeshProUGUI>().text);
            inputedChars.Add(matrixChars[(int)gridPos.x, (int)gridPos.y]);
            isClicked = true;
        }
    }

    public List<Char> GetInputedChars()
    {
        return inputedChars;
    }

    public List<string> GetInputedStrings()
    {
        return inputedString;
    }

    public int GetCharsAmount()
    {
        return chars.Length;
    }

    private IEnumerator ClearInputedChars(List<Char> charList)
    {
        yield return new WaitForSeconds(0.001f);
        charList.Clear();
    }

    private void CleanInputedChars(List<Char> charList)
    {
        foreach (Char letter in charList)
        {
            letter.GetComponent<SpriteRenderer>().color = new Color32(137, 130, 108, 255);
        }
    }

    private IEnumerator ClearInputedStrings(List<string> stringList)
    {
        yield return new WaitForSeconds(0.001f);
        stringList.Clear();
    }

    private void ClearAll()
    {
        StartCoroutine(ClearInputedChars(inputedChars));
        StartCoroutine(ClearInputedStrings(inputedString));
        CleanInputedChars(inputedChars);
        
        isClicked = false;
    }

    private void ClearWrong()
    {
        StartCoroutine(ClearInputedChars(inputedChars));
        StartCoroutine(ClearInputedStrings(inputedString));
        if (!wordsArr.IsTheWordRight())
        {
            CleanInputedChars(inputedChars);
        }
        isClicked = false;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        Vector2 gridPos = new Vector2(newX, newY);
        return gridPos;
    }

    private Char[,] SetCharMatrix(Char[] chars)
    {
        int loopLength;
        int charsIndex = 0;
        switch (chars.Length)
        {
            case 9:
                loopLength = 3;
                break;
            case 16:
                loopLength = 4;
                break;
            case 25:
                loopLength = 5;
                break;
            default:
                loopLength = 6;
                break;
        }
        Char[,] matrix = new Char[loopLength, loopLength];

        for (int i = 0; i < loopLength; i++)
            for (int j = 0; j < loopLength; j++)
            {
                matrix[i, j] = chars[charsIndex];
                charsIndex++;
            }

        return matrix;
    }

}