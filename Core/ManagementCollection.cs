namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a collection of <see cref="IManagementBase">IManagementBase</see> objects. This class is used to manage a certain set of managements alltogheter.
    /// The management objects must implement/extend type T. 
    /// T is a valid implementation of <see cref="IManagementBase">IManagementBase</see> interface. 
    /// So, T represents a sub-type of IManagementBase, specific for the current configuration of the managements. For example, in case of the "AGROmanagement", T should be type interface "CRA.AgroManagement.IManagement" defined in library "CRA.AgroManagemet2014.dll".
    /// </summary>
    public class ManagementCollection<T> where T: IManagementBase
    {
        private List<T> management;
        private string productionActivity;

        /// <summary>
        /// Builds an empty management collection
        /// </summary>
        public ManagementCollection()
        {
            this.management = new List<T>();
        }

        /// <summary>
        /// This list contains the objects that represent the management published
        /// </summary>
        public virtual List<T> Management
        {
            get
            {
                return this.management;
            }
            set
            {
                this.management = value;
            }
        }


        /// <summary>
        /// Production activity
        /// </summary>
        public string ProductionActivity
        {
            get
            {
                return this.productionActivity;
            }
            set
            {
                this.productionActivity = value;
            }
        }
    }
}

