using UnityEngine;

public class Counter : MonoBehaviour
{
    public int MaxAmountOfObjects;
    public int AmountOfCollectedObjects;

    private UIController _uiController;

    private void Start()
    {
        _uiController = FindObjectOfType<UIController>();
    }
    public void SetMaxAmountOfObjects(int ObjectsAmount)
    {
        MaxAmountOfObjects = ObjectsAmount;
        _uiController.SetMaxSliderNum(ObjectsAmount);
    }
    public void IncreaseAmountOfCollectedObjects()
    {
        AmountOfCollectedObjects++;
        _uiController.IncreaseSliderNum();
    }
    public void DicreaseAmountOfCollectedObjects()
    {
        AmountOfCollectedObjects--;
        _uiController.DicreaseSliderNum();
    }
    public void ZeroiseAmountOfCollectedObjects()
    {
        AmountOfCollectedObjects = 0;
        _uiController.ZeroiseSliderNum();
    }
}
