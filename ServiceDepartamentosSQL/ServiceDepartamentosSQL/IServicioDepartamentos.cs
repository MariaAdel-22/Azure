using ServiceDepartamentosSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDepartamentosSQL
{
    [ServiceContract]
    public interface IServicioDepartamentos
    {
        [OperationContract]
        List<Departamento> GetDepartamentos();

        [OperationContract]
        Departamento BuscarDepartamento(int id);
    }
}
