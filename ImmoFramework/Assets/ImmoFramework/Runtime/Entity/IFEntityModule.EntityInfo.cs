
using System;
using System.Collections.Generic;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    public sealed partial class IFEntityModule : IFModule
    {
        private sealed class EntityInfo
        {
            public IFEntity Entity { get; private set; }
            public IFEntity Parent { get; private set; }
            public List<IFEntity> Children { get; private set; }



            public EntityInfo()
            {
                Entity = null;
                Parent = null;
                Children = new List<IFEntity>();
            }


            public static EntityInfo Create(IFEntity entity)
            {
                return new EntityInfo
                {
                    Entity = entity
                };
            }
            

            public void Clear()
            {
                Entity = null;
                Parent = null;
                Children.Clear();
            }
        }
    }
}