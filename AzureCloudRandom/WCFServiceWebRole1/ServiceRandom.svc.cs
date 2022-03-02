using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceRandom" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceRandom.svc or ServiceRandom.svc.cs at the Solution Explorer and start debugging.
    public class ServiceRandom : IServiceRandom
    {
        public List<int> GetRandomNumbers()
        {
            List<int> numeros = new List<int>();

            Random random = new Random();

            for (int i=0;i<=14;i++) {

                numeros.Add(random.Next(1, 255));
            }

            return numeros;
        }
    }
}
