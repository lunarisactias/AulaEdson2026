using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : NetworkBehaviour
{
    [Networked] public NetworkBool IsCollected { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<NetworkObject>().HasInputAuthority)
        {
            if (IsCollected) return;

            PlayerRef localPlayer = other.GetComponent<NetworkObject>().InputAuthority;

            RPC_Dono(localPlayer);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Dono(PlayerRef collector)
    {
        if (IsCollected) return; 
        IsCollected = true;
        NetworkObject playerObj = Runner.GetPlayerObject(collector);
        if (playerObj != null)
        {
            MovimentoController player = playerObj.GetComponent<MovimentoController>();
            if (player != null)
            {
                player.RPC_AddScore(1); 
            }
        }
        Runner.Despawn(Object);
    }


}
