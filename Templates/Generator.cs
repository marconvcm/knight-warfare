using Godot;
using System;

public partial class Generator : StaticBody2D
{
    [Export]
    public PackedScene EnemyPrefab; // Prefab do inimigo

    [Export]
    public NodePath[] SpawnPoints; // Array de spawn points

    [Export]
    public float SpawnInterval = 2.0f; // Intervalo entre os spawns

    private float _timeSinceLastSpawn = 0.0f;

    public override void _Process(double delta)
    {
        _timeSinceLastSpawn += (float)delta;

        if (_timeSinceLastSpawn >= SpawnInterval)
        {
            SpawnEnemy();
            _timeSinceLastSpawn = 0.0f;
        }
    }

    private void SpawnEnemy()
    {
        // Seleciona um spawn point aleatório
        int spawnIndex = (int)(GD.Randi() % SpawnPoints.Length);
        Node2D spawnPoint = GetNode<Node2D>(SpawnPoints[spawnIndex]);

        // Instancia o inimigo
        Node2D enemyInstance = (Node2D)EnemyPrefab.Instantiate();
        enemyInstance.Position = spawnPoint.Position;

        // Adiciona o inimigo à cena
        GetParent().AddChild(enemyInstance);
    }
}
