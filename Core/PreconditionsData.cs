namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Utility class used sometimes as parameter for pre and post-conditions tests calls. It groups a collection of conditions (implementations of <see cref="ICondition">ICondition</see> interface) into 5 different groups, according to their specific Type: AtLeastOne, CannotBeZeroIf,GreaterThan,RangeBased,RangeOneRangeTwo.
    /// They represents the more common conditions used.
    /// </summary>
    public sealed class PreconditionsData : IPreconditionsData
    {
        private List<VarInfo> _atLeastOne = new List<VarInfo>();
        private Dictionary<VarInfo, VarInfo> _cannotBeZeroIf = new Dictionary<VarInfo, VarInfo>();
        private Dictionary<VarInfo, VarInfo> _greaterThan = new Dictionary<VarInfo, VarInfo>();
        private List<VarInfo> _rangeBased = new List<VarInfo>();
        private Dictionary<VarInfo, VarInfo> _rangeOneRangeTwo = new Dictionary<VarInfo, VarInfo>();

        /// <summary>
        /// AtLeastOne VarInfo list
        /// </summary>
        public List<VarInfo> AtLeastOne
        {
            get
            {
                return this._atLeastOne;
            }
        }

        /// <summary>
        /// CannotBeZeroIf VarInfo list
        /// </summary>
        public Dictionary<VarInfo, VarInfo> CannotBeZeroIf
        {
            get
            {
                return this._cannotBeZeroIf;
            }
        }

        /// <summary>
        /// GreaterThan VarInfo list
        /// </summary>
        public Dictionary<VarInfo, VarInfo> GreaterThan
        {
            get
            {
                return this._greaterThan;
            }
        }

        /// <summary>
        /// RangeBased VarInfo list
        /// </summary>
        public List<VarInfo> RangeBased
        {
            get
            {
                return this._rangeBased;
            }
        }

        /// <summary>
        /// RangeOneRangeTwo VarInfo list
        /// </summary>
        public Dictionary<VarInfo, VarInfo> RangeOneRangeTwo
        {
            get
            {
                return this._rangeOneRangeTwo;
            }
        }
    }
}

