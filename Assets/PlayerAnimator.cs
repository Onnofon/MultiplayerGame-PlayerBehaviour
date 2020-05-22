using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerAnimator : NetworkBehaviour
{
    public Animator animator;

    [Client]
    public void PlayAnimation(string anim)
    {
        CmdPlayAnimation(anim);
    }

    [Command]
    public void CmdPlayAnimation(string anim)
    {
        RpcPlayAnimation(anim);
    }
    [ClientRpc]
    public void RpcPlayAnimation(string anim)
    {
        animator.Play(anim);
    }
}
