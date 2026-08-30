using System.Globalization;

namespace sistema_gestao_faculdade.Entity
{
    public class Disciplina
    {
        public string Codigo { get; }
        public string Nome { get; }
        public int CargaHoraria { get; }
        public Professor Professor { get; }

        public Disciplina(string codigo, string nome, int cargaHoraria, Professor professor)
        {
            Codigo = codigo;
            Nome = nome;
            CargaHoraria = cargaHoraria;
            Professor = professor;
        }

        public void ExibirInformacoes(Curso curso)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Curso: {curso.Codigo} - {curso.Nome}");
            Console.WriteLine($"Disciplina: {Codigo} - {Nome}");
            Console.WriteLine($"Professor: {Professor.Nome}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        // 4 - Cadastrar Disciplina
        public static void CadastrarDisciplina(List<Professor> professores, List<Disciplina> disciplinas)
        {
            if (professores.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNenhuma professor cadastrado.");

                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.WriteLine("\n===== CADASTRAR DISCIPLINA =====\n");

            Console.Write("Código: ");
            var codigo = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                Console.WriteLine("\nO código da disciplina é obrigatório.");
                return;
            }

            if (disciplinas.Any(x => x.Codigo == codigo))
            {
                Console.WriteLine("\nDisciplina já cadastrada.");
                return;
            }

            Console.Write("Nome: ");
            var nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("\nO nome da disciplina é obrigatório.");
                return;
            }

            // Cada letra de cara palavra maiuscula
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string textoFormatado = textInfo.ToTitleCase(nome.ToLower());

            Console.Write("Carga Hóraria: ");
            if (!int.TryParse(Console.ReadLine(), out int cargaHoraria) || cargaHoraria <= 0)
            {
                Console.WriteLine("\nDigite um valor válido.");
                return;
            }

            if (cargaHoraria == null) 
            {
                Console.WriteLine("\nDigite um valor válido.");
                return;
            }

            Console.Write("Professor Responsável: ");
            var professorResponsavel = Console.ReadLine();

            var professorEncontrado = professores.FirstOrDefault(x => x.Registro == professorResponsavel);
            if (professorEncontrado == null)
            {
                Console.WriteLine("\nProfessor não encontrado.");
                return;
            }

            disciplinas.Add(new Disciplina(codigo, textoFormatado, cargaHoraria, professorEncontrado));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nDisciplina {textoFormatado} cadastrada.");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}