using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Models
{
    class FavoriteGroupe : List<Favorite>
    {
        public string title { get; set; }
        public FavoriteGroupe(string title)
        {
            this.title = title;
        }
    }
}
