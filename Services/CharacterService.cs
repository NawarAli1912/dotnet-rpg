using AutoMapper;
using dotnet_rpg.Contracts;
using dotnet_rpg.Contracts.Dtos.Character;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var character = _mapper.Map<Character>(newCharacter);
            
            _context.Characters.Add(character);

            await _context.SaveChangesAsync();

            return await GetAllCharacters();
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new();
            try
            {
                Character character = await _context.Characters.FirstAsync(item => item.Id == id);

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                response = await GetAllCharacters();

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
            var response = new ServiceResponse<List<GetCharacterDto>>();

            var dbCharacters = await _context.Characters.ToListAsync();

            response.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);

            return response;
        }


        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try 
            {
                var dbCharacter = await _context.Characters.FirstAsync(item => item.Id == id);
                response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            ServiceResponse<GetCharacterDto> response = new();

            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(item => item.Id == updateCharacterDto.Id);

                ArgumentNullException.ThrowIfNull(character, nameof(character));

                character.Name = updateCharacterDto.Name;
                character.Class = updateCharacterDto.Class;
                character.HitPoints = updateCharacterDto.HitPoints;
                character.Defense = updateCharacterDto.Defense;
                character.Strength = updateCharacterDto.Strength;
                character.Intelligence = updateCharacterDto.Intelligence;

                await _context.SaveChangesAsync();

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