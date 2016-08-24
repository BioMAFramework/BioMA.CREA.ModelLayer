using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CRA.ModelLayer.Core;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("BioMA.CREA.ModelLayer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CRA and JRC")]
[assembly: AssemblyProduct("Model Layer Libraries")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("134d5c8c-5c96-4a57-9d85-0449094b86b3")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.6212.29140")]
[assembly: AssemblyFileVersion("1.0.0.0")]


public class AssemblyInfo
{
    private Type myType = typeof(Preconditions);

    public string Company
    {
        get
        {
            Type attributeType = typeof(AssemblyCompanyAttribute);
            AssemblyCompanyAttribute attribute = (AssemblyCompanyAttribute) this.myType.Assembly.GetCustomAttributes(attributeType, false)[0];
            return attribute.Company;
        }
    }

    public string Copyright
    {
        get
        {
            Type attributeType = typeof(AssemblyCopyrightAttribute);
            AssemblyCopyrightAttribute attribute = (AssemblyCopyrightAttribute) this.myType.Assembly.GetCustomAttributes(attributeType, false)[0];
            return attribute.Copyright;
        }
    }

    public string Version
    {
        get
        {
            return this.myType.Assembly.GetName().Version.ToString();
        }
    }
}
