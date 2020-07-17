using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 单个有限大小地图
/// TODO 6x6
/// </summary>
public class TilingMap : AbstractMap
{
    public readonly Vector3Int Size;
    private readonly Slot[,,] slots;
    public TilingMap(Vector3Int size, int seed) : base()
    {
        this.Size = size;
        this.slots = new Slot[size.x, size.y, size.y];
        Random = new System.Random(seed);
    	for (int x = 0; x < size.x; x++) {
			for (int y = 0; y < size.y; y++) {
				for (int z = 0; z < size.y; z++) {
					this.slots[x,y,z] = new Slot(new Vector3Int(x,y,z), this);
				}
			}
		}
    }

    public Slot GetSlot(Vector3Int position)
    {

    }

    public IEnumerable<Slot> GetAllSlots()
    {

    }

    public void ApplyBoundaryConstraints(IEnumerable<BoundaryConstraint> constraints)
    {

    }


}