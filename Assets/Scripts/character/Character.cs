using System;
using System.Collections.Generic;
using character;
using player;
using UnityEngine;

public class Character : MonoBehaviour, IDataPersistence
{
    // 현재 캐릭터 상태
    public enum CharacterState
    {
        DOROTHY,
        SCARECROW,
        WOODCUTTER,
        LION,
    }

    public Vector2 lookDirection = new Vector2(1.0f, 0); // 캐릭터 이동 방향

    [Header("Game Objects")] public List<GameObject> characterPrefabs;
    public CharacterState currentState = CharacterState.DOROTHY;

    [Header("Player Movement")] public float xMoveLimit = 9.0f;
    public float yMoveLimit = 4.5f;

    public float speed = 2.5f;
    public float jumpForce = 1400.0f;

    [Header("Info")] public Vector2 movePosition;
    public int jumpCount;
    public int possibleJump;

    public float timeToStealth = 5.0f;  // 은신 가능 시간
    public float startToStealth;  // 은신 시작 시간 

    public List<IAbility> Abilities;
    public IAbility CurrentAbility;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    private Character _character;
    private List<GameObject> _characterPrefabs;
    private List<Animator> _animator;
    private Collider2D _enemyCollider;
    private Collider2D _platformCollider;


    private void Awake()
    {
        _animator = new List<Animator>();

        rb = GetComponent<Rigidbody2D>();
        _characterPrefabs = new List<GameObject>();
        foreach (var p in characterPrefabs)
        {
            var a = Instantiate(p, transform);
            _characterPrefabs.Add(a);
            a.SetActive(false);
            _animator.Add(a.transform.GetChild(0).GetComponent<Animator>());
        }
        // _platformCollider = transform.Find("PlatformCollider").GetComponent<Collider2D>();
    }

    private int _friendNumber;

    public void LoadData(PlayerData data)
    {
        int a = 0;
        foreach (var b in data.IsClear)
        {
            if (b) a++;
        }

        _friendNumber = a + 1;
    }

    public void SaveData(PlayerData data)
    {
    }

    private void Start()
    {
        Abilities = new List<IAbility>();
        Abilities.Add(new DorothyMovement());
        if (_friendNumber >= 2)
        {
            Abilities.Add(new ScarecrowMovement());
        }

        if (_friendNumber >= 3)
        {
            Abilities.Add(new WoodcutterMovement());
        }

        if (_friendNumber == 4)
        {
            Abilities.Add(new LionMovement());
        }

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
        Vector2 movePosition = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= xMoveLimit)
        {
            lookDirection = -lookDirection;
            foreach (var a in _animator)
            {
                a.SetBool("IsMoving", true);
            }

            movePosition = Vector2.right * delta;
            transform.Translate(movePosition);
            if (transform.localScale.x < 0)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -xMoveLimit)
        {
            lookDirection = -lookDirection;
            foreach (var a in _animator)
            {
                a.SetBool("IsMoving", true);
            }

            movePosition = Vector2.left * delta;
            transform.Translate(movePosition);
            if (transform.localScale.x > 0)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        foreach (var a in _animator)
        {
            a.SetBool("IsMoving", false);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < possibleJump)
        {
            rb.velocity = Vector2.zero;
            foreach (var a in _animator)
            {
                a.SetBool("IsJumping", true);
            }

            jumpCount++;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Change(CharacterState state)
    {
        EventManager.Instance.ChangeCharacter(state);
        for (int i = 0; i < Abilities.Count; i++)
        {
            if (Abilities[i].Name == state.ToString())
            {
                _characterPrefabs[i].SetActive(true);
                CurrentAbility = Abilities[i];
                CurrentAbility.Action(this);
            }
            else
            {
                _characterPrefabs[i].SetActive(false);
                Abilities[i].Init(this);
            }
        }
    }

    public void Change()
    {
        if (Abilities.Count == 1)
        {
            Change(0);
            return;
        }

        int currentIndex = 0;
        for (int i = 0; i < Abilities.Count; i++)
        {
            if (currentState.ToString() == Abilities[i].Name)
            {
                currentIndex = i;
            }
        }

        if (currentIndex == Abilities.Count - 1)
        {
            currentState = 0;
        }
        else
        {
            currentState++;
        }

        Change(currentState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Tile"))
        {
            foreach (var a in _animator)
            {
                a.SetBool("IsJumping", false);
            }

            jumpCount = 0;
            Change();
        }
    }

    // 허수아비 은신 
    void OnCrashCheck()
    {
        if (currentState == CharacterState.SCARECROW)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Character"), LayerMask.NameToLayer("Enemy"), false);
        }
    }
}