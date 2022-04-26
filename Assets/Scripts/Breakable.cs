using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Entity))]
public class Breakable : MonoBehaviour
{
    [SerializeField]
    private Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
