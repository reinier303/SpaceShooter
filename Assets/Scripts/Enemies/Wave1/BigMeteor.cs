using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : Meteor
{

    public override void Die()
    {
        objectPooler.SpawnFromPool("Meteor", transform.position, Quaternion.identity);
        objectPooler.SpawnFromPool("Meteor", transform.position, Quaternion.identity);
        objectPooler.SpawnFromPool("Meteor", transform.position, Quaternion.identity);

        base.Die();
    }
}
