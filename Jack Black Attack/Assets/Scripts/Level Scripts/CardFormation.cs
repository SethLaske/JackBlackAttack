using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFormation : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private float initialWidth;
    [SerializeField] private float initialHeight;

    public List<Vector2> GetSpawnPositions() {
        List<Vector2> normalizedPoints = new List<Vector2>();

        foreach (Transform point in spawnPoints) {
            normalizedPoints.Add( new Vector2(point.localPosition.x / initialWidth, point.localPosition.y / initialHeight));
            //Debug.Log("Normalized position: " + new Vector2(point.localPosition.x / initialWidth, point.localPosition.y / initialHeight));
        }

        return normalizedPoints;
    }
}
