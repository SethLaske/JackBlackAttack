using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public CardFormation defaultSpawnPositions;     //Will clear eventually
    [SerializeField] private CardFormation[] allFormations;

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SpawnTile spawnTile;

    [SerializeField] private float roomWidth;
    [SerializeField] private float roomHeight;

    private void Start()
    {
        int cardValue = Random.Range(0, allFormations.Length);
        Debug.Log("Card Value: " + (cardValue + 1));
        SpawnTiles(allFormations[cardValue]);
    }

    public void SpawnTiles(CardFormation spawnFormation) {
        List<Vector2> spawnPositons = spawnFormation.GetSpawnPositions();

        foreach (Vector2 spawnPosition in spawnPositons) {
            Vector3 tilePosition = transform.position + new Vector3((int)(spawnPosition.x * roomWidth), (int)(spawnPosition.y * roomHeight), 0);
            SpawnTile tile = Instantiate(spawnTile, tilePosition, Quaternion.identity);
            tile.SetLevelManager(levelManager);
            tile.SpawnEnemy();
        }

        levelManager.SetEnemyCount(spawnPositons.Count);
    }
}
