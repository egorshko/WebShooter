using UnityEngine;

public class Wall : MonoBehaviour
{
	public GameObject Web;
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
		if (collision.gameObject.CompareTag("Web"))
		{
			Debug.Log("Eeeeeee");
			Instantiate(Web, collision.gameObject.transform.position + new Vector3(0,0,-0.4f), Quaternion.Euler(0,180f, Random.Range(0,360)));
		}
	}
}
