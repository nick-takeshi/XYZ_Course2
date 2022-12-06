using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Paarams")]
    [SerializeField] private bool _inverScale;
    [SerializeField] public float _speed;
    [SerializeField] protected float _jumpPower;
    [SerializeField] protected float _damgeVelocity;
    [SerializeField] protected int _damage;

    [Header("Checkers")]
    [SerializeField] protected LayerMask _groundLayer;
    [SerializeField] protected CheckCircleOverlap _attackRange;
    [SerializeField] protected SpawnListComponent _particles;
    [SerializeField] protected SpawnComponent _JumpParticles;
    [SerializeField] protected LayerCheck _groundCheck;

    protected Vector2 _direction;
    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected PlaySoundsComponent _sounds;
    protected bool _isGrounded;
    protected bool _isJumping;
    protected SpriteRenderer _sprite;
    public float radius = 1;


    protected static readonly int isGroundKey = Animator.StringToHash("isGrounded");
    protected static readonly int isRunningKey = Animator.StringToHash("isRunning");
    protected static readonly int isVertVelocityKey = Animator.StringToHash("VertVelocity");
    protected static readonly int GetHit = Animator.StringToHash("GetHit");

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _sounds = GetComponent<PlaySoundsComponent>();
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    protected virtual void Update()
    {
        _isGrounded = _groundCheck._isTouchingLayer;
    }
    public void FixedUpdate()
    {
        var xVelocity = _direction.x * CaplculateSpeed();
        var yVelocity = CalculateYVelocity();
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);


        _animator.SetBool("isRunning", _direction.x != 0);
        _animator.SetFloat("VertVelocity", _rigidbody.velocity.y);
        _animator.SetBool(isGroundKey, _isGrounded);


        UpdateSpriteDir(_direction);
    }

    protected virtual float CaplculateSpeed()
    {
        return _speed;
    }

    protected virtual float CalculateYVelocity()
    {
        var yVelocity = _rigidbody.velocity.y;
        var isJumpPressing = _direction.y > 0;

        if (_isGrounded)
        {
            _isJumping = false;
        }
        if (isJumpPressing)
        {
            _isJumping = true;
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
        }
        //else if (_rigidbody.velocity.y > 0 && _isJumping)
        //{
        //    yVelocity *= 0.5f;
        //}

        return yVelocity;
    }
    protected virtual float CalculateJumpVelocity(float yVelocity)
    {
        if (_isGrounded)
        {
            yVelocity += _jumpPower;

            DoJumpVfx();
        }
        return yVelocity;
    }
    public void DoJumpVfx()
    {
        _sounds.Play("Jump");

        _JumpParticles.Spawn();
    }
    public void UpdateSpriteDir(Vector2 direction)
    {
        var multiplier = _inverScale ? -1 : 1;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(multiplier, 1, 1);
            _sprite.flipX = false;
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1 * multiplier, 1, 1);
        }
    }
    public virtual void TakeDamage()
    {
        _animator.SetTrigger(GetHit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damgeVelocity);

    }

    public virtual void Attack()
    {
        _animator.SetTrigger("Attack");
        _attackRange.Check();
        _sounds.Play("Melee");

    }

}
