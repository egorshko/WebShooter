using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Transform[] SpawnPoints;
	public GameObject[] FallingObjects;
	public GameObject Coin;
	public int MaxCoins;
	public int AmountOfThrowingObject;
	public int TimeBeforeThrownig;
	public bool IsBonusRound;


	private MainGameController _mainGameController;
	private Counter _counter;
	private LvlPreset _currentLvlPreset;
	private GameObject _throwedObject;
	private ThrowingObject _throwedObjectScript;
	private int NumberOfObject;
	private float TimeBeforeThrowing;

	private void Awake()
	{
		_mainGameController = FindObjectOfType<MainGameController>();
		_counter = FindObjectOfType<Counter>();
	}
	public void StartSpawner(LvlPreset lvlPreset)
	{
		if (!IsBonusRound)
		{
			_currentLvlPreset = lvlPreset;
			_counter.SetMaxAmountOfObjects(_currentLvlPreset.TrowingObjects.Length);
			for (int i = 0; i < _currentLvlPreset.TrowingObjects.Length; i++)
			{
				TimeBeforeThrowing += _currentLvlPreset.TrowingObjects[i].TimeBeforeThrowing;
				Invoke("SpawnObject", TimeBeforeThrowing);
				if (i == _currentLvlPreset.TrowingObjects.Length - 1)
				{
					TimeBeforeThrowing += 5;
					Invoke("EndLvl", TimeBeforeThrowing);
				}
			}
		}
		else
		{
			for (int i = 0; i < MaxCoins; i++)
			{
				TimeBeforeThrowing += 0.2f;
				Invoke("SpawnCoin", TimeBeforeThrowing);
				if (i == MaxCoins - 1)
				{
					TimeBeforeThrowing += 5;
					Invoke("EndLvl", TimeBeforeThrowing);
				}
			}
		}
	}
	private void SpawnCoin()
	{
		_throwedObject = Instantiate(Coin, SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
		//NumberOfObject++;
		_throwedObjectScript = _throwedObject.GetComponent<ThrowingObject>();
		_throwedObjectScript.SetCounter(_counter);
		_throwedObjectScript.SetForce(Random.Range(3000f, 3501f));
		_throwedObjectScript.TrowObjectUp();
	}
	private void SpawnObject()
	{
		_throwedObject = Instantiate(FallingObjects[Random.Range(0, FallingObjects.Length)], SpawnPoints[_currentLvlPreset.TrowingObjects[NumberOfObject].SpawnPoint].position, Quaternion.identity);
		NumberOfObject++;
		_throwedObjectScript = _throwedObject.GetComponent<ThrowingObject>();
		_throwedObjectScript.SetCounter(_counter);
		_throwedObjectScript.SetForce(_currentLvlPreset.TrowingObjects[NumberOfObject - 1].ForceOfThrowing);
		_throwedObjectScript.TrowObjectUp();
	}
	private void EndLvl()
	{
		_mainGameController.EndGame();
	}
}
