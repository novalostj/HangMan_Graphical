namespace HangMan_Graphical
{
    public struct Word
    {
        private string name = "No_Name";
        private List<string> words = new();

        public string Name { get => name; init => name = value; }
        public List<string> Words { get => words; init => words = value; }

        public string[] GetRandom { get => new string[2] { Name, Words[new Random().Next(Words.Count)] }; }
    }

    public class Words_Assets
    {
        private Word animal = new()
        {
            Name = "Animals",
            Words =
            {
                "dog",
                "cat",
                "alligator",
                "frog",
                "elephant",
                "monkey",
                "chicken",
                "cow",
                "crow",
                "lion",
                "tiger"
            }
        };
        private Word food = new()
        {
            Name = "Foods",
            Words =
            {
                "adobo",
                "bulalo",
                "pancit",
                "spaghetti",
                "lechon",
                "sisig",
                "karekare",
                "sinigang",
                "laing",
                "pinakbet"
            }
        };
        private Word country = new()
        {
            Name = "country",
            Words =
            {
                "philippines",
                "japan",
                "ukraine",
                "india",
                "taiwan",
                "malaysia",
                "indonesia",
                "vietnam",
                "kazakhstan",
                "cuba",
                "denmark",
                "switzerland",
                "finland",
                "kuwait"
            }
        };
        private List<Word> words = new();

        public Words_Assets()
        {
            words = new()
            {
                animal,
                food,
                country
            };
        }

        public List<Word> Words { get => words; } 
    }
}
