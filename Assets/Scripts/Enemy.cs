using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Rigidbody2D _body;
	private Transform _patrolPoint;
	private float _patrolDistance = 6f;
	private float _speed = 5f;
	private GameObject _player;
	private bool _movingRight;
	private bool _isPatrol = true;
	private void Start()
	{
		_patrolPoint = GameObject.FindGameObjectWithTag("PatrolPoint").transform;
		_body = GetComponent<Rigidbody2D>();
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	private void FixedUpdate()
	{
		if(Vector2.Distance(transform.position, _player.transform.position) > _patrolDistance)
		{
			_isPatrol = true;
		}
		if(Vector2.Distance(transform.position, _player.transform.position) < _patrolDistance)
		{
			_isPatrol = false;
		}
		if(_isPatrol)
		{
			Patrol();
		}
		if(!_isPatrol)
		{
			Pursuit();
		}
	}
	private void Patrol()
	{
		if(transform.position.x >= _patrolPoint.position.x + _patrolDistance)
		{
			_movingRight = false;
		}
		else if(transform.position.x <= _patrolPoint.position.x - _patrolDistance)
		{
			_movingRight = true;
		}
		if(_movingRight)
		{
			if(transform.localScale.x < 0)
			{
				Flip();
			}
			transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
		}
		else
		{
			if(transform.localScale.x > 0)
			{
				Flip();
			}
			transform.position = new Vector2(transform.position.x + -_speed * Time.deltaTime, transform.position.y);
		}
	}
	private void Pursuit()
	{
		float step = _speed * Time.deltaTime;
		if(_player.transform.position.x > transform.position.x && transform.localScale.x < 0)
		{
			Flip();
		}
		if(_player.transform.position.x < transform.position.x && transform.localScale.x > 0)
		{
			Flip();
		}
		transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, step);
	}
	private void ComeBack()
	{

	}
	private void Flip()
	{
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.y);
	}
}
