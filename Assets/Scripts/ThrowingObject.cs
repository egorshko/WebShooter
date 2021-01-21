using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
	public GameObject SpiderWeb;
	public bool IsStucked;

	private Rigidbody _rigidbody;
	private GameObject Web;
	private Counter _counter;
	private Vector3 CustomWebPosition;
	private Vector3 ThrowingVector;
	private float _throwingForce = 3000f;
	private bool IsObjectStucked;
	private bool IsObjectedWebed;
	private void Awake()
	{
		IsStucked = false;
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.isKinematic = true;
		CustomWebPosition = new Vector3(0, 0, -0.1f);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Wall") && !IsObjectStucked && IsObjectedWebed)
		{
			IsObjectStucked = true;
			GetObjectStucked();
		}
		if (collision.gameObject.CompareTag("Web"))
		{
			IsObjectedWebed = true;
			ThrowingVector = transform.position;
			ThrowingVector.z = 7500f;
			ThrowingVector.x = (transform.position.x - collision.transform.position.x) * 1000;
			ThrowingVector.y = (transform.position.y - collision.transform.position.y) * 2500;
			_rigidbody.AddForce(ThrowingVector);
		}
	}
	private void GetObjectStucked()
	{
		gameObject.tag = "Wall";
		_counter.IncreaseAmountOfCollectedObjects();
		_rigidbody.isKinematic = true;
		IsStucked = true;
		Web = Instantiate(SpiderWeb, transform.position + CustomWebPosition, Quaternion.identity);
		Web.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360f)));
	}
	public void TrowObjectUp()
	{
		_rigidbody.isKinematic = false;
		_rigidbody.AddForce(Vector3.up * _throwingForce);
	}
	public void SetCounter(Counter counter)
	{
		_counter = counter;
	}
	public void SetForce(float force)
	{
		_throwingForce = force;
	}
}
