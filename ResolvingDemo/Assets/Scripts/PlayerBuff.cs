using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerBuff : MonoBehaviour
{
    private Dictionary<int,BuffBase> buffDict = new Dictionary<int, BuffBase>();
    public float playerHP = 100;
    [Header("属性集合:HP-0,MP-1")]
    public List<float> playerAttributes;
    // Start is called before the first frame update
    void Start()
    {
        //初始buff
        foreach(var bf in BuffManager.Instance.buffBase){
            buffDict[bf.buffID] = bf;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<buffDict.Count;i++){
            var bf = buffDict.ElementAt(i);
            switch(bf.Value.buffTimeType){
                case BuffBase.BuffTimeType.durable:
                    StartCoroutine(BuffManager.buffEffectDurable(bf.Value));
                    RemoveBuff(bf.Value);
                    i--;
                    break;
                case BuffBase.BuffTimeType.Infinite:
                    BuffManager.buffEffectInfinite(bf.Value);
                    break;
            }
        }
        
    }

    public void AddBuff(BuffBase buffbase){
        if(buffDict.ContainsKey(buffbase.buffID)){
            return;
        }else{
            buffDict[buffbase.buffID] = buffbase;
        }
    }

    public void AddBuffByID(int bfID){
        AddBuff(BuffManager.BuffDict[bfID]);
    }

    public void RemoveBuff(BuffBase buffbase){
        buffDict.Remove(buffbase.buffID);
    }


}
