using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;

public abstract class AbstractMap
{
    
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public static System.Random Random;

	public const float BLOCK_SIZE = 2f;


	public const int HISTORY_SIZE = 3000;
	// public readonly RingBuffer<HistoryItem> History;
    public readonly QueueDictionary<Vector3Int, ModuleSet> RemovalQueue;
    private HashSet<Slot> workArea;
    public readonly Queue<Slot> BuildQueue;

    private int backtrackBarrier;

    // TODO define
    private int backtrackAmount = 0;

    public readonly short[][] InitialModuleHealth;

    public AbstractMap()
    {
        // this.History = new RingBuffer<HistoryItem>(AbstractMap.HISTORY_SIZE);
        // this.History.OnOverflow = item => item.Slot.Forget();
        this.RemovalQueue = new QueueDictionary<Vector3Int, ModuleSet>(() => new ModuleSet());
        this.BuildQueue = new Queue<Slot>();

        this.InitialModuleHealth = this.createInitialModuleHealth(ModuleData.Current);

        this.backtrackBarrier = 0;
    }

    public abstract Slot GetSlot(Vector3Int position);

    public abstract IEnumerable<Slot> GetAllSlots();

    public abstract void ApplyBoundaryConstraints(IEnumerable<BoundaryConstraint> constraints);



    // public void Undo(int steps)
    // {
    //     while (steps > 0 && this.History.Any())
    //     {
    //         var item = this.History.Pop();

    //         foreach (var slotAddress in item.RemovedModules.Keys)
    //         {
    //             this.GetSlot(slotAddress).AddModules(item.RemovedModules[slotAddress]);
    //         }

    //         item.Slot.Module = null;
    //         this.NotifySlotCollapseUndone(item.Slot);
    //         steps--;
    //     }
    //     if (this.History.Count == 0)
    //     {
    //         this.backtrackBarrier = 0;
    //     }
    // }

    private short[][] createInitialModuleHealth(Module[] modules)
    {
        var initialModuleHealth = new short[6][];
        for (int i = 0; i < 6; i++)
        {
            initialModuleHealth[i] = new short[modules.Length];
            foreach (var module in modules)
            {
                foreach (var possibleNeighbor in module.PossibleNeighbors[(i + 3) % 6])
                {
                    initialModuleHealth[i][possibleNeighbor.Index]++;
                }
            }
        }

        for (int i = 0; i < modules.Length; i++)
        {
            for (int d = 0; d < 6; d++)
            {
                if (initialModuleHealth[d][i] == 0)
                {
                    Debug.LogError("Module " + modules[i].Name + " cannot be reached from direction " + d + " (" + modules[i].GetFace(d).ToString() + ")!", modules[i].Prefab);
                    throw new Exception("Unreachable module.");
                }
            }
        }
        return initialModuleHealth;
    }

    public short[][] CopyInititalModuleHealth()
    {
        return this.InitialModuleHealth.Select(a => a.ToArray()).ToArray();
    }
}
