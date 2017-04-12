using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersPets.Models
{
    public class SimpsonPets
    {
        public long IdPet { get; set; }
        public string NamePet { get; set; }
        public long IdOwner { get; set; }
        public string NameSimpsons { get; set; }
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t\t{2}\t{3}", this.IdOwner, this.NameSimpsons, this.IdPet, this.NamePet);
        }

    }
}