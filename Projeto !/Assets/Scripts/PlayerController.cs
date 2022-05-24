using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    private Controle _controle;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;

    private void OnEnable()
    {
        //Inicializacao de variavel
        _controle = new Controle();

        //Referencias dos componentes no mesmo objeto do unity
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        
        // Referencias para a camera main guardada na class Camera
        _mainCamera = Camera.main;

        // Atribuido ao delegate do action triggered no player input
        _playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        //retirando a atribuicao ao delegate
        _playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        //comparando o nome do action que esta chagando com o nome da actions de mover
        if (obj.action.name.CompareTo(_controle.Gameplay.Moviment.name)==0) 
        {
            //atribuir ao moveInput o valor proveniente do input do jogador como um Vector2
            _moveInput = obj.ReadValue<Vector2>();
        }
    }

    private void Move()
    {
        //Calcula o movimento do eixo da camera para o movimento frente/tras
        Vector3 moveVertical = _mainCamera.transform.forward * _moveInput.y;
        
        //calcula o movimento no eixo da camera para o movimento esquerda/direita
        Vector3 moveHorizontal = _mainCamera.transform.right * _moveInput.x;
        
        //adiciona a força no objeto atravez do rigidbody, com intensidade definida por move Speed
        _rigidbody.AddForce((moveVertical + moveHorizontal) * moveSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
