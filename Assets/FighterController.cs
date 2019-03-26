using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public List<GameObject> bases = new List<GameObject>();
    public int tiberium = 7;
    public GameObject spawnpoint;
    bool canShoot;
    public Vector3 target;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        int rnd = Random.Range(0, 3);
        if (bases[rnd].transform.position == this.transform.position)
        {
            if (rnd < 3)
            { rnd++; }
            else if(rnd == 3)
            { rnd--; }
        }
        this.GetComponent<Seek>().targetGameObject = bases[rnd];
        target = bases[rnd].transform.position;
    }
    void Update()
    {
        if (Vector3.Distance(this.transform.position, target) < 10 && tiberium > 0)
        {
            this.GetComponent<Boid>().maxSpeed = 1;
            if (canShoot == false)
            {
                StartCoroutine("Shoot");
                tiberium--;
                canShoot = true;
            }
        }
        if (tiberium == 0)
        {
            this.GetComponent<Seek>().targetGameObject = spawnpoint;
            this.GetComponent<Boid>().maxSpeed = 5;
        }
    }
    IEnumerator Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.transform.SetParent(this.transform);
        bullet.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
        yield return new WaitForSeconds(0.2f);
        canShoot = false;
    }
}
public class SeekState : State {
    public override void Enter()
    {
        owner.GetComponent<Seek>().enabled = true;
    }
    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, owner.GetComponent<FighterController>().target) <= 10)
        {
            owner.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }
}
public class AttackState : State
{
    public override void Enter()
    {
        owner.GetComponent<Arrive>().enabled = true;
    }
}