using PocLinq.Models;
using PocLinq.Service;

namespace PocLinq
{
    public class Menu
    {
        private readonly MusicaService _service;

        public Menu(MusicaService service)
        {
            _service = service;
        }

        public async Task ShowAsync()
        {
            await _service.MusicasJson();

            while (true)
            {
                Console.WriteLine("Menu de Músicas:");
                Console.WriteLine("1. Ver todas as músicas");
                Console.WriteLine("2. Filtrar por gênero");
                Console.WriteLine("3. Músicas longas (+5min)");
                Console.WriteLine("4. Top 5 recentes");
                Console.WriteLine("5. Buscar por título");
                Console.WriteLine("6. Buscar por artista");
                Console.WriteLine("7. Agrupar por gênero");
                Console.WriteLine("8. Verificar se há músicas de um ano");
                Console.WriteLine("9. Filtrar por duração (entre dois valores)");
                Console.WriteLine("0. Sair");

                Console.Write("\nEscolha: ");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ExibirMusicas(_service.BuscarTodasAsMusicas());
                        break;

                    case "2":
                        Console.Write("Digite o gênero: ");
                        var genero = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(genero))
                            ExibirMusicas(_service.BuscarPorGenero(genero));
                        else
                            Console.WriteLine("Você precisa digitar algum gênero.");
                        break;

                    case "3":
                        ExibirMusicas(_service.BuscarPorMusicasMaiores5Minutos());
                        break;

                    case "4":
                        ExibirMusicas(_service.BuscarTop5Recentes());
                        break;

                    case "5":
                        Console.Write("Digite parte do título: ");
                        var nomeParcial = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nomeParcial))
                            ExibirMusicas(_service.BuscarPorNomeDaMusicaParcial(nomeParcial));
                        else
                            Console.WriteLine("Você precisa digitar parte do nome.");
                        break;

                    case "6":
                        Console.Write("Digite o nome do artista: ");
                        var artista = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(artista))
                            ExibirMusicas(_service.BuscarPorArtista(artista));
                        else
                            Console.WriteLine("Você precisa digitar um nome de artista.");
                        break;

                    case "7":
                        var grupos = _service.AgruparPorGenero();
                        foreach (var grupo in grupos)
                        {
                            Console.WriteLine($"\nGênero: {grupo.Key}");
                            foreach (var musica in grupo)
                                Console.WriteLine($" - {musica.Nome}");
                        }
                        break;

                    case "8":
                        Console.Write("Digite o ano: ");
                        if (int.TryParse(Console.ReadLine(), out int ano))
                        {
                            var existe = _service.VerificaSeTemMusicasNesseAno(ano);
                            Console.WriteLine(existe ? "Há músicas desse ano." : "Nenhuma música encontrada nesse ano.");
                        }
                        else
                        {
                            Console.WriteLine("Ano inválido.");
                        }
                        break;

                    case "9":
                        Console.Write("Duração mínima (segundos): ");
                        if (!int.TryParse(Console.ReadLine(), out int min))
                        {
                            Console.WriteLine("Valor inválido.");
                            break;
                        }

                        Console.Write("Duração máxima (segundos): ");
                        if (!int.TryParse(Console.ReadLine(), out int max))
                        {
                            Console.WriteLine("Valor inválido.");
                            break;
                        }

                        ExibirMusicas(_service.BuscarMusicaDuracaoEntreValores(min, max));
                        break;

                    case "0":
                        Console.WriteLine("Obrigado por usar a aplicação! Pressione qualquer tecla para sair...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        private void ExibirMusicas(IEnumerable<Musica> musicas)
        {
            foreach (var musica in musicas)
            {
                Console.WriteLine($"\nNome: {musica.Nome}\nArtista: {musica.Artista}\nAno: {musica.Ano}\nGênero: {musica.Genero}\nDuração: {musica.Duracao}s\n");
            }
        }
    }
}