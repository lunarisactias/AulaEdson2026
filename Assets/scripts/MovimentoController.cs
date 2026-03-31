using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MovimentoController : NetworkBehaviour
{
    private CharacterController characterController;
    public float velocidade = 5f;
    public float rotateSpeed = 150f; 
    public Animator animator;

    [Header("Configurações de Pulo e Gravidade")]
    public float forcaPulo = 1.5f; 
    public float gravidade = -9.81f;
    private float velocidadeVertical; 

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            Jump();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        {
            if (characterController.isGrounded && velocidadeVertical < 0)
            {
                velocidadeVertical = -2f; 
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direcao = new Vector3(horizontal, 0, vertical);

            if (direcao.magnitude > 0.1f)
            {
                float velocidadeAtual = velocidade;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    velocidadeAtual = velocidade * 2;
                    animator.SetBool("correndo", true);
                }
                else
                {
                    animator.SetBool("correndo", false);
                }

                characterController.Move(transform.forward * vertical * velocidadeAtual * Runner.DeltaTime);
                
                transform.Rotate(Vector3.up * horizontal * rotateSpeed * Runner.DeltaTime);

                animator.SetBool("podeAndar", true);
            }
            else
            {
                animator.SetBool("podeAndar", false);
                animator.SetBool("correndo", false); 
            }

            
            velocidadeVertical += gravidade * Runner.DeltaTime;

            Vector3 movimentoVertical = new Vector3(0, velocidadeVertical, 0);
            characterController.Move(movimentoVertical * Runner.DeltaTime);
        }
    }

    private void Jump()
    {
        velocidadeVertical = Mathf.Sqrt(forcaPulo * -2f * gravidade);
        animator.SetTrigger("pular");
    }
}