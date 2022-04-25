using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShootController : MonoBehaviour {
	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private float shootDelay;

	[SerializeField]
	private GameObject projectile;

	private Action state;
	private int layerMask;
	private bool inShoot;
	[SerializeField]
	private float moveTime;
	private float originalTime;

	[SerializeField]
	private Entity entity;

	private AudioSource deathSound;

	void Awake() {
		state = Action.Move;
		layerMask = 1 << 3;
		inShoot = false;
		originalTime = moveTime;
		deathSound = GetComponent<AudioSource>();
	}

	void Update() {
		float newX = transform.position.x;
		float newY = transform.position.y;
		moveTime -= Time.deltaTime;
		if( state == Action.Move ) {
			newY -= moveSpeed * Time.deltaTime;
		}
		else if( state == Action.Shoot ) {
			if( !inShoot ) {
				StartCoroutine(Shoot());
			}
		}
		transform.position = new Vector2(newX, newY);
	}

	private void FixedUpdate() {
		if( moveTime <= 0 ) {
			state = Action.Shoot;
			moveTime = originalTime;
		}

	}

	void OnTriggerEnter2D(Collider2D collision) {
		if( collision.gameObject.CompareTag("Bullet") ) {
			deathSound.Play();
			entity.Deactivate();
			gameObject.SetActive(false);
			Destroy(gameObject, deathSound.clip.length);
		}
	}

	private IEnumerator Shoot() {
		inShoot = true;
		Instantiate(projectile,transform.position, Quaternion.identity);
		state = Action.Move;
		yield return new WaitForSeconds(shootDelay);
		inShoot = false;
	}
	private enum Action {
		Move, Shoot
	}
}