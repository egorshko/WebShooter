using UnityEngine;

public class WebToTheWall : MonoBehaviour
{
	public GameObject SpiderWeb;

	private Rigidbody _rigidbody;
	private GameObject Web;
	private Counter _counter;
	private Vector3 CustomWebPosition;
	private Vector3 ThrowingVector;
	private Vector3 RotationVector;
	private float _throwingForce = 3000f;
	private bool IsObjectStucked;
	private bool IsObjectedWebed;
	private bool NeedToRotate = true;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();

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
			//Destroy(collision.gameObject);
			SphereCollider collider = collision.gameObject.GetComponent<SphereCollider>();
			collider.isTrigger = true;
			IsObjectedWebed = true;
			ThrowingVector = transform.position;
			ThrowingVector.z = 12000f;
			ThrowingVector.x = (transform.position.x - collision.transform.position.x) * 1000;
			ThrowingVector.y = (transform.position.y - collision.transform.position.y) * 2500;
			_rigidbody.AddForce(ThrowingVector);
		}


	}
	private void GetObjectStucked()
	{
		//_counter.IncreaseAmountOfCollectedObjects();
		ThrowingStickman stickman = GetComponentInParent<ThrowingStickman>();
		if(stickman != null)
		{
			stickman.NeedToRotate = false;
		}
		NeedToRotate = false;
		gameObject.tag = "Wall";
		_rigidbody.isKinematic = true;
		//IsStucked = true;
		Web = Instantiate(SpiderWeb, transform.position + CustomWebPosition, Quaternion.identity);
		Web.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		Web.transform.Rotate(new Vector3(0, 180f, Random.Range(0, 360f)));
	}
}
