using Commander.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>()
            {
                new Command{Id = 1, Name = "Garima", Email = "jai@gmail.com", Department = "C.S"},
                new Command { Id = 2, Name = "jai", Email = "jai@gmail.com", Department = "C.C" },
                new Command { Id = 3, Name = "Geet", Email = "geet@gmail.com", Department = "Bca" },
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id = 0, Name = "Garima", Email = "garima@gmail.com", Department = "C.S"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}
