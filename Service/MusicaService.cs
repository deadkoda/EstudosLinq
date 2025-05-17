using System.Text.Json;
using PocLinq.Models;

namespace PocLinq.Service
{
    public class MusicaService
    {
        private readonly List<Musica> _musicas = new();

        public async Task MusicasJson()
        {
            if (_musicas.Any()) return; // evita recarregar caso ja esteja preenchido

            var httpClient = new HttpClient();
            var url = "https://guilhermeonrails.github.io/api-csharp-songs/songs.json";
            var response = await httpClient.GetStringAsync(url);

            var songs = JsonSerializer.Deserialize<List<Musica>>(response);

            if (songs != null)
                _musicas.AddRange(songs);
        }

        public IEnumerable<Musica> BuscarTodasAsMusicas() => _musicas;

        public IEnumerable<Musica> BuscarPorGenero(string genero) => _musicas.Where(s => s.Genero.Contains(genero));

        public IEnumerable<Musica> BuscarPorMusicasMaiores5Minutos(int duracaoMinima = 300) => _musicas.Where(s => s.Duracao > duracaoMinima);

        public IEnumerable<Musica> BuscarTop5Recentes(int count = 5) => _musicas.OrderByDescending(s => s.Ano).Take(count);

        public IEnumerable<Musica> BuscarPorNomeDaMusicaParcial(string nome) => _musicas.Where(s => s.Nome.Contains(nome));

        public IEnumerable<Musica> BuscarPorArtista(string artista) => _musicas.Where(s => s.Artista.Contains(artista));

        public IEnumerable<IGrouping<string, Musica>> AgruparPorGenero() => _musicas.GroupBy(s => s.Genero);

        public bool VerificaSeTemMusicasNesseAno(int year) => _musicas.Any(s => s.AnoNumerico == year);

        public bool VerificaSeTemMusicasAposAnos2000(int year) => _musicas.All(s => s.AnoNumerico > year);

        public IEnumerable<Musica> BuscarMusicaDuracaoEntreValores(int qtdMinSegundos, int qtdMaxSegundos) => _musicas.Where(s => s.Duracao >= qtdMinSegundos && s.Duracao <= qtdMaxSegundos);
    }
}
