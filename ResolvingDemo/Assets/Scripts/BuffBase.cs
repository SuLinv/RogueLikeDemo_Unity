using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BuffBase
{
    public int buffID;
    public string buffName;
    public int maxLimit;//最大层数或最大时间
    public float duration;//持续时间
    public float frequency;//触发频率
    public float duraValue;//执行数值
    public float attackRate;//攻击触发几率
    public bool addValue;//增加还是减少数值

    public BuffType buffType;
    public enum BuffType{
        DebugLog,
        AttributeModify,
        AttackEffect,
    }

    public AttributeType attributeType;
    public enum AttributeType{
        HP = 0,
        MP = 1,
    }

    public BuffTimeType buffTimeType;
    public enum BuffTimeType{
        Instant,
        Infinite,
        durable,
    }

}
