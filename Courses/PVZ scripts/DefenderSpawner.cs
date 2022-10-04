using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    private Defender defender;

    //private void Update()
    //{
    //    Debug.Log(defender);
    //}
    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        if (defender != null)
        {
            var starDisplay = FindObjectOfType<StarDisplay>();
            int defenderCost = defender.GetStarCost();
            if (starDisplay.HaveEnoughStars(defenderCost))
            {
                SpawnDefender(gridPos);
                starDisplay.SpendStars(defenderCost);
            }
        }
        else return;
    }
    
    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        Vector2 gridPos = new Vector2(newX, newY);
        return gridPos;
    }

    private void SpawnDefender(Vector2 roundedPos)
    {
        if (defender != null)
        {
            Defender newDefender = Instantiate(defender, roundedPos, transform.rotation);
        }
        else return;
    }
}
