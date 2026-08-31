using System.Globalization;

namespace sistema_gestao_faculdade.Entity
{

    public class Curso
    {

        public string Codigo { get; }
        public string Nome { get; }
        public TipoCurso Tipo { get; }
        public List<Disciplina> disciplinas { get; } = new List<Disciplina>();

        public Curso(string codigo, string nome, TipoCurso tipo)
        {
            Codigo = codigo;
            Nome = nome;
            Tipo = tipo;
        }

        public static string FormatarTipo(TipoCurso tipo)
        {
            return tipo switch
            {
                TipoCurso.Graduacao => "Graduação",
                TipoCurso.PosGraduacao => "Pós-Graduação",
                _ => tipo.ToString()
            };
        }

        // 1 - Cadastrar Curso
        public static void CadastrarCurso(List<Curso> cursos)
        {
            Console.WriteLine("\n===== CADASTRAR CURSO =====\n");

            Console.Write("Código: ");
            var codigo = Console.ReadLine().Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(codigo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nO código do curso é obrigatório.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            var cursoEncontrado = cursos.FirstOrDefault(x => x.Codigo == codigo);
            if (cursos.Contains(cursoEncontrado))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEste curso já está cadastrado.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.Write("Nome do Curso: ");
            var nomeCurso = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nomeCurso))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nO nome do curso é obrigatório.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string textoFormatado = textInfo.ToTitleCase(nomeCurso.ToLower());

            Console.WriteLine("\n1 - Graduação\n2 - Pós-Graduação");

            Console.Write("Tipo: ");
            if (!int.TryParse(Console.ReadLine().Trim(), out int tipo))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Digite um número válido.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            TipoCurso tipoCurso = (TipoCurso)tipo;

            Curso curso = new Curso(codigo, textoFormatado, tipoCurso);
            cursos.Add(curso);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nCurso {textoFormatado} cadastrado com sucesso!");

            Console.ForegroundColor = ConsoleColor.White;
        }

        // 5 - Vincular disciplina a um curso
        public static void VincularDisciplinaCurso(List<Curso> cursos, List<Disciplina> disciplinas)
        {
            if (cursos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNenhum curso cadastrado.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (disciplinas.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNenhuma disciplina cadastrado.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.WriteLine("\n===== VINCULAR DISCIPLINA AO CURSO =====\n");

            Console.Write("Código: ");
            string CodigoCurso = Console.ReadLine().Trim() ?? string.Empty;

            var cursoEncontrado = cursos.FirstOrDefault(x => x.Codigo == CodigoCurso);
            if (cursoEncontrado is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nCódigo {CodigoCurso} não encontrado.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.Write("Disciplina: ");
            string CodigoDisciplina = Console.ReadLine().Trim() ?? string.Empty;

            var disciplinaEncontrada = disciplinas.FirstOrDefault(x => x.Codigo == CodigoDisciplina);
            if (disciplinaEncontrada == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDisciplina não encontrada.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (cursoEncontrado.disciplinas.Any(x => x.Codigo == CodigoDisciplina))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nEssa disciplina já foi adicionada ao curso.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            cursoEncontrado.disciplinas.Add(disciplinaEncontrada);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nA Disciplina {CodigoDisciplina} vinculada ao curso {CodigoCurso}.");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            disciplinaEncontrada.ExibirInformacoes(cursoEncontrado);
        }
    }
}