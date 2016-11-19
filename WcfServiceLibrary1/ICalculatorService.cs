using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LogicWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICalculatorService
    {
        //metodos que quiero exponer
        [OperationContract]
        int Add(int value1, int value2);

        [OperationContract]
        int Multiply(int value1, int value2);
        

        // TODO: Add your service operations here
    }
    
}
