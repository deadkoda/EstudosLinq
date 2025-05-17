using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PocLinq.Models
{
    public class Musica
    {
        [JsonPropertyName("song")]
        public string Nome { get; set; }
        [JsonPropertyName("artist")]
        public string Artista { get; set; }
        [JsonPropertyName("duration_ms")]
        public int Duracao { get; set; }
        [JsonPropertyName("year")]
        public string Ano { get; set; }
        [JsonPropertyName("genre")]
        public string Genero { get; set; }

        public int AnoNumerico => int.TryParse(Ano, out var y) ? y : 0; //feita conversão na classe porque Json envia ano como string.

    }
}
