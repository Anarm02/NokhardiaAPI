namespace NokhardiaAPI.Entities
{
	public class Character
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string SurName { get; set; }
		public  string ImagePath { get; set; }
		public List<string> Clans { get; set; }
		public List<string> Skills { get; set; }
		public List<string> Race { get; set; }
		public string Title { get; set; }
		public string ShortBio { get; set; }
		public List<string> RelatedCharacters { get; set; }
		public List<string> Story { get; set; }
	}
}
