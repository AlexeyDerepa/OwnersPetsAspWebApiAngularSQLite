using OwnersPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwnersPets.Controllers
{
    public class AnimalsController : ApiController
    {
        DbSQLiteContext db = new DbSQLiteContext();
        public IEnumerable<SimpsonPets> Get(int id)
        {
            return db.SelectOwnerPets(id);
        }

        public IEnumerable<SimpsonPets> POST([FromBody] SimpsonPets Owner)
        {
            db.InsertNewPet(Owner.IdOwner, Owner.NamePet);
            return db.SelectOwnerPets(Owner.IdOwner);
        }

        [HttpDelete]
        public IEnumerable<SimpsonPets> DeleteOwner(int IdPet, int IdOwner)
        {
            db.DeleteEntryFromTablePets(IdPet);
            return db.SelectOwnerPets(IdOwner);
        }
    }
}
