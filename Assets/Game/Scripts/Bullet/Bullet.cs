using UnityEngine;
using Modules.Utils;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Game;

public class Bullet:MonoBehaviour
{
    [SerializeField] public TeamType Team = TeamType.None;
    public Vector2 Direction;

    [SerializeField] public int Damage;
    [SerializeField] public float Speed;

    public void Initialize(BulletConfig config, Vector2 direction, TeamType team)
    {
        Damage = config.Damage;
        Speed = config.Speed;
        Direction = direction;
        Team = team;

        SetupLayer(team);
        SetupVisual(team, config);
    }

    private void SetupLayer(TeamType team)
    {
        gameObject.layer = team switch
        {
            TeamType.None => LayerMask.NameToLayer("Default"),
            TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
            TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void SetupVisual(TeamType team, BulletConfig config)
    {
        // Визуальная настройка делегируется отдельному компоненту
        var visual = GetComponent<BulletVisual>();
        visual?.SetTeamColor(team, config);
    }
}
