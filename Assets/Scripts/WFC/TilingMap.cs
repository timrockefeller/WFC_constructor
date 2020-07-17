using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 单个有限大小地图
/// TODO 8x8
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
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.y; z++)
                {
                    this.slots[x, y, z] = new Slot(new Vector3Int(x, y, z), this);
                }
            }
        }
    }

    public override Slot GetSlot(Vector3Int position)
    {
        if (position.x < 0 || position.x >= this.Size.x
        || position.y < 0 || position.y >= this.Size.y
        || position.z < 0 || position.z >= this.Size.z)
        {
            return null;
        }
        return this.slots[
            position.x,
            position.y,
            position.z
        ];

    }

    public override IEnumerable<Slot> GetAllSlots()
    {
        for (int x = 0; x < this.Size.x; x++)
        {
            for (int y = 0; y < this.Size.y; y++)
            {
                for (int z = 0; z < this.Size.y; z++)
                {
                    yield return this.slots[x, y, z];
                }
            }
        }
    }


    public override void ApplyBoundaryConstraints(IEnumerable<BoundaryConstraint> constraints)
    {
        foreach (var constraint in constraints)
        {
            int y = constraint.RelativeY;
            if (y < 0)
            {
                y += this.Size.y;
            }
            switch (constraint.Direction)
            {
                case BoundaryConstraint.ConstraintDirection.Up:
                    for (int x = 0; x < this.Size.x; x++)
                    {
                        for (int z = 0; z < this.Size.y; z++)
                        {
                            if (constraint.Mode == BoundaryConstraint.ConstraintMode.EnforceConnector)
                            {
                                this.GetSlot(new Vector3Int(x, this.Size.y - 1, z)).EnforceConnector(Orientations.UP, constraint.Connector);
                            }
                            else
                            {
                                this.GetSlot(new Vector3Int(x, this.Size.y - 1, z)).ExcludeConnector(Orientations.UP, constraint.Connector);
                            }
                        }
                    }
                    break;
                case BoundaryConstraint.ConstraintDirection.Down:
                    for (int x = 0; x < this.Size.x; x++)
                    {
                        for (int z = 0; z < this.Size.y; z++)
                        {
                            if (constraint.Mode == BoundaryConstraint.ConstraintMode.EnforceConnector)
                            {
                                this.GetSlot(new Vector3Int(x, 0, z)).EnforceConnector(Orientations.DOWN, constraint.Connector);
                            }
                            else
                            {
                                this.GetSlot(new Vector3Int(x, 0, z)).ExcludeConnector(Orientations.DOWN, constraint.Connector);
                            }
                        }
                    }
                    break;
                case BoundaryConstraint.ConstraintDirection.Horizontal:


                    // public const int LEFT = 0;
                    // public const int DOWN = 1;
                    // public const int BACK = 2;
                    // public const int RIGHT = 3;
                    // public const int UP = 4;
                    // public const int FORWARD = 5;
                    if (constraint.Mode == BoundaryConstraint.ConstraintMode.EnforceConnector)
                    {
                        for (int x = 1; x < this.Size.x - 1; x++)
                        {
                            Debug.Log(this.Size);
                            this.GetSlot(new Vector3Int(x, y, 0)).EnforceConnector(Orientations.LEFT, constraint.Connector);
                            this.GetSlot(new Vector3Int(x, y, this.Size.y - 1)).EnforceConnector(Orientations.RIGHT, constraint.Connector);
                        }
                        for (int z = 1; z < this.Size.x - 1; z++)
                        {
                            this.GetSlot(new Vector3Int(0, y, z)).EnforceConnector(Orientations.FORWARD, constraint.Connector);
                            this.GetSlot(new Vector3Int(this.Size.x, y, z)).EnforceConnector(Orientations.BACK, constraint.Connector);
                        }
                    }
                    else
                    {

                    }
                    break;
            }
        }
    }


}