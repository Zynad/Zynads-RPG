using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage structure
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f };

    //upgrade
    public int weaponLevel = 0;
    private SpriteRenderer SpriteRenderer;

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();     
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            return;


            //Create a new damage object, then we wiill send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            coll.SendMessage("RecieveDamage", dmg);
            
        }
        
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
    public void UpgradeWeapon()
    {
        weaponLevel++;
        SpriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        //Change stats %%
    }
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        SpriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
