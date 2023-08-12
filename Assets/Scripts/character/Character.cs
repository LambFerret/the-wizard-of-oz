using System;
using System.Collections.Generic;
using System.Linq;
using character;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 현재 캐릭터 상태
    public enum CharacterState
    {
        DOROTHY,
        LION,
        SCARECROW,
        WOODCUTTER
    }

    public string currentCrashState = "";

    public Vector2 lookDirection = new Vector2(1.0f, 0);    // 캐릭터 이동 방향

    [Header("Game Objects")]
    public List<GameObject> characterPrefabs;
    public CharacterState currentState = CharacterState.DOROTHY;

    [Header("Player Movement")]
    public float speed;
    public float jumpForce;

    [Header("Info")]
    public Vector2 movePosition;
    public int jumpCount;
    public int possibleJump;

    public Rigidbody2D _rb;
    public Collider2D _col;

    private Character _character;
    private List<IAbility> _abilities;
    public IAbility Ability;
    private List<GameObject> _characterPrefabs;


    private void Awake()
    {
        _abilities = new List<IAbility>();
        _abilities.Add(new DorothyMovement());
        //_abilities.Add(new LionMovement());
        //_abilities.Add(new ScarecrowMovement());
        //_abilities.Add(new WoodcutterMovement());
        _rb = GetComponent<Rigidbody2D>();
        _characterPrefabs = new List<GameObject>();
        foreach (var p in characterPrefabs)
        {
            var a = Instantiate(p, transform);
            _characterPrefabs.Add(a);
            a.SetActive(false);
        }
    }

    private void Start()
    {
        Change();
    }

    private void Update()
    {
        Move();
        Jump();
        OnCrashCheck();
    }

    public void Move()
    {
        float delta = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lookDirection = -lookDirection;

            movePosition = Vector2.right * delta;
            transform.Translate(movePosition);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookDirection = -lookDirection;

            movePosition = Vector2.left * delta;
            transform.Translate(movePosition);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < possibleJump)
        {
            jumpCount++;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(new Vector2(0, jumpForce));
        }
        else if (_rb.velocity.y > 0)
        {
            _rb.velocity *= 0.5f;
        }
    }

    public void Change(CharacterState state)
    {
        for (int i = 0; i < _abilities.Count; i++)
        {
            if (_abilities[i].Name == state.ToString())
            {
                _characterPrefabs[i].SetActive(true);
                Ability = _abilities[i];
                Ability.Action(this);
            }
            else
            {
                _characterPrefabs[i].SetActive(false);
                _abilities[i].Init(this);
            }
        }
    }

    public void Change()
    {
        if (currentState == CharacterState.WOODCUTTER)
        {
            currentState = CharacterState.DOROTHY;
        }
        else
        {
            currentState++;
        }

        Change(currentState);

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Tile"))
        {
            jumpCount = 0;
            Change();
        }
    }

    // 캐릭터 스킬에 따른 특정 오브젝트와의 충돌 상쇄 처리
    void OnCrashCheck()
    {    
        switch(currentState)
        {
            case CharacterState.DOROTHY:
                if (currentCrashState == "Enemy")
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), false);
                else
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Obstacle"), false);
                break;
            case CharacterState.SCARECROW:   // Enemy와 충돌하지 않음
                currentCrashState = "Enemy";
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Obstacle"), false);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), true);
                break;
            case CharacterState.WOODCUTTER:
                currentCrashState = "Obstacle";   // 장애물 및 용암과 충돌하지 않음
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), false);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Obstacle"), true);
                break;
            case CharacterState.LION:
                if(currentCrashState == "Enemy")
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), false);
                else
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Obstacle"), false);
                break;
            default:
                break;
        }
    }
}
    