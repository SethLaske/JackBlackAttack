using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropPair
{
    public GameObject item;
    public int spawnChance;
}

[CreateAssetMenu]
public class DropTable : ScriptableObject
{
    public List<DropPair> dropPairs;

    public int spawnChancesSum;



    public GameObject GetDrop()
    {

        if (spawnChancesSum <= 0)
        {
            spawnChancesSum = 0;

            foreach (DropPair pair in dropPairs)
            {
                spawnChancesSum += pair.spawnChance;
            }
        }

        int sum = Random.Range(0, spawnChancesSum);

        foreach (DropPair pair in dropPairs)
        {
            if (sum < pair.spawnChance)
            {
                return pair.item;
            }
            sum -= pair.spawnChance;
        }

        return null;
    }
}
