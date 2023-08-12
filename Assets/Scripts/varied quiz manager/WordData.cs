 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI charText;

    [HideInInspector]
    public char charValue;

    private Button buttonObj;

    private void Awake()
    {
        buttonObj = GetComponent<Button>();
        if (buttonObj)
        {
            buttonObj.onClick.AddListener(()=>CharSelected());
        }
    }

    public void SetChar(char value)
    {
        charText.text = value + "";
        charValue = value;
    }

    private void CharSelected()
    {
        QuizLogoManager.instance.SelectedOption(this);
    }
}
