﻿using ApiEmpleadosOAuth.Data;
using ApiEmpleadosOAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosOAuth.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;

        public RepositoryEmpleados(EmpleadosContext context) {

            this.context = context;
        }

        public List<Empleado> GetEmpleados() {

            return this.context.Empleados.ToList();
        }

        public Empleado FindEmpleado(int id) {

            return this.context.Empleados.FirstOrDefault(x => x.IdEmpleado == id);
        }

        public Empleado ExisteEmpleado(string apellido, int id) {

            var consulta = from datos in this.context.Empleados where datos.Apellido == apellido && datos.IdEmpleado == id select datos;

            if (consulta.Count() == 0)
            {

                return null;
            }
            else {

                return consulta.First();
            }
        }
    }
}