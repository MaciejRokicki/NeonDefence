using UnityEngine;

public abstract class TrackingMissileStrategy
{
    protected GameObject baseGameObject;
    protected Turret turret;
    protected Vector3 direction = Vector3.up;

    public TrackingMissileStrategy(GameObject baseGameObject, Turret turret)
    {
        this.baseGameObject = baseGameObject;
        this.turret = turret;
    }

    public abstract void Update();
}
