namespace LaunchDeck.Model
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Text { get; set; } = "";
        public DateTime Created { get; set; }
        public Note()
        {

        }
        public Note(string text, DateTime creationDate)
        {
            
            Text = text;
            Created = creationDate;
        }
    }
}
