using UnityEngine;
using System.Collections;

public class bird : MonoBehaviour {

    public Transform target;
    public float speed = 5;
    public float lifetime = 30;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }
}
