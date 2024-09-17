using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpellUIManager : MonoBehaviour
{
    private Vector2 input;
    private float currentAngle;
    private int selection;
    private int previousSelection;
    public SpellMenuItem[] spells;
    private SpellMenuItem currentSpell;
    private SpellMenuItem previousSpell;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (input == Vector2.zero)
        {
            selection = -1;
            if (currentSpell != null)
            {
                currentSpell.Deselect();
                currentSpell = null;
                previousSpell = null;
            }
            return;
        }
        currentAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
        
        currentAngle = (currentAngle + 360) % 360;
        selection = (int)(currentAngle / 90);
        
        if(selection != previousSelection)
        {
            previousSpell = spells[previousSelection];
            previousSpell.Deselect();
            previousSelection = selection;
            currentSpell = spells[selection];
            currentSpell.Select();
        }
    }
    public void HandleSelectInput(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>().normalized;
    }
}
