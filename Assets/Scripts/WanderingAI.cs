using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
	public float speed = 3.0f;
	public float obstacleRange = 5.0f;

	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	public Animator animator;
	private int _state;

	private bool _alive;

	void Start()
	{
		animator = GetComponent<Animator>();
		_alive = true;
	}

	void Update()
	{
		if (_alive)
		{
			_state = 0;
			
			transform.Translate(0, 0, speed * Time.deltaTime);
			

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast(ray, 0.75f, out hit))
			{
				GameObject hitObject = hit.transform.gameObject;
				if (hitObject.GetComponent<PlayerCharacter>())
				{
					if (_fireball == null)
					{
						_fireball = Instantiate(fireballPrefab) as GameObject;
						_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
						_fireball.transform.rotation = transform.rotation;
					}
				}
				else if (hit.distance < obstacleRange)
				{
					float angle = Random.Range(-110, 110);
					transform.Rotate(0, angle, 0);
				}
			}
		}
		else { _state = 1; }

		animator.SetInteger("State", _state);
	}

	public void SetAlive(bool alive)
	{
		_alive = alive;
		
	}
}
