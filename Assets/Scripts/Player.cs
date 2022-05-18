using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _body;
	private Collider2D _collider;
	[SerializeField]private float _speed = 7f;
	[SerializeField]private float _jumpForce = 10f;
	private bool _isRight = true;
	private void Start()
	{
		_body = GetComponent<Rigidbody2D>();
		_collider = GetComponent<Collider2D>();
	}
	private void FixedUpdate()
	{
		Walk();
		if(Input.GetKey(KeyCode.Space))
		{
			Jump();
		}
		if(_isRight == true && _body.velocity.x < 0)
		{
			Flip();
		}
		if(_isRight == false && _body.velocity.x > 0)
		{
			Flip();
		}
	}
	private void Walk()
	{
		float direction = Input.GetAxis("Horizontal");
		_body.velocity = new Vector2(direction * _speed, _body.velocity.y);
	}
	private void Jump()
	{
		Vector3 max = _collider.bounds.max;
		Vector3 min = _collider.bounds.min;
		Vector2 corner_1 = new Vector2(max.x - 0.5f, min.y - 0.1f);
		Vector2 corner_2 = new Vector2(min.x + 0.5f, min.y - 0.2f);
		Collider2D[] hits = Physics2D.OverlapAreaAll(corner_1, corner_2);
		bool grounded = false;
		for(int i = 0;i < hits.Length;i++)
		{
			if(hits[i].gameObject.tag == "Block")
			{
				grounded = true;
			}
			if(grounded)
			{
				_body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
			}
		}
	}
	private void Flip()
	{
		_isRight = !_isRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
}
