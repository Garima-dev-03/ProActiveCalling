using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commander.Models;


namespace Commander.Data
{
    public class CommanderContext: DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt): base(opt)
        {

        }
        // Creating Representation of Command model in a database
        // In simple terms: Mapping of model classes in database

        public DbSet<Command> Commands { get; set; }
    }





}





