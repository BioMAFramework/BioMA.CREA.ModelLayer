/*! 
    \namespace CRA.ModelLayer.Strategy Strategy definition of the Bioma Model Layer
 * 
 * The CRA.ModelLayer.Strategy namespace contains the classes related to the Strategy concept of the Bioma Model Layer. 
    
*/
namespace CRA.ModelLayer.Strategy
{
    using CRA.ModelLayer.MetadataTypes;
    using System;
    using System.Collections.Generic;

    ///<summary>
    ///Interface implemented by strategy classes. 
    ///From the software point of view, strategies are the building blocks of the model algorithm. They can be selected at runtime to be used, implementing the strategy design pattern: the model offers different way to get some output, and the user can select directly at run time the ones to be used in a specific context.
    ///From a modelling point of view, strategies are the implementation of the model's logics in terms of equations. Moreover strategies implement the test of pre/post-conditions on the input, the outputs and the parameters of the model.
    ///Strategies can be combined together, leading to the creation of a 'composite' strategy. This can happen only if the involved strategies share the same strategy component interface (see below).
    ///Each strategy exposes the list of its switches (see <see cref="Switch">Switch class</see> documentation), inputs, outputs and parameters. 
    /// 
    ///Each variable used by the strategy (input, output or parameter) is represented by a <see cref="CRA.ModelLayer.Core.VarInfo">VarInfo class</see> class. Those variables are managed through the <see cref="CRA.ModelLayer.Strategy.ModellingOptionsManager">ModellingOptionsManager object</see> of the strategy.
    ///The sets of related variables are usually contained in a <see cref="CRA.ModelLayer.Core.IDomainClass">domain class</see> which represents a part of the domain of the simulated process (e.g. State of a soil layer)
    ///All the strategies of the same library (component) share the same domain classes, since the models share the same simulated physical domain.
    ///
    ///The IStrategy interface does not define any method for containing the logic of the model. This is because the logic method must contain in its signature the list of domain classes used, and this list changes according to the specific component. Thus, any component should define a 'strategy component interface':
    ///a strategy component interface is an interface that extends the IStrategy interface and is defined inside every component. All the strategies of the component should implement it. This behaviour guarantees that all the strategies share the same domain classes and so they can 'dialogue' between themselves. 
    ///The strategy component interface must define 3 methods:
    /// an 'Update' method containing the logics of the model, a 'TestPreconditions' method containing the preconditions tests and a 'TestPostconditions' method containing the postconditions tests. 
    ///The three method should declare all the domain classes of the components in their signature.    
    /// 
    ///Strategies can be automatically generated using the SCC (Strategy Class Coder) tool of the Bioma framework. See Bioma website for further info.
    ///Existing strategies can be analyzed using the MCE (Model Component Explorer) tool of the Bioma framework. See Bioma website for further info.
    /// </summary>
    public interface IStrategy : IAnnotatable
    {
        /// <summary>
        /// True if the strategy is a context strategy (the strategy uses specific models according to the context given by the inputs provided),false otherwise.
        /// </summary>
        bool IsContext { get; }

        /// <summary>
        /// Time step definition
        /// </summary>
        IList<int> TimeStep { get; }

        /// <summary>
        /// Model type definition
        /// </summary>
        string ModelType { get; }

        /// <summary>
        /// Model domain definition
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Return the <see cref="CRA.ModelLayer.MetadataTypes.PublisherData">publisher data</see> associated to the strategy
        /// </summary>
        PublisherData PublisherData { get; }

        /// <summary>
        /// Returns the <see cref="CRA.ModelLayer.Strategy.ModellingOptionsManager">ModellingOptionsManager</see> of the strategy 
        /// </summary>
        ModellingOptionsManager ModellingOptionsManager { get; }

        ///<summary>
        /// Returns the types of the domain classes used by the strategy
        ///</summary>
        ///<returns></returns>
        IEnumerable<Type> GetStrategyDomainClassesTypes();
    }
}

