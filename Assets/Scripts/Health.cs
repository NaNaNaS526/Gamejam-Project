using UnityEngine;

public class Health : MonoBehaviour
{
	private float _startingHealth = 3;
	public float currentHealth{get; private set;}
	private void Awake()
	{
		currentHealth = _startingHealth;
	}
	public void TakeDamage(int damage)
	{
		currentHealth = Mathf.Clamp(currentHealth - damage, 0, _startingHealth);
		if(currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}
