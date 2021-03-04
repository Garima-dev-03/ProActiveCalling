using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    [Route("api/Commands")]
    [ApiController]

    
    public class CommandsController : ControllerBase
    {

        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        [HttpGet]

        //  public ActionResult<IEnumerable<Command>> GetAllCommands()
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var CommandItems = _repository.GetAllCommands();

            //return Ok(CommandItems);                                           //through Domain model
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(CommandItems));
        }

        [HttpGet("{id}")]
        //  public ActionResult<Command> GetCommandById(int id)
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var CommandItem = _repository.GetCommandById(id);
            if(CommandItem!=null)
            {

                return Ok(_mapper.Map<CommandReadDto>(CommandItem));
            }
            return NotFound();                   //if id does not match with the id in database return notfound
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommandAsync(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://145887b8361c.ngrok.io");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var jsonchange = JsonConvert.SerializeObject(commandReadDto);
            var body = JsonConvert.SerializeObject(commandReadDto, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("api/calling", content);
            
            return commandReadDto;
        }

      
    }
}
