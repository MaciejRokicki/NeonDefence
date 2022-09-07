using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Turret turret;
    [SerializeField]
    private GameObject target;

    private void Awake()
    {
        turret = transform.parent.GetComponent<Turret>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (target != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        Vector3 dir = turret.transform.position - target.transform.position;
        //Vector3 dir2 = transform.position - turret.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90.0f;

        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        Quaternion lerpedRotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turret.rotationSpeed);
        //transform.position = turret.transform.position + lerpedRotation * dir2;
        transform.rotation = lerpedRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
