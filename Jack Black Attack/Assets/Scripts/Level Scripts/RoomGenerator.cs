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

    private GameObject spawnTileParent;

    [SerializeField] private SpawnTable[] spawnTables;

    private void Start()
    {
        //int cardValue = Random.Range(0, allFormations.Length);
        
        //SpawnTiles(allFormations[cardValue]);

        spawnTileParent = new GameObject();
        spawnTileParent.name = "SpawnTileParent";
        spawnTables = Resources.LoadAll<SpawnTable>("SpawnTables");
    }

    /// <summary>
    /// Pass in 1 - 13 (1 is Ace, 11 Jack, 12 Queen, 13 King)
    /// </summary>
    public void StartRoomGeneration(int cardValue) {
        if (cardValue < 1)
        {
            Debug.LogError("NO CARD VALUES OF UNDER 1");
            cardValue = 1;
        }
        else if (cardValue > 13) {
            Debug.LogError("NO CARD VALUES OF OVER 13");
            cardValue = 13;
        }
        SpawnTiles(allFormations[cardValue - 1]);
    }
    public void SpawnTiles(CardFormation spawnFormation) {

        List<Vector2> spawnPositons = spawnFormation.GetSpawnPositions();

        SpawnTable activeSpawnTable = spawnTables[Random.Range(0, spawnTables.Length)];
        //Debug.Log("Spawned Tile: " + activeSpawnTable.name);
        //Debug.Log("Total Options: " + (spawnTables.Length - 1));

        foreach (Vector2 spawnPosition in spawnPositons) {
            Vector3 tilePosition = transform.position + new Vector3((int)(spawnPosition.x * roomWidth), (int)(spawnPosition.y * roomHeight), 0);
            SpawnTile tile = Instantiate(spawnTile, tilePosition, Quaternion.identity);
            tile.transform.parent = spawnTileParent.transform;
            tile.SetLevelManager(levelManager);
            tile.SetEnemy(activeSpawnTable.GetSpawnedEnemy());


            tile.ActivateSpawnSequence();
        }

        levelManager.SetEnemyCount(spawnPositons.Count);
    }

    public void ClearTiles() {
        //ToDo: Remove the spawn tiles via animation
        /*foreach (Transform child in spawnTileParent.transform)
        {
            Destroy(child.gameObject);
        }*/
        foreach (SpawnTile spawnTile in spawnTileParent.GetComponentsInChildren<SpawnTile>()) {
            spawnTile.ActivateDestroySequence();
        }
    }
}
