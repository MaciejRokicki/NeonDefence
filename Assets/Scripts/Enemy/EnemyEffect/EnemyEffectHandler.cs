using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour 
{
    [SerializeField]
    private List<EnemyEffect> effects;

    private void Awake()
    {
        effects = new List<EnemyEffect>();
    }

    private void Update()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i] != null)
            {
                effects[i].Update();
            }
        }
    }

    public void ApplyEffect(EnemyEffect enemyEffect)
    {
        bool isEffectDuplicated = false;

        foreach (EnemyEffect effect in effects)
        {
            if (effect.CheckDuplicates(enemyEffect))
            {
                isEffectDuplicated = true;
                effect.turret = enemyEffect.turret;
                break;
            }
        }

        if (!isEffectDuplicated)
        {
            effects.Add(enemyEffect);
            enemyEffect.OnEffectStart();
        }
    }

    public void RemoveEffect(EnemyEffect enemyEffect)
    {
        enemyEffect.OnEffectEnd();
        effects.Remove(enemyEffect);
    }

    public void RemoveEffects(Turret turret)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            EnemyEffect enemyEffect = effects[i];

            if (enemyEffect.turret == turret)
            {
                RemoveEffect(enemyEffect);
            }
        }
    }
}
