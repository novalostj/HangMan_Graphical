namespace HangMan_Graphical
{
    public class WordStorage : Words_Assets
    {
        private Random r = new();

        public string[] GetRandom => Words[r.Next(Words.Count)].GetRandom;
    }
}
