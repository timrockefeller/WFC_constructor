using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slot
{
    public Vector3Int Position;

    /// <summary>
    /// 可能放置的块
    /// </summary>
    public ModuleSet Modules;

    /// <summary>
    /// 健康值
    /// </summary>
    public short[][] ModuleHealth;

    /// <summary>
    /// 确定后最终模型
    /// </summary>
	public Module Module;

    /// <summary>
    /// 归属地图
    /// </summary>
    private AbstractMap map;

    /// <summary>
    /// 是否坍缩，即已确定
    /// </summary>
    /// <value></value>
    public bool Collapsed
    {
        get
        {
            return this.module != null;
        }
    }

    public Slot(Vector3Int position, AbstractMap map)
    {
        this.Position = position;
        this.map = map;
        this.ModuleHealth = map.CopyInititalModuleHealth();
        this.Modules = new ModuleSet(initializeFull: true);
    }

    public Slot(Vector3Int position, AbstractMap map, Slot prototype)
    {
        this.Position = position;
        this.map = map;
        this.ModuleHealth = prototype.ModuleHealth.Select(a => a.ToArray()).ToArray();
        this.Modules = new ModuleSet(prototype.Modules);
    }

    /// <summary>
    /// 获取指定方向的邻居
    /// 
    /// TODO 缓存？
    /// </summary>
    /// <param name="direction">参阅Orientation类</param>
    public Slot GetNeighbor(int direction)
    {
        return this.map.GetSlot(this.Position + Orientations.Direction[direction]);
    }

}
