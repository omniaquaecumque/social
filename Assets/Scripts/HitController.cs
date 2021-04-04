using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public float gravity = 10.0f;
	private float pushForce;
	private Vector3 pushDir;

	public Rigidbody rb;

	public void HitPlayer(Vector3 velocityF, float time)
	{
		rb.velocity = velocityF;

		pushForce = velocityF.magnitude;
		pushDir = Vector3.Normalize(velocityF);
		StartCoroutine(Decrease(velocityF.magnitude, time));
	}

	private IEnumerator Decrease(float value, float duration)
	{
		float delta = 0;
		delta = value / duration;

		for (float t = 0; t < duration; t += Time.deltaTime)
		{
			yield return null;

            pushForce = pushForce - Time.deltaTime * delta;
            pushForce = pushForce < 0 ? 0 : pushForce;
			
			rb.AddForce(new Vector3(0, gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
		}
	}

}
