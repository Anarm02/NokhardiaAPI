namespace NokhardiaAPI.DTO_s
{
	public class CharacterCreateDto
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public IFormFile Image { get; set; }
		public List<string> Clans { get; set; }
		public List<string> Race { get; set; }
		public string Title { get; set; }
		public List<string> RelatedCharacters { get; set; }
		public string ShortBio { get; set; }
		public List<string> Skills { get; set; }
		public List<string> Story { get; set; }
	}
}
