using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character {
    
    public void InvokeMovement(Vector2 input) {
        // The evaluation order will dictate bias for the cardinal directions
        if (!Mathf.Approximately(input.x, 0f)) {
            if (input.x > 0) {
                print("East");
                // this.MoveEast();
            } else {
                print("West");
                // this.MoveWest();
            }
            return;
        }

        if (Mathf.Approximately(input.y, 0f)) return;
        
        if (input.y > 0) {
            print("North");
            // this.MoveNorth();
        } else {
            print("South");
            // this.MoveSouth();
        }
    }
}
