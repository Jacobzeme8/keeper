namespace keeper.Models
{
  public class Vault
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string name { get; set; }

        public string description { get; set; }
        public string Img { get; set; }
        public bool ?IsPrivate { get; set; }
        public Account Creator { get; set; }
    }
}