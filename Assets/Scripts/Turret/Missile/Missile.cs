using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(Missile))]
public abstract class Missile : MonoBehaviour
{
    protected Turret turret;
    protected SpriteRenderer spriteRenderer;

    protected void Awake()
    {
        turret = transform.parent.GetComponent<Turret>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        spriteRenderer.size = GetComponent<BoxCollider2D>().size = turret.data.missileSize;
        spriteRenderer.sprite = turret.data.missileSprite;
        spriteRenderer.material = turret.data.missileMaterial;
    }
}
