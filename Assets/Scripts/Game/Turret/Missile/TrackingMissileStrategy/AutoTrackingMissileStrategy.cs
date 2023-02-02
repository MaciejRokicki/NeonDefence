using UnityEngine;

public class AutoTrackingMissileStrategy : TrackingMissileStrategy
{
    private GameObject target;
    private bool changeDirection = true;

    public AutoTrackingMissileStrategy(GameObject go, Turret turret, GameObject target) : base(go, turret) 
    {
        this.target = target;
        direction = (target.transform.position - baseGameObject.transform.position).normalized;
    }

    public override void Update()
    {
        if(!target.activeSelf)
        {
            changeDirection = false;
        }

        if (changeDirection)
        {
            direction = (target.transform.position - baseGameObject.transform.position).normalized;
        }

        baseGameObject.transform.position += direction * turret.MissileSpeed * Time.deltaTime;
        baseGameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        changeDirection = false;
    }
}
