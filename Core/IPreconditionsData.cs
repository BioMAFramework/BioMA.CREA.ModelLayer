namespace CRA.ModelLayer.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Utility interface used sometimes as parameter for pre and post-conditions tests calls. It groups a collection of conditions (implementations of <see cref="ICondition">ICondition</see> interface) into 5 different groups, according to their specific Type: AtLeastOne, CannotBeZeroIf,GreaterThan,RangeBased,RangeOneRangeTwo.
    /// They represents the more common conditions used.
    /// A valid implementation of this interface is the <see cref="PreconditionsData">PreconditionsData</see> class.
    ///	</summary>
    internal interface IPreconditionsData
    {
        /// <summary>
        /// Returns all the VarInfo object subject of an AtLeastOne condition
        /// </summary>
        List<VarInfo> AtLeastOne { get; }

        /// <summary>
        /// Returns all the VarInfo object subject of an CannotBeZeroIf condition
        /// </summary>
        Dictionary<VarInfo, VarInfo> CannotBeZeroIf { get; }

        /// <summary>
        /// Returns all the VarInfo object subject of an GreaterThan condition
        /// </summary>
        Dictionary<VarInfo, VarInfo> GreaterThan { get; }

        /// <summary>
        /// Returns all the VarInfo object subject of an RangeBased condition
        /// </summary>
        List<VarInfo> RangeBased { get; }

        /// <summary>
        /// Returns all the VarInfo object subject of an RangeOneRangeTwo condition
        /// </summary>
        Dictionary<VarInfo, VarInfo> RangeOneRangeTwo { get; }
    }
}

