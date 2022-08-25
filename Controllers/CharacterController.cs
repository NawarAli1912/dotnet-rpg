using dotnet_rpg.Contracts;
using dotnet_rpg.Contracts.Dtos.Character;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _charactersService;

        public CharacterController(ICharacterService charactersService)
        {
            _charactersService = charactersService;
        }

        [HttpGet("GetAll")]        
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll() 
        {
            var result = await _charactersService.GetAllCharacters();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id) 
        {
            var result = await _charactersService.GetCharacterById(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter) 
        {
            var result = await _charactersService.AddCharacter(newCharacter);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updateCharacterDto) 
        {
            var result = await _charactersService.UpdateCharacter(updateCharacterDto);
            
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id) 
        {
            var result = await _charactersService.DeleteCharacter(id);
            
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}