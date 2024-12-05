using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LadderInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform bottom;
    [SerializeField] Transform top;
    [SerializeField]
    [Range(0, 1)]
    private float startOffset;
    private float positionOnLadder;
    private bool isActiveClimbing;
    private GameObject player;
    public float threshold = 0.15f;
    public ActionContainer climbAction;
    private bool _interacted;

    public bool interacted
    {
        get => _interacted;
        set => _interacted = value;
    }
    public void Interact(CharacterAnimatorManager anim)
    {
       
        player = anim.gameObject;
        positionOnLadder = GetPositionOnLadder();
        //get needed rotation
        Vector3 directionToLadder = transform.position - player.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToLadder);
        targetRotation.x = 0f;
        targetRotation.z = 0f;

        if (positionOnLadder <= .5)
        {
            player.transform.DOMove(bottom.position + new Vector3(0, startOffset, 0), .2f).OnComplete(
                () =>
                {
                    anim.characterCombatManager.stateManager.isClimbing = true;
                    isActiveClimbing = true;
                });
        }
        else
        {
            player.transform.DOMove(top.position - new Vector3(0, startOffset, 0), .2f).OnComplete(
                () =>
                {
                    anim.characterCombatManager.stateManager.isClimbing = true;
                    isActiveClimbing = true;
                });
        }
        player.transform.DORotate(targetRotation.eulerAngles, .2f);
    }
    private void Update()
    {
        if (!isActiveClimbing) return;
        
        positionOnLadder = GetPositionOnLadder();
        //get on/off logic
        if(positionOnLadder <= threshold)//at bottom
        {
            player.GetComponent<CharacterStateManager>().isClimbing = false;
            isActiveClimbing = false;
        }
        else if (positionOnLadder == 1)//at top
        {
            player.transform.DOMove(player.transform.position +(player.transform.forward.normalized*.25f), .1f)
            .OnComplete(() =>
            {
                player.GetComponent<CharacterStateManager>().isClimbing = false;
                isActiveClimbing = false;
            });
        }
    }
    private float GetPositionOnLadder()
    {
        if (player == null) return -1;
        Vector3 playerPosition = player.transform.position;

        Vector3 ladderDirection = top.position - bottom.position;

        Vector3 projection = Vector3.Project(playerPosition - bottom.position, ladderDirection);

        float ladderLength = ladderDirection.magnitude;

        float distanceFromBottom = projection.magnitude;

        float normalizedPosition = Mathf.Clamp01(distanceFromBottom / ladderLength);

        return normalizedPosition;
    }
}
