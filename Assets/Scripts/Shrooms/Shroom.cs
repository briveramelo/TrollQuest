using UnityEngine;
using System.Collections;

public struct StatUpgrade {
    public Stat StatToUpgrade;
    public int upgradeAmount;
    public StatUpgrade(Stat statToUpgrade, int upgradeAmount) {
        StatToUpgrade = statToUpgrade;
        this.upgradeAmount = upgradeAmount;
    }
}

public class Shroom : MonoBehaviour{

    [SerializeField] Stat myStatToUpgrade;
    [SerializeField] int myUpgradeAmount;
    StatUpgrade myStatUpgrade;

    public StatUpgrade GetCollected(){
        myStatUpgrade = new StatUpgrade(myStatToUpgrade, myUpgradeAmount);
        StartCoroutine(EndSelf());
        return myStatUpgrade;
    }

    IEnumerator EndSelf(){
        yield return null;
        Destroy(gameObject);
    }
}


