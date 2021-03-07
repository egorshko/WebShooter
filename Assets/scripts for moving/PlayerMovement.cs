using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Transform[] Points;
	private MovementPoints[] _movementPoints;
	public float MaxSpeed;
	public float MaxRotationSpeed;

	//private ShootingController _shootingController;
	private GameObject[] _enemyObjects;
	private Animator[] _enemyAnimators;
	private float RotationSideX;
	private float RotationSideY;
	private float RotationSideZ;
	private float _speed;
	private float _rotationSpeed;
	private int _currentPointNum;
	private int TimesOfRotateX;
	private int TimesOfRotateY;
	private int TimesOfRotateZ;
	[SerializeField] private bool _needToMove;

	private void Start()
	{
		//_enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
		//_enemyAnimators = new Animator[_enemyObjects.Length];
		/*for (int i = 0; i < _enemyAnimators.Length; i++)
		{
			_enemyAnimators[i] = _enemyObjects[i].GetComponent<Animator>();
		}*/
		//_shootingController = FindObjectOfType<ShootingController>();
		_speed = MaxSpeed;
		_movementPoints = new MovementPoints[Points.Length];
		for (int i = 0; i < _movementPoints.Length; i++)
		{
			_movementPoints[i] = Points[i].gameObject.GetComponent<MovementPoints>();
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (_movementPoints[_currentPointNum].NeedToSlowDownTime)
			SlowDownTime();
		if (_movementPoints[_currentPointNum].NeedToSpeedUpTime)
			SpeedUpTime();
		if (_movementPoints[_currentPointNum].NeedToRotate)
		{
			RotatePlayer(_movementPoints[_currentPointNum].RotationVector);
		}
		ChangePoint();
	}
	private void FixedUpdate()
	{
		if (_needToMove && _currentPointNum < Points.Length)
		{
			transform.position = Vector3.MoveTowards(transform.position, Points[_currentPointNum].position, _speed);
		}
	}
	public void ChangePoint()
	{
		_currentPointNum++;
	}
	private void SlowDownTime()
	{
		_speed = MaxSpeed / 10f;
		_rotationSpeed = MaxRotationSpeed / 10f;
		//_shootingController.SlowDownTime();
		for (int i = 0; i < _enemyAnimators.Length; i++)
		{
			_enemyAnimators[i].SetFloat("SpeedMultiplier", 0.2f);
		}
	}
	private void SpeedUpTime()
	{
		_speed = MaxSpeed;
		_rotationSpeed = MaxRotationSpeed;
		//_shootingController.SpeedUpTime();
		for (int i = 0; i < _enemyAnimators.Length; i++)
		{
			_enemyAnimators[i].SetFloat("SpeedMultiplier", 1f);
		}
	}
	public void RotatePlayer(Vector3 RotatingVector)
	{
		if (RotatingVector.x != 0)
		{
			if (RotatingVector.x > 0)
				RotationSideX = 1;
			else
				RotationSideX = -1;
			TimesOfRotateX = (int)(ConvertToPositive(RotatingVector.x) / _rotationSpeed);
		}
		else
		{
			RotationSideX = 0;
		}
		if (RotatingVector.y != 0)
		{
			if (RotatingVector.y > 0)
				RotationSideY = 1;
			else
				RotationSideY = -1;
			TimesOfRotateY = (int)(ConvertToPositive(RotatingVector.y) / _rotationSpeed);
		}
		else
		{
			RotationSideY = 0;
		}
		if (RotatingVector.z != 0)
		{
			if (RotatingVector.z > 0)
				RotationSideZ = 1;
			else
				RotationSideZ = -1;
			TimesOfRotateZ = (int)(ConvertToPositive(RotatingVector.z) / _rotationSpeed);
		}
		else
		{
			RotationSideZ = 0;
		}


		TimesOfRotateX = (int)(ConvertToPositive(RotatingVector.x) / _rotationSpeed);
		if (RotatingVector.y != 0)
			TimesOfRotateY = (int)(ConvertToPositive(RotatingVector.y) / _rotationSpeed);
		if (RotatingVector.z != 0)
			TimesOfRotateZ = (int)(ConvertToPositive(RotatingVector.z) / _rotationSpeed);


		int maxnum = 0;
		if (TimesOfRotateX > TimesOfRotateY && TimesOfRotateX > TimesOfRotateY)
		{
			maxnum = TimesOfRotateX;
		}
		if (TimesOfRotateY > TimesOfRotateX && TimesOfRotateY > TimesOfRotateZ)
		{
			maxnum = TimesOfRotateY;
		}
		if (TimesOfRotateZ > TimesOfRotateY && TimesOfRotateZ > TimesOfRotateX)
		{
			maxnum = TimesOfRotateZ;
		}
		Debug.Log(maxnum);
		for (int i = 0; i < maxnum; i++)
		{
			if (i <= TimesOfRotateX)
			{
				Invoke("RotateALittleX", 0.01f * i);
			}
			if (i <= TimesOfRotateY)
			{
				Invoke("RotateALittleY", 0.01f * i);
			}
			if (i <= TimesOfRotateZ)
			{
				Invoke("RotateALittleZ", 0.01f * i);
			}

		}
		transform.Rotate(RotatingVector);
	}
	private void RotateALittleX()
	{
		transform.Rotate(new Vector3(_rotationSpeed * RotationSideX, 0, 0));
	}
	private void RotateALittleY()
	{
		transform.Rotate(new Vector3(0, _rotationSpeed * RotationSideY, 0));
	}
	private void RotateALittleZ()
	{
		transform.Rotate(new Vector3(0, 0, _rotationSpeed * RotationSideZ));
	}
	private float ConvertToPositive(float num)
	{
		if (num > 0)
			return num;
		if (num < 0)
			return -num;
		return 0;
	}
}
