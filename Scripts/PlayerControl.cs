using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int HP = 2;
	public Transform explosion;
	public float speed = 0.05f;
	public Animator _animator;
	public Transform tank;
	public Transform gun;
	public Rigidbody2D bullet;
	public float bulletSpeed = 3f;
	private Rigidbody2D body;
	private Vector2 moveDirection;
	private Vector3 rotation;

	void Start ()
	{
		_animator.speed = 0;
		body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		body.AddForce(moveDirection * speed);

		if(Mathf.Abs(body.velocity.x) > speed/100f)
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed/100f, body.velocity.y);
		}
		if(Mathf.Abs(body.velocity.y) > speed/100f)
		{
			body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed/100f);
		}
	}

	void Update () 
	{
		if(Input.GetKey(KeyCode.W))
		{
			moveDirection = new Vector2(0, 1);
			rotation = new Vector3(0, 0, 0);
			_animator.speed = 1;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			moveDirection = new Vector2(0, -1);
			rotation = new Vector3(0, 0, 180);
			_animator.speed = 1;
		}
		else if(Input.GetKey(KeyCode.A))
		{
			moveDirection = new Vector2(-1, 0);
			rotation = new Vector3(0, 0, 90);
			_animator.speed = 1;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			moveDirection = new Vector2(1, 0);
			rotation = new Vector3(0, 0, -90);
			_animator.speed = 1;
		}
		else
		{
			moveDirection = new Vector2(0, 0);
			_animator.speed = 0;
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			Rigidbody2D bulletInstance = Instantiate(bullet, gun.position, Quaternion.identity) as Rigidbody2D;
			bulletInstance.velocity = gun.TransformDirection(Vector2.up * bulletSpeed);
		}
		
		tank.localRotation = Quaternion.Euler(rotation);

		if(HP <= 0)
		{
			GameControl.playerDead = true;
			float randomZ = Random.Range(0, 360f);
			Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, randomZ));
			Destroy(gameObject);
		}
	}
}