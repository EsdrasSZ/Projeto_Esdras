using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float initialTime;
    public TMP_Text coinText;
    public int coins = 0;
    public int coletaveis = 0;
    public float moveSpeed;
    public float maxVelocity;

    public float rayDistance;
    public LayerMask groundLayer;

    public float jumpForce;

    private Controle _controle;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput;

    private float _timeRemaining;

    private void Start()
    {
        _timeRemaining = initialTime;
    }

    private bool _isGrounded;

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
        if (obj.action.name.CompareTo(_controle.Gameplay.Moviment.name) == 0)
        {
            //atribuir ao moveInput o valor proveniente do input do jogador como um Vector2
            _moveInput = obj.ReadValue<Vector2>();
        }

        if (obj.action.name.CompareTo(_controle.Gameplay.Jump.name) == 0)
        {
            if (obj.performed) Jump();
        }
    }

    private void Move()
    {
        //Pega o vetor que aponta na direção em que a camera está olhando e zeramos o componente Y
        Vector3 camForward = _mainCamera.transform.forward;
        camForward.y = 0;

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
        LimiteVelocity();
    }

    private void LimiteVelocity()
    {
        //pegar a velocidade
        Vector3 velocity = _rigidbody.velocity;

        //checar se a velocidade está dentro dos limites nos diferentes eixos
        //limitando o eixo x usando ifs, abs e sing
        if (Mathf.Abs(velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x) * maxVelocity;

        // -maxVelcity < velocity.z < mexVelocity 
        velocity.z = Mathf.Clamp(value: velocity.z, -maxVelocity, maxVelocity);

        // alterar a velocidade do player para ficar  dentro dos limites 
        _rigidbody.velocity = velocity;
    }

    /* como fazer o jogador pular...
     * 1 - checar se o jogador está no chão
     *-- a - checra colisão a partir da fisica (usando os eventos de colisão)
     * -- a - vantagens: facil de implementar (adicionar uma função que já existe na Unity - OncollisionEnter)
     * -- a -- desvantagens: não sabemos a hora exta que a unity vai chamar essa função (pode ser que o jogador
     * toque no chão e demore alguns frames pra o jogo saber que ele está no chão)
     * -- b - atrasvés do raycast: o---/ bolinha vai atirar um raio, o raio vai bater en alguns objetos e a gente
     * recebe o resultado dessa colisão
     * 
     * 2-
     * 
     * 
     */
    private void Jump()
    {
        if (_isGrounded) _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    private void CheckGround()
    {
        _isGrounded = Physics.Raycast(origin: transform.position, direction: Vector3.down, rayDistance,
            (int) groundLayer);
    }

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.yellow);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            PlayerObserverManager.playerCoinsChanged(coins);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Coletavel"))
        {
            coletaveis++;
            PlayerObserverManager.playerColetavelChanged(coletaveis);
            
            Destroy(other.gameObject);
        }
    }

    private void CheckVictory()
    {
        if (coins >= 10)
        {
            GameManager.instance.CallVictory();
        }
        
    }

    private void CheckGameOver()
    {
        if (_timeRemaining <= 0)
        {
            GameManager.instance.CallgameOver();
        }
    }
}

    