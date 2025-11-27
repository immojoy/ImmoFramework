
using System;
using System.Collections.Generic;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    public sealed partial class IFEntityModule : IFModule
    {
        /// <summary>
        /// Entity data used during the showing process
        /// </summary>
        public sealed class EntityDataForShow
        {
            /// <summary>
            ///     <para> Unique serial identifier for the entity </para>
            ///     <para> This is used for the corresponding <b>GameObject</b> name postfix</para>
            /// </summary>
            public int SerialId { get; private set; }


            /// <summary>
            /// The unique identifier for the entity asset
            /// </summary>
            public int EntityId { get; private set; }


            /// <summary>
            /// The logic type associated with the entity, see <see cref="IFEntityLogic"/>
            /// </summary>
            public Type LogicType { get; private set; }


            /// <summary>
            /// The entity group to which the entity that is about to show belongs
            /// </summary>
            public IFEntityGroup EntityGroup { get; private set; }


            /// <summary>
            /// The user-defined data associated with the entity
            /// </summary>
            public object EntityData { get; private set; }


            public EntityDataForShow()
            {
                SerialId = 0;
                EntityId = 0;
                EntityGroup = null;
                EntityData = null;
            }


            /// <summary>
            /// Create a new instance of <b>EntityDataForShow</b>
            /// </summary>
            /// <param name="serialId">Serial identifier for the entity instance</param>
            /// <param name="entityId">Unique identifier for the entity asset</param>
            /// <param name="logicType">Logic type associated with the entity</param>
            /// <param name="entityGroup">The entity group to which the entity that is about to show belongs</param>
            /// <param name="userData">The user-defined data associated with the entity</param>
            /// <returns>A new instance of <b>EntityDataForShow</b></returns>
            public static EntityDataForShow Create(int serialId, int entityId, Type logicType, IFEntityGroup entityGroup, object userData)
            {
                return new EntityDataForShow
                {
                    SerialId = serialId,
                    EntityId = entityId,
                    LogicType = logicType,
                    EntityGroup = entityGroup,
                    EntityData = userData
                };
            }
            

            public void Clear()
            {
                SerialId = 0;
                EntityId = 0;
                EntityGroup = null;
                EntityData = null;
            }
        }
    }
}