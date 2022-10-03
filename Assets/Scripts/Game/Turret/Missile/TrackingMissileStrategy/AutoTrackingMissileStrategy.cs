using UnityEngine;

public class AutoTrackingMissileStrategy : TrackingMissileStrategy
{
    private GameObject target;

    public AutoTrackingMissileStrategy(GameObject go, Turret turret, GameObject target) : base(go, turret) 
    {
        this.target = target;
    }

    public override void Update()
    {
        if (target != null)
        {
            direction = (target.transform.position - baseGameObject.transform.position).normalized;
        }

        baseGameObject.transform.position += direction * turret.missileSpeed * Time.deltaTime;
        baseGameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f);
    }
}
