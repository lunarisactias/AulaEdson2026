using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MovimentoController : NetworkBehaviour
{
    private CharacterController characterController;
    public float velocidade = 5f;
    public float rotateSpeed = 1f;
    public Animator animator;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direcao = new Vector3(horizontal, 0, vertical);
            if (direcao.magnitude > 0.1f)
            {
                #region primeira forma de movimentação
                ////movimento do personagem
                //characterController.Move(direcao * velocidade * Runner.DeltaTime);
                ////rotacao do personagem
                //transform.rotation = Quaternion.LookRotation(direcao);
                #endregion

                #region segunda forma de movimentação
                characterController.Move(transform.forward * vertical * velocidade * Runner.DeltaTime);
                characterController.transform.Rotate(Vector3.up * horizontal * rotateSpeed * Runner.DeltaTime);
                #endregion
                //animacao do personagem
                animator.SetBool("podeAndar", true);
            }
            else
            {
                animator.SetBool("podeAndar", false);
            }


        }
    }
}
