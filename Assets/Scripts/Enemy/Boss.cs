using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    [SerializeField] private BulletData BulletData;
    [SerializeField] private EnemyData EnemyData;
    private List<ISkill> skills = new List<ISkill>();
    private float _skillCoolDown = 3f;
    private float _nextSkillTime = 2f;
    private Transform _firePoint;
    private Vector2 _bossPosAppear;
    private float _speed;
    public override void Initialize(float health, float speed)
    {
        base.Initialize(health, speed);
        _nextSkillTime = Time.time + _skillCoolDown;
        skills.Add(new SpreadShotSkill(BulletData));
    }
    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _bossPosAppear, _speed * Time.deltaTime);
    }
    private void Start()
    {
        _speed = EnemyData.speed;
    }
    private void Update()
    {
        if (Time.time >= _nextSkillTime && skills.Count > 0)
        {
            _firePoint = this.transform;
            ExecuteRandomSkill(_firePoint);
            _nextSkillTime = Time.time + _skillCoolDown;
        }
    }
    private void ExecuteRandomSkill(Transform firePoint)
    {
        int randomIndex = Random.Range(0, skills.Count);
        skills[randomIndex].Execute(firePoint);
    }
}
