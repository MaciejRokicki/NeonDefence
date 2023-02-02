using UnityEngine;

public class BasicTrackingMissileStrategy : TrackingMissileStrategy
{
    public BasicTrackingMissileStrategy(GameObject baseGameObject, Turret turret) : base(baseGameObject, turret) { }

    public override void Update()
    {
        baseGameObject.transform.position += baseGameObject.transform.rotation * direction * turret.MissileSpeed * Time.deltaTime;
    }
}
