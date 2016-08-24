namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a collection of <see cref="ICondition">ICondition</see>. This class is used to manage a certain set of conditions alltogheter.
    /// </summary>
    public sealed class ConditionsCollection
    {
        private Dictionary<System.Type, List<ICondition>> _conditions = new Dictionary<System.Type, List<ICondition>>();

        private StringBuilder _VerifyConditions(System.Type type, string callID)
        {
            StringBuilder builder = new StringBuilder("");
            List<ICondition> list = this._conditions[type];
            foreach (ICondition condition in list)
            {
                string str;
                foreach (VarInfo info in condition.ControlledVarInfos)
                {
                    if (info.CurrentValue == null)
                    {
                        throw new Exception("Cannot verify conditions for a null CurrentValue (VarInfo name:" + info.Name + ").");
                    }
                }
                if (!condition.IsApplicable(out str))
                {
                    throw new Exception(str);
                }
                builder.Append(condition.TestCondition(callID));
            }
            return builder;
        }

        /// <summary>
        /// Add a condition to the collection. The method checks if the condition is applicable, through the IsApplicable method. If not, it throws exception.
        /// The method checks if the condition is already present in the collection. If yes, it throws an exeception.
        /// The condition is registered among the other conditions having the same Type.
        /// </summary>
        /// <param name="condition"></param>
        public void AddCondition(ICondition condition)
        {
            string str;
            List<ICondition> list;
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }
            if (!condition.IsApplicable(out str))
            {
                throw new Exception(str);
            }
            System.Type key = condition.GetType();
            if (!this._conditions.TryGetValue(key, out list))
            {
                list = new List<ICondition> {
                    condition
                };
                this._conditions.Add(key, list);
            }
            else
            {
                if (list.Contains(condition))
                {
                    throw new Exception("The conditions' set already contains this condition");
                }
                list.Add(condition);
            }
        }

        /// <summary>
        /// Returns the list of the conditions that are implementations of the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<ICondition> GetConditionsForType(System.Type type)
        {
            return (from a in this._conditions[type] select a);
        }


        /// <summary>
        /// Verifies all the conditions of the collection and returns a string which is the union of the error strings of each single condition.
        /// </summary>
        /// <param name="callID">The callID is a string which is concatenated to the errors of the conditions, to trace the situation in which the test was performed (for example, which is the strategy calling for the test)</param>
        /// <returns></returns>
        public string VerifyConditions(string callID)
        {
            StringBuilder builder = new StringBuilder("");
            foreach (KeyValuePair<System.Type, List<ICondition>> pair in this._conditions)
            {
                builder.Append(this._VerifyConditions(pair.Key, callID));
            }
            return builder.ToString();
        }

        private string VerifyConditions(System.Type type, string callID)
        {
            return this._VerifyConditions(type, callID).ToString();
        }

        /// <summary>
        /// Returns the list of the types of the conditions contained in the conditions collection.
        /// </summary>
        public List<System.Type> ConditionsTypes
        {
            get
            {
                return this._conditions.Keys.ToList<System.Type>();
            }
        }
    }
}

