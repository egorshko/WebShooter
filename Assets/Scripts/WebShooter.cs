using UnityEngine;

public class WebShooter : MonoBehaviour
{
	public GameObject Web;
	public float WebSpeed;
	public Animator RightHandAnimator;
	public Animator LeftHandAnimator;
	public Transform RightHandTransform;
	public Transform LeftHandTransform;
	public Vector3 RightHandPosition;
	public Vector3 LeftHandPosition;


	private GameObject WebObject;
	private WebContainer _webContainer;
	private Vector3 TouchPosition;

	private void Start()
	{
		_webContainer = FindObjectOfType<WebContainer>();
		RightHandPosition = RightHandTransform.position;
		LeftHandPosition = LeftHandTransform.position;
	}
	public void ShootWeb(Vector3 PositionOfTouch)
	{
		if (_webContainer.CheckWeb())
		{
			_webContainer.DecreaseWebAmount();
			Destroy(WebObject);
			if (PositionOfTouch.x > 0)
			{
				RightHandPosition = RightHandTransform.position;
				WebObject = Instantiate(Web, new Vector3(RightHandPosition.x, RightHandPosition.y, RightHandPosition.z), Quaternion.identity);
				RightHandAnimator.SetTrigger("Shoot");
				PositionOfTouch.x -= RightHandPosition.x;
				PositionOfTouch.y -= RightHandPosition.y;
			}
			else
			{
				LeftHandPosition = LeftHandTransform.position;
				WebObject = Instantiate(Web, new Vector3(LeftHandPosition.x, LeftHandPosition.y, LeftHandPosition.z), Quaternion.identity);
				LeftHandAnimator.SetTrigger("Shoot");
				PositionOfTouch.x -= LeftHandPosition.x;
				PositionOfTouch.y -= LeftHandPosition.y;
			}
			PositionOfTouch.z = 6f;//удачно подобрав значение можно хорошей точности в стрельбе добиться
			TouchPosition = PositionOfTouch + new Vector3(0, 5f, 0);
		}
	}
	private void FixedUpdate()
	{
		MoveWeb();
	}
	private void MoveWeb()
	{
		if (WebObject != null)
		{
			WebObject.transform.Translate(TouchPosition * Time.deltaTime * WebSpeed, Space.World);
		}
	}
}
