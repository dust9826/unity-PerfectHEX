using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuPanelController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text title;
    [SerializeField] Text content;

    List<string> menuName;

    public int selection { get; private set; }

    private void Start()
    {
        canvas.SetActive(false);
        selection = 0;
    }

    bool SetSelection(int value)
    {
        if (value < 0 || value >= menuName.Count)
            return false;

        selection = value;
        content.text = menuName[selection];
        return true;
    }

    public void Show(string _title, List<string> option)
    {
        canvas.SetActive(true);
        menuName = option;
        title.text = _title;
        SetSelection(0);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void Next()
    {
        int index = selection + 1;
        SetSelection(index);
    }

    public void Previous()
    {
        int index = selection - 1;
        SetSelection(index);
    }
}