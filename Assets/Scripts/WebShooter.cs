using UnityEngine;

public class WebShooter : MonoBehaviour
{
    public GameObject Web;
    public Animator RightHandAnimator;
    public Animator LeftHandAnimator;
    public Transform RightHandTransform;
    public Transform LeftHandTransform;
    private GameObject WebObject;
    private Vector3 TouchPosition;
    public Vector3 RightHandPosition;
    public Vector3 LeftHandPosition;
    public void ShootWeb(Vector3 PositionOfTouch)
    {
        Destroy(WebObject);
        if(PositionOfTouch.x > 0)
        {
            WebObject = Instantiate(Web, new Vector3(RightHandPosition.x, RightHandPosition.y, -5.334535f), Quaternion.identity);
            RightHandAnimator.SetTrigger("Shoot");
            PositionOfTouch.x -= RightHandPosition.x;
            PositionOfTouch.y -= RightHandPosition.y;
            RightHandTransform.LookAt(PositionOfTouch * -1);
        }
        else
        {
            WebObject = Instantiate(Web, new Vector3(LeftHandPosition.x, LeftHandPosition.y, -5.334535f), Quaternion.identity);
            LeftHandAnimator.SetTrigger("Shoot");
            PositionOfTouch.x -= LeftHandPosition.x;
            PositionOfTouch.y -= LeftHandPosition.y;
        }
        PositionOfTouch.z = 5.1f;//удачно подобрав значение можно хорошей точности в стрельбе добиться
        TouchPosition = PositionOfTouch;
    }
    private void FixedUpdate()
    {
        MoveWeb();
    }
    private void MoveWeb()
    {
        if(WebObject!= null)
        {
            WebObject.transform.Translate(TouchPosition * Time.deltaTime * 15f, Space.World);
        }
    }
}
