using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SpawnPair {
    public BaseEnemy enemy;
    public int spawnChance;
}

[CreateAssetMenu]
public class SpawnTable : ScriptableObject
{
    public List<SpawnPair> spawnPairs;

    public int spawnChancesSum;



    public BaseEnemy GetSpawnedEnemy() {

        if (spawnChancesSum <= 0)
        {
            spawnChancesSum = 0;

            foreach (SpawnPair pair in spawnPairs)
            {
                spawnChancesSum += pair.spawnChance;
            }
        }

        int sum = Random.Range(0, spawnChancesSum);

        foreach (SpawnPair pair in spawnPairs) {
            if (sum < pair.spawnChance) {
                return pair.enemy;
            }
            sum -= pair.spawnChance;
        }

        return null;
    }
}
