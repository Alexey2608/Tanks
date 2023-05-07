using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage = 1;
	public bool isEnemy;
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.transform.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}

		if(coll.transform.CompareTag("Block"))
		{
			Destroy(coll.transform.gameObject);
			Destroy(gameObject);
		}

		if(!isEnemy)
		{
			if(coll.transform.CompareTag("Enemy"))
			{
				EnemyControl enemy = coll.transform.GetComponent<EnemyControl>();
				enemy.HP -= damage;
				Destroy(gameObject);
			}
		}
		else
		{
			if(coll.transform.CompareTag("Player"))
			{
				PlayerControl player = coll.transform.GetComponent<PlayerControl>();
				player.HP -= damage;
				Destroy(gameObject);
			}
		}
	}
}