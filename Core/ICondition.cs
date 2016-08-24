namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a condition that one or more VarInfo objects must satisfy. It is used to check the values of inputs/outputs/parameters of a strategy in the model layer context (see <see cref="Preconditions">Preconditions</see> documentation for more info on the pre/post conditions tests)
    /// </summary>
    public interface ICondition
    {

        /// <summary>
        /// Returns true if the condition is applicable on the current VarInfo object(s), false otherwise. Usually the applicability is based on the VarInfoValueType of the VarInfo objects.
        /// </summary>
        /// <param name="nonApplicabilityError">The 'nonApplicabilityError' output argument contains the non applicability errors: the reason for the non applicability. If the condition is applicable, this string is null.</param>
        /// <returns>true if the condition is applicable on the current VarInfo object(s), false otherwise</returns>
        bool IsApplicable(out string nonApplicabilityError);


        /// <summary>
        /// Tests the condition and return an error string. An empty string is returned if the condition is satisfied.
        /// </summary>
        /// <param name="callID">An identifier of the test, to be iserted in the logged error, to trace the context in which the condition test was called (for example, to trace the name of model component that triggered the test)</param>
        /// <returns>Returns a string containing the error information in case the condition is not satisfied, returns an empty string in case the condition is satisfied.</returns>
        string TestCondition(string callID);

        /// <summary>
        /// Returns the list of the VarInfoValueTypes for which the condition is applicable. 
        /// </summary>
        IEnumerable<VarInfoValueTypes> ApplicableVarInfoValueTypes { get; }

        /// <summary>
        /// The name used to identify the condition among the others. It is specified by the implementation of each condition, and cannot be changed by the consumers.
        /// </summary>
        string ConditionName { get; }

        /// <summary>
        /// Returns the list of the VarInfo objects "controlled" by the condition. 'Controlled' means that the VarInfos values are tested/used in the TestCondition method. 
        /// </summary>
        IEnumerable<VarInfo> ControlledVarInfos { get; }
    }
}

