using AutoMapper;
using dotnet_rpg.Contracts;
using dotnet_rpg.Contracts.Dtos.Character;

namespace dotnet_rpg.Services
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> Characters = new List<Character>()
        {
            new Character
            {
                Id = 1
            },
            new Character
            {
                Id = 2,
                Name = "Sam"
            }
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var character = _mapper.Map<Character>(newCharacter);

            character.Id = Characters.Max(c => c.Id) + 1;
            
            Characters.Add(character);

            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = _mapper.Map<List<GetCharacterDto>>(Characters)
            };
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new();
            try 
            {
                Character character = Characters.First(item => item.Id == id);

                Characters.Remove(character);

                response.Data = _mapper.Map<List<GetCharacterDto>>(Characters);    
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = _mapper.Map<List<GetCharacterDto>>(Characters)
            };
        }


        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var character = Characters.FirstOrDefault(a => a.Id == id);

            return new ServiceResponse<GetCharacterDto>
            {
                Data = _mapper.Map<GetCharacterDto>(character)
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            ServiceResponse<GetCharacterDto> response = new();
            try 
            {
                Character character = Characters.FirstOrDefault(item => item.Id == updateCharacterDto.Id);

                ArgumentNullException.ThrowIfNull(character, nameof(character));

                character.Name = updateCharacterDto.Name;
                character.Class = updateCharacterDto.Class;
                character.HitPoints = updateCharacterDto.HitPoints;
                character.Defense = updateCharacterDto.Defense;
                character.Strength = updateCharacterDto.Strength;
                character.Intelligence = updateCharacterDto.Intelligence;
                response.Data = _mapper.Map<GetCharacterDto>(character);    
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}