
using System;
using System.Collections.Generic;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    public sealed partial class IFEntityModule : IFModule
    {
        /// <summary>
        ///     <para>Entity data used during the loading process</para>
        ///     <para>This is passed to the resource manager to collect the instance of the loaded asset</para>
        ///     <para>The instantiated instance and <b>EntityDataForShow</b> are used later to show the entity</para>
        /// </summary>
        private sealed class EntityDataForLoad
        {
            public EntityDataForShow EntityDataForShow { get; private set; }
            public IFEntityInstance EntityInstance { get; private set; }



            public EntityDataForLoad()
            {
                EntityDataForShow = null;
                EntityInstance = null;
            }


            /// <summary>
            /// Create a new instance of EntityDataForLoad
            /// </summary>
            /// <param name="entityDataForShow">Entity data for showing the entity</param>
            /// <param name="entityInstance">Instantiated instance of the entity asset</param>
            /// <returns></returns>
            public static EntityDataForLoad Create(EntityDataForShow entityDataForShow, IFEntityInstance entityInstance)
            {
                return new EntityDataForLoad
                {
                    EntityDataForShow = entityDataForShow,
                    EntityInstance = entityInstance
                };
            }
            

            public void Clear()
            {
                EntityDataForShow = null;
                EntityInstance = null;
            }
        }
    }
}