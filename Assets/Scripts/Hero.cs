using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Hero : Creature
{
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private LayerMask _interactionLayer;
    

    [SerializeField] private AnimatorController _armed;
    [SerializeField] private AnimatorController _disarmed;
    [SerializeField] private AnimatorController _armedRedHat;
    [SerializeField] private AnimatorController _armedGreen;
    [SerializeField] private Cooldown _throwCooldown;
    [SerializeField] private Cooldown _meleeCooldown;
    [SerializeField] private SpawnComponent _throwSpawner;

    [SerializeField] private CheckCircleOverlap _interactionCheck;
    [SerializeField] private SpawnComponent _timer;
    [SerializeField] private ShieldComponent _shield;



    private bool _allowDoubleJump;
    private Rigidbody2D rigid;
    private CircleCollider2D coll;
    private const string SwordId = "Sword";
    private HealthComponent _health;
    private CameraShakeEffect _cameraShake;
    private float _maxHealth = 10;
    [SerializeField] private HeroProgressBar _progress;
    
    [SerializeField] private HeroFlashlight _flashlight;

    public void ToggleFlashlight()
    {
        var isActive = _flashlight.gameObject.activeSelf;
        _flashlight.gameObject.SetActive(!isActive);
    }

    public GameSession _session;

    private int SwordCount => _session.Data.Inventory.Count(SwordId);
    private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;


    private bool CanThrow
    {
        get
        {
            if (SelectedItemId == SwordId) 
                return SwordCount > 1;
            
            var def = DefsFacade.I.Items.Get(SelectedItemId);
            return def.HasTag(ItemTag.Throwable);
        }
    }


    protected override void Awake()
    {
        base.Awake();
        _sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();


    }
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _session.Data.Inventory.OnChanged += OnInventoryChanged;
        _session.StatsModel.OnUpgraded += OnHeroUpgraded;
        _health = GetComponent<HealthComponent>();
        _health.SetHealth(_session.Data.Hp.Value);
        UpdateHeroWeapon();
        gameObject.transform.localScale = Vector3.one;
        _cameraShake = FindObjectOfType<CameraShakeEffect>();
        _progress = FindObjectOfType<HeroProgressBar>();
    }
    private void OnHeroUpgraded(StatId statId)
    {
        switch (statId)
        {
            case StatId.Hp:
                _maxHealth = (int)_session.StatsModel.GetValue(statId);
                Debug.Log("max" + _maxHealth);
                _progress.SetProgress(_session.Data.Hp.Value / _maxHealth);
                Debug.Log(_session.Data.Hp.Value);
                Debug.Log(_maxHealth);
                Debug.Log(_session.Data.Hp.Value / _maxHealth);

                //_session.Data.Hp.Value = health;
                //_health.SetHealth(health);
                break;
        }
    }

    private void OnInventoryChanged(string id, int value)
    {
        if (id == SwordId) UpdateHeroWeapon();
        
    }

    private void OnDestroy()
    {
        _session.Data.Inventory.OnChanged -= OnInventoryChanged;

    }
    protected override void Update()
    {
        base.Update();

        if (rigid.velocity.x > 0 | rigid.velocity.y > 0)
        {
            EndDodge();
        }

        SetSkin();
    }
    protected override float CalculateYVelocity()
    {
        if (_isGrounded) _allowDoubleJump = true;
        

        return base.CalculateYVelocity();
    }
    protected override float CalculateJumpVelocity(float yVelocity)
    {
        if (!_isGrounded && _allowDoubleJump && _session.PerksModel.IsDoubleJumpSupported)
        {
            _session.PerksModel.PerkCooldown.Reset();
            _allowDoubleJump = false;

            DoJumpVfx();

            return _jumpPower;
        }

        return base.CalculateJumpVelocity(yVelocity);
    }

    
    public void AddInInventory(string id, int value)
    {
        _session.Data.Inventory.Add(id, value);

        if (id == "Sword")
        {
            _sounds.Play("Sword");
        }
    }

    public override void TakeDamage()
    {

        base.TakeDamage();
        _cameraShake.Shake();
        _session.Data.Hp.Value = _health.Health;

    }
    public void Interact()
    {
        _interactionCheck.Check();

    }
    public void SpawnFootDust()
    {
        _particles.Spawn("Run");
    }
    public void SpawnLandingDust()
    {
        _particles.Spawn("Landing");
    }
    public void SpawnAttackPartickle()
    {
        _particles.Spawn("Attack");
    }
    public override void Attack()
    {
        if (SwordCount <= 0) return;
        if (_meleeCooldown.IsReady)
        {
            base.Attack();
            _meleeCooldown.Reset();
        }
        
    }
    public void UpdateHeroWeapon()
    {
        if (SwordCount > 0)
        {
            if (_session.SkinModel.Used == "Red Hat")
            {
                _animator.runtimeAnimatorController = _armedRedHat;
            }
            if (_session.SkinModel.Used == "default" | _animator.runtimeAnimatorController == _disarmed)
            {
                _animator.runtimeAnimatorController = _armed;
            }
        }
        else
        {
            _animator.runtimeAnimatorController = _disarmed;
        }
;
    }
    public void NextItem()
    {
        _session.QuickInventory.SetNextItem();
    }
    public void OnHealthChanged(int currentHealth)
    {
        _session.Data.Hp.Value = currentHealth;
    }
    public void OnDoThrow()
    {
        //_particles.Spawn("Throw");
        var throwableId = _session.QuickInventory.SelectedItem.Id;
        var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
        _throwSpawner.SetPrefab(throwableDef.Projectile);

        var instance = _throwSpawner.SpawnInstance();
        ApplyRangeDamageStat(instance);
        _session.Data.Inventory.Remove(throwableId, 1);
    }
    private void ApplyRangeDamageStat(GameObject projectile)
    {
        var hpModify = projectile.GetComponent<ChangeHPComponent>();
        var damageValue = (int)_session.StatsModel.GetValue(StatId.RangeDamage);
        if (projectile.name == "Hero-PearlProjectile(Clone)") { damageValue += 3; }
        damageValue = ModifyDamageByCrit(damageValue);
        hpModify.SetDamage(damageValue);
    }
    public int ModifyDamageByCrit(int damage)
    {
        var critChance = _session.StatsModel.GetValue(StatId.CriticalDamage);
        if (Random.value * 100 <= critChance)
        {
            return damage * 2;
        }
        else
        {
            return damage;
        }
    }
    public void Throw()
    {
        if (_throwCooldown.IsReady & CanThrow)
        {
            _animator.SetTrigger("Throw");
            _throwCooldown.Reset();
            _sounds.Play("Range");
        }
    }
    public void Dodge()
    {
        if (_session.PerksModel.IsDodgeSupported)
        {
            coll.radius = 0.06f;
            coll.offset = new Vector2(0, -0.26f);
            _animator.SetBool("isDodge", true);
            _session.PerksModel.PerkCooldown.Reset();

        }

        Invoke("EndDodge", 1.2f);
    }

    public void EndDodge()
    {
        coll.radius = 0.3f;
        coll.offset = new Vector2(0, -0.03f);
        _animator.SetBool("isDodge", false);

    }

    public void UseInventory()
    {
        if (IsSelectedItem(ItemTag.Throwable))
        {
            Throw();
        }
        else if (IsSelectedItem(ItemTag.Potion))
        {
            UsePotion();
        }
    }
    private bool IsSelectedItem(ItemTag tag)
    {
        return _session.QuickInventory.SelectedDef.HasTag(tag);
    }
    public void UsePotion()
    {
        var potion = DefsFacade.I.Potions.Get(SelectedItemId);

        switch (potion.Effect)
        {
            case Effect.AddHp:
                if (_session.Data.Hp.Value + (int)potion.Value >= _maxHealth)
                {
                    _session.Data.Hp.Value = (int)_maxHealth;
                    _health.SetHealth((int)_maxHealth);
                    Debug.Log("overmax");
                }
                else
                {
                    _session.Data.Hp.Value += (int)potion.Value;
                    _health.SetHealth(_session.Data.Hp.Value);
                    Debug.Log("plus");

                }
                _sounds.Play("Heal");
                break;
                    case Effect.SpeedUp:
                        _speedUpCooldown.Value = _speedUpCooldown.RemainingTime + potion.Time;
                        _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                        _speedUpCooldown.Reset();
                break;
            default:
                break;
        }

        _session.Data.Inventory.Remove(potion.Id, 1);
    }
    private readonly Cooldown _speedUpCooldown = new Cooldown();
    private float _additionalSpeed;

    protected override float CaplculateSpeed()
    {
        if (_speedUpCooldown.IsReady)
            _additionalSpeed = 0f;

        var defaultSpeed = _session.StatsModel.GetValue(StatId.Speed);

        return defaultSpeed + _additionalSpeed;
    }

    public void UsePerk()
    {
        if (_session.PerksModel.IsShieldSupported)
        {
            _shield.Use();
            _session.PerksModel.PerkCooldown.Reset();
        }
    }

    public void SetSkin()
    {
        if (_session.SkinModel.Used == "Red Hat")
        {
            _animator.runtimeAnimatorController = _armedRedHat;
            UpdateHeroWeapon();
        }
        if (_session.SkinModel.Used == "default")
        {
            _animator.runtimeAnimatorController = _armed;
            UpdateHeroWeapon();
        }
        if (_session.SkinModel.Used == "Green Dolphin")
        {
            _animator.runtimeAnimatorController = _armedGreen;
            UpdateHeroWeapon();
        }
    }
}
