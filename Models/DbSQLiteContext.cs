using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OwnersPets.Models
{
    public class DbSQLiteContext
    {
        readonly string databaseOwners;
        readonly string tableSimpsons;
        readonly string tablePets;
        SQLiteConnection connection;
        

        public DbSQLiteContext(string databaseName = "Owners.db", string tableSimpsons = "Simpsons", string tablePets = "Pets")
        {
            this.databaseOwners = databaseName;
            this.tableSimpsons = tableSimpsons;
            this.tablePets = tablePets;
            this.connection = new SQLiteConnection(string.Format("Data Source=|DataDirectory|{0};", databaseName));


            CheckDBandTable();
        }
        private void CheckDBandTable()
        {
            string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "App_Data\\" + this.databaseOwners;

            bool flag = File.Exists(path);
            if (flag == false)
            {
                this.CreateDataBase();
                this.CreateTables();
                //this.InsertEntrys();
            }
        }

        public bool InsertNewOwner(string owner)
        {
            string command = string.Format("Insert into '{0}' ('NameSimpsons') values ('{1}');", this.tableSimpsons, owner);
            return ExecuteNonQuery(new SQLiteCommand(command, this.connection));
        }
        public bool InsertNewPet(long IdOwner, string pet)
        {
            string command = string.Format("INSERT INTO '{0}' (NamePet, IdOwner) VALUES ('{1}' ,'{2}');", this.tablePets, pet, IdOwner);
            return ExecuteNonQuery(new SQLiteCommand(command, this.connection));
        }
        private bool InsertEntrys()
        {
            string command = string.Format("INSERT INTO '{0}' ('NameSimpsons') VALUES ('Вася'),('Коля'),('Иван'),('Толя'),('Анна');INSERT INTO '{1}' ('NamePet', 'IdOwner') VALUES ('Бася',1),('Вася',1),('Дунай',2),('Мурзик',3),('Котик',3),('Машка',3),('Маркиз',4),('Бобик',4);", this.tableSimpsons, this.tablePets);
            return ExecuteNonQuery(new SQLiteCommand(command, this.connection));
        }
        public bool DeleteEntryFromTableSimpsons(long id)
        {
            string command = string.Format("PRAGMA FOREIGN_KEYS=ON; DELETE FROM '{0}' WHERE Id = {1};", this.tableSimpsons, id);
            return ExecuteNonQuery(new SQLiteCommand(command, this.connection));
        }
        public bool DeleteEntryFromTablePets(long id)
        {
            string command = string.Format("DELETE FROM '{0}' WHERE Id = {1};", this.tablePets, id);
            return ExecuteNonQuery(new SQLiteCommand(command, this.connection));
        }
        public bool CreateTables()
        {
            string command = string.Format("CREATE TABLE {0} (Id INTEGER PRIMARY KEY AUTOINCREMENT , NameSimpsons TEXT NOT NULL); CREATE TABLE {1} (Id INTEGER PRIMARY KEY AUTOINCREMENT , NamePet TEXT NOT NULL, IdOwner INTEGER, FOREIGN KEY(IdOwner) REFERENCES Simpsons(Id) ON DELETE CASCADE); ", this.tableSimpsons, this.tablePets);
            return ExecuteNonQuery(new SQLiteCommand(command, this.connection));
        }

        private bool ExecuteNonQuery(SQLiteCommand command)
        {
            connection.Open();
            bool flag = command.ExecuteNonQuery() != 0;
            connection.Close();
            return flag;
        }
        private void CreateDataBase()
        {
            SQLiteConnection.CreateFile(this.databaseOwners);
        }

        public List<SimpsonPets> SelectOwnerPets(long idOwner)
        {

            List<SimpsonPets> sp = new List<SimpsonPets>();
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(string.Format("SELECT u.Id as IdOwner, u.NameSimpsons,d.Id as IdPet, d.NamePet FROM '{0}' d left JOIN '{1}' u ON u.Id = d.IdOwner where d.IdOwner ='{2}';", this.tablePets, this.tableSimpsons, idOwner), connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                SimpsonPets temp = new SimpsonPets();
                temp.IdPet = Convert.ToInt64(record["IdPet"].ToString());
                temp.IdOwner = Convert.ToInt64(record["IdOwner"].ToString());
                temp.NamePet = record["NamePet"].ToString();
                temp.NameSimpsons = record["NameSimpsons"].ToString();
                sp.Add(temp);
            }
            connection.Close();
            return sp;
        }
        public List<SimpsonPets> SelectOwnersAndCountPets()
        {
            List<SimpsonPets> sp = new List<SimpsonPets>();
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(
                string.Format("SELECT t.IdOwner as IdOwner, t.NameSimpsons as NameSimpsons, count(t.IdPet) as Amount from (SELECT u.Id as IdOwner, u.NameSimpsons, p.Id as IdPet FROM '{1}' as u left JOIN '{0}' as p  ON u.Id = p.IdOwner) as t GROUP BY t.IdOwner;", this.tablePets, this.tableSimpsons), connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                SimpsonPets temp = new SimpsonPets();
                temp.IdPet = Convert.ToInt64(record["Amount"].ToString());
                temp.IdOwner = Convert.ToInt64(record["IdOwner"].ToString());
                temp.NameSimpsons = record["NameSimpsons"].ToString();
                sp.Add(temp);
            }
            connection.Close();
            return sp;
        }

    }
}