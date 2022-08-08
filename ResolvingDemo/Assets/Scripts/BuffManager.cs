using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour {

    public PlayerBuff playerAttributes;
    private static BuffManager _instance;
    public static BuffManager Instance
    {
        get { return _instance; }
    }

    void Awake(){
        _instance = this;
    }

    public List<BuffBase> buffBase = new List<BuffBase>();
    private Dictionary<int,BuffBase> buffDict = new Dictionary<int, BuffBase>();
    public static Dictionary<int,BuffBase> BuffDict{
        get {
            return BuffDict;
        }
    }

    void Start(){
        foreach(var bf in buffBase){
            buffDict[bf.buffID] = bf;
        }
    }

    public static IEnumerator buffEffectDurable(BuffBase buffBase){
        float duraTime = buffBase.frequency;
        while(duraTime<=buffBase.duration){
            switch(buffBase.buffType){
                case BuffBase.BuffType.AttributeModify:
                    Instance.buffEffectDurableAttribute(buffBase);
                    break;
            }
            duraTime += buffBase.frequency;
            yield return new WaitForSeconds(buffBase.frequency);
        }
    }

    public static void buffEffectInfinite(BuffBase buffBase){
        switch(buffBase.buffType){
            case BuffBase.BuffType.DebugLog:
                Debug.Log(buffBase.buffName + Time.time);
                break;
        }
    }

    private void buffEffectDurableAttribute(BuffBase buffBase){
        if(buffBase.addValue)   playerAttributes.playerAttributes[(int)buffBase.attributeType] += buffBase.duraValue;
        else    playerAttributes.playerAttributes[(int)buffBase.attributeType] -= buffBase.duraValue;
    }
}
