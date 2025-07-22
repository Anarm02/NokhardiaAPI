using Google.Apis.Storage.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NokhardiaAPI.Context;
using NokhardiaAPI.DTO_s;
using NokhardiaAPI.Entities;
using NokhardiaAPI.Services;

namespace NokhardiaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CharactersController : ControllerBase
	{
		private readonly IGCSService _gcsService;
		private readonly AppDbContext _context;

		public CharactersController(IGCSService gcsService, AppDbContext context)
		{
			_gcsService = gcsService;
			_context = context;
		}
		[HttpPost]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> CreateCharacter([FromForm] CharacterCreateDto dto)
		{
			if (dto.Image == null || dto.Image.Length == 0)
				return BadRequest("Image file is required");

			
			var imageUrl = await _gcsService.UploadFileAsync(dto.Image, "characters");

			var character = new Character
			{
				Name = dto.Name,
				SurName = dto.Surname,
				Title = dto.Title,
				ShortBio = dto.ShortBio,
				ImagePath = imageUrl,
				Clans = dto.Clans ?? new(),
				Race = dto.Race ?? new(),
				RelatedCharacters = dto.RelatedCharacters ?? new(),
				Skills = dto.Skills ?? new(),
				Story = dto.Story ?? new()
			};

		
			_context.Characters.Add(character);
			await _context.SaveChangesAsync();

	
			return Ok(new
			{
				Message = "Character successfully created",
				CharacterId = character.Id,
				ImageUrl = imageUrl
			});
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var characters = await _context.Characters.ToListAsync();
			return Ok(characters);
		}

		// GET: /api/characters/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var character = await _context.Characters.FindAsync(id);
			if (character == null)
				return NotFound(new { Message = $"Character with ID {id} not found." });

			return Ok(character);
		}
	}
}
