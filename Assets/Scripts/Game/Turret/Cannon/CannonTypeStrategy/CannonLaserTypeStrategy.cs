﻿using UnityEngine;

public class CannonLaserTypeStrategy : CannonTypeStrategy
{
    private GameObject laser;
    private GameObject target;

    private bool isLaserActive = false;
    private float activationTimer = 0.0f;
    private float deactiveLaserTimer = 0.0f;

    public CannonLaserTypeStrategy(Cannon cannon, Turret turret, GameObject laser) : base(cannon, turret) 
    {
        this.laser = laser;
    }

    public override void Start()
    {
        laser.GetComponent<Missile>().SetTurret(turret);
        cannon.GetComponent<SpriteRenderer>().sortingOrder = 5;
    }

    public override void Update()
    {
        if (cannon.target != null && cannon.target.activeSelf)
        {
            cannon.GetComponent<Cannon>().RotateToTarget();

            RaycastHit2D hit = Physics2D.Raycast(
                cannon.transform.position, 
                cannon.transform.rotation * Vector2.up, 
                turret.Range + cannon.transform.localScale.x / 2, 
                cannon.enemyLayerMask);

            if (hit && hit.transform.tag == "Enemy")
            {
                target = hit.transform.gameObject;

                if(isLaserActive)
                {
                    Shoot();
                }
                else
                {
                    ActivateLaser();
                }
            }
            else
            {
                if(isLaserActive)
                {
                    float range = turret.Range + cannon.transform.localScale.x / 2;

                    laser.GetComponent<SpriteRenderer>().size 
                        = laser.GetComponent<BoxCollider2D>().size 
                        = new Vector2(turret.variant.MissileSpriteSize.x, range);
                    laser.transform.localPosition = new Vector2(0.0f, range / 2);
                }
            }
        }
        else
        {
            DeactivateLaser();
            cannon.FindTarget();
        }
    }

    private void ActivateLaser()
    {
        deactiveLaserTimer = 0.0f;
        activationTimer += Time.deltaTime;

        float x = turret.variant.MissileSpriteSize.x * activationTimer;
        float y = GetLaserRange();

        x = Mathf.Clamp(x, 0.0f, turret.variant.MissileSpriteSize.x);

        laser.transform.localPosition = new Vector2(0.0f, y / 2);
        laser.GetComponent<SpriteRenderer>().size = new Vector2(x, y);

        if (activationTimer > turret.LaserActivationTime)
        {
            isLaserActive = true;

            activationTimer = 0.0f;
        }
    }

    private void DeactivateLaser()
    {
        deactiveLaserTimer += Time.deltaTime;

        if (deactiveLaserTimer > turret.LaserDeactivationTime)
        {
            isLaserActive = false;
            laser.GetComponent<SpriteRenderer>().size = laser.GetComponent<BoxCollider2D>().size = Vector2.zero;

            deactiveLaserTimer = 0.0f;
        }
    }

    protected override void Shoot()
    {
        float distance = GetLaserRange();

        laser.GetComponent<SpriteRenderer>().size = laser.GetComponent<BoxCollider2D>().size = new Vector2(turret.variant.MissileSpriteSize.x, distance);
        laser.transform.localPosition = new Vector2(0.0f, distance / 2);
    }

    private float GetLaserRange()
    {
        if (turret.variant.PenetrationMissile)
        {
            return turret.Range + cannon.transform.localScale.x / 2;
        }
        else
        {
            return Vector2.Distance(cannon.transform.position, target.transform.position);
        }
    }
}