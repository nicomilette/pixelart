using System.Collections.Generic;

namespace PixelArtist.Models
{
    public class User
    {
        required public string Username { get; set; }
        public List<Artwork> ArtworkArr { get; set; } = new List<Artwork>();
        public string? Password { get; internal set; }
    }

    public class Artwork
    {
        // Define properties of Artwork class as needed
    }
}
