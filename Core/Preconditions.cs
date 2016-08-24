namespace CRA.ModelLayer.Core
{
    using System;

    /// <summary>
    /// Manager class for the pre/post condition tests of the model's variables (VarInfo objects). This class contains the methods to check the preconditions and the postconditions on the input/outputs/parameters of a model,
    /// to identify values which are not valid according to some specified conditions (<see cref="ICondition">ICondition</see> implementations).
    /// </summary>
    /// <remarks>
    /// Example of instruction to execute a precondition test on inputs and parameters of a strategy
    /// 
    ///             //define an empty collection of conditions
    ///             ConditionsCollection cc = new ConditionsCollection();
    ///             
    ///             //define a RangeBasedCondition on an input (a property SoilTemperatureLayer1 belonging to domain class Exogenous)
    ///             //note that the VarInfo definition of this property is in the VarInfo class associated to the domain class. The domain class is 'Exogenous', so the VarInfo class is 'ExogenousVarInfo'
    ///             RangeBasedCondition r1 = new RangeBasedCondition(CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1); 
	///				//if the VarInfoValueType of the property is suitable to the type of condition
    ///             if(r1.ApplicableVarInfoValueTypes.Contains( CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1.ValueType))
    ///                    //add the condition to the collection of conditions
    ///                    {cc.AddCondition(r1);}
    ///
    ///             //define a RangeBasedCondition on a parameter of the strategy (parameter MinimumTemperatureForGrowth )
    ///             //note that in this case we invoke directly the constructor that creates the RangeBasedCondition using a parameter (VarInfo)
    ///             cc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("MinimumTemperatureForGrowth")));
    ///                
    ///             //define a listener to the precondition errors or use one of the existing listeners like TestsOutputDefault or TestsOutputXML defined in this project
    ///             ITestsOutput mylistener = new MyListener();
    ///             Preconditions.StrategyTestsOutput = mylistener;
    ///                
    ///             //Evaluate the precondition test
    ///             Preconditions pre = new Preconditions();
    ///             string callID="Test invoked by component SoilBorne";
    ///             string preConditionsResult = pre.VerifyPreconditions(cc, callID);
    ///                                    
    ///             //trace the precondition test results on the listener
    ///             if (!string.IsNullOrEmpty(preConditionsResult))
    ///                    { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component SoilBorne"); }
    ///					
    /// </remarks>
    public class Preconditions
    {
        /// <summary>
        /// Shows the "about" visual form with an help button to open the help file, 
        /// and another help button to open the code documentation. 
        /// </summary>
        public void Info()
        {
            AboutBox frm = new AboutBox();
            frm.ShowDialog();
        }

        /// <summary>
        /// Method that manages the output of pre/post conditions tests using an implementation of the design pattern strategy. 
        /// </summary>
        /// <param name="testResult">String containing test results.</param>
        /// <param name="saveLog">Used ONLY by the default output strategy (<see cref="TestsOutputDefault">TestsOutputDefault</see>):
        /// saveLog=false: output of pre and post conditions goes on screen (console output)
        /// saveLog=true: output on txt file</param>
        /// <param name="componentName">Component name to be concatenated to the output string to trace the component that triggered the test</param>
        public void TestsOut(string testResult, bool saveLog, string componentName)
        {
            new TestPreconditions().TestsOut(testResult, saveLog, componentName);
        }

        /// <summary>
        /// Tests a collection of post-conditions and returns errors
        /// </summary>
        /// <param name="condCollection">Object containing a collection of conditions to test (see <see cref="ConditionsCollection">ConditionsCollection</see> documentation)</param>
        /// <param name="callID">An identifier of the test, to be inserted in the logged error, to trace the context in which the condition test was called (for example, to trace the name of model component that triggered the test)</param>
        /// <returns>A string containing the post-condition tests errors. Returns an empty string if the post-conditions are all satisfied.</returns>
        public string VerifyPostconditions(ConditionsCollection condCollection, string callID)
        {
            TestPreconditions preconditions = new TestPreconditions();
            return preconditions.VerifyPostconditions(condCollection, callID);
        }

        /// <summary>
        /// Tests a collection of pre-conditions and returns errors
        /// </summary>
        /// <param name="condCollection">Object containing a collection of conditions to test (see <see cref="ConditionsCollection">ConditionsCollection</see> documentation)</param>
        /// <param name="callID">An identifier of the test, to be inserted in the logged error, to trace the context in which the condition test was called (for example, to trace the name of model component that triggered the test)</param>
        /// <returns>A string containing the pre-condition tests errors. Returns an empty string if the pre-conditions are all satisfied.</returns>
        public string VerifyPreconditions(ConditionsCollection condCollection, string callID)
        {
            TestPreconditions preconditions = new TestPreconditions();
            return preconditions.VerifyPreconditions(condCollection, callID);
        }


        ///<summary>
        ///Boolean switch to controlling the pre/post conditions test execution:
        /// If true: precondition/postcondition tests are performed
        /// If false: precondition/postcondition tests are not performed
        ///</summary>
        public static bool RunPrePostConditionTests
        {
            get
            {
                return TestPreconditions.RunPrePostConditionTests;
            }
            set
            {
                TestPreconditions.RunPrePostConditionTests = value;
            }
        }

        /// <summary>
        /// Property to get/set the <see cref="ITestsOutput">ITestsOutput</see> implementation for managing the test output format (tab separated, XML, and trace to client listeners provided).
        /// </summary>
        public static ITestsOutput StrategyTestsOutput
        {
            get
            {
                return TestPreconditions.StrategyTestsOutput;
            }
            set
            {
                TestPreconditions.StrategyTestsOutput = value;
            }
        }
    }
}

