using Cities;
using System;
using System.Collections;
using Units;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts
{
    public class Unit : MonoBehaviour, IDamager, IDamagable
    {
        [SerializeField]
        private Sprite icon;

        private UnitType type;

        private float price;
        private float visionRadius;

        private float damage;
        private float attackSpeed;
        private float attackRange;

        private float health;
        private float healthRegenPerSecond;

        private float speed;

        public OwnerType owner;

        //Testing
        private Color color;

        public float Strenght { get; private set; }

        public event Action<OwnerType> OnOutOfHealth;

        public void GiveDamage(IDamagable damageReciever, OwnerType owner, float amount)
        {
            damageReciever.TakeDamage(owner, this.damage);
        }

        public void TakeDamage(OwnerType owner, float amount)
        {
            this.health -= amount;
            if(this.health <= 0) 
            {
                OnOutOfHealth?.Invoke(owner);
            }

            StartCoroutine(colorChangeRoutine());

            IEnumerator colorChangeRoutine()
            {
                var oldColor = this.color;
                this.SetColor(Color.red);
                yield return new WaitForSeconds(attackSpeed / 2);
                this.SetColor(oldColor);
            }
        }

        private void SetColor(Color newColor)
        {
            this.color = newColor;
        }

        private void SetTarget()
        {
            //set idamagable for attack
            //overlap sphere and if enemy in the sphere give damage to that enemy;
        }

        public void Init(UnitScriptableObject config)
        {
            this.price = config.Price;
            this.visionRadius = config.VisionRadius;

            this.health = config.HealthPoints;
            this.healthRegenPerSecond = config.HealthRegenPerSecond;

            this.damage = config.Damage;
            this.attackSpeed = config.AttackSpeed;
            this.attackRange = config.AttackRange;

            this.speed = config.Speed;

            this.icon = config.Icon;

            this.type = config.Type;
        }
    }
}
