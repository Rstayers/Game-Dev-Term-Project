using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : CharacterAnimatorManager
{
    // Start is called before the first frame update
    public void UpdateAnimatorFlags(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }
}
