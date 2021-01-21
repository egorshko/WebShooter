using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject WinPanel;
    public Slider ObjectsSlider;
    public Text WinText;

    private Counter _counter;
    private int _currentSliderNum;

    private void Awake()
    {
        _counter = FindObjectOfType<Counter>();
    }
    public void SetMaxSliderNum(int num)
    {
        ObjectsSlider.maxValue = num;
    }
    public void IncreaseSliderNum()
    {
        _currentSliderNum++;
        ObjectsSlider.value = _currentSliderNum;
    }
    public void DicreaseSliderNum()
    {
        _currentSliderNum--;
        ObjectsSlider.value = _currentSliderNum;
    }
    public void ZeroiseSliderNum()
    {
        _currentSliderNum = 0;
        ObjectsSlider.value = _currentSliderNum;
    }
    public void ActivateWinPanel()
    {
        WinPanel.SetActive(true);
        WinText.text = _counter.AmountOfCollectedObjects + "/" + _counter.MaxAmountOfObjects + " objects caught";
    }
}
