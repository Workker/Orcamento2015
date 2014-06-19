using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Orcamento.Domain
{
    [Serializable]
    public class ProducaoHospitalar : IAggregateRoot<int>
    {
        [NonSerialized()]
        private int id;


        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }


        public virtual MesEnum Mes { get; set; }

        public virtual double Valor { get; set; }

        public ProducaoHospitalar(MesEnum mes)
        {
            this.Mes = mes;
        }

        protected ProducaoHospitalar() { }
    }
}
