using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormPracticaExamenTable.Models
{
    public class Alumno : TableEntity
    {
        private string _idAlumno;
        public string idAlumno
        {
            get { return this._idAlumno; }
            set
            {
                this._idAlumno = value;
                this.RowKey = value;
            }
        }

        private string _Curso;
        public string curso
        {
            get { return this._Curso; }
            set
            {
                this._Curso = value;
                this.PartitionKey = value;
            }
        }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Nota { get; set; }
    }
}
