using OwnersPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OwnersPets.Controllers
{
    public class SimpsonsController : ApiController
    {
        DbSQLiteContext db = new DbSQLiteContext();

        public IEnumerable<SimpsonPets> Get()
        {
            return db.SelectOwnersAndCountPets();
        }
        public IEnumerable<SimpsonPets> POST([FromBody] SimpsonPets Owner)
        {
            db.InsertNewOwner(Owner.NameSimpsons);
            return db.SelectOwnersAndCountPets();
        }

        public IEnumerable<SimpsonPets> DeleteOwner(int id)
        {
            db.DeleteEntryFromTableSimpsons(id);
            return db.SelectOwnersAndCountPets();
        }
    }
}
