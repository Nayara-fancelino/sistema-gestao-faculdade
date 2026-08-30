namespace sistema_gestao_faculdade.Entity
{
    public class Principal
    {
        // 1. AS LISTAS PRECISAM SER ESTÁTICAS PARA QUE OS MÉTODOS ESTÁTICOS AS ENXERGUEM
        public static List<Professor> professores = new List<Professor>();
        public static List<Aluno> alunos = new List<Aluno>();
        
        public static void CadastrarProfessor()
        {
            Console.WriteLine("--- Cadastro de Professor ---");
            Console.Write("CPF: ");
            string cpf = Console.ReadLine()?.Trim() ?? string.Empty;

            // Usa diretamente as listas da classe Principal
            if (professores.Any(p => p.CPF == cpf) || alunos.Any(a => a.CPF == cpf))
            {
                Console.WriteLine("Erro: CPF já cadastrado no sistema!");
                return;
            }

            Console.Write("Registro: ");
            string registro = Console.ReadLine()?.Trim() ?? string.Empty;

            if (professores.Any(p => p.Registro.Equals(registro, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Erro: Registro de professor já existe!");
                return;
            }

            Console.Write("Nome: ");
            string nome = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.Write("E-mail: ");
            string email = Console.ReadLine()?.Trim() ?? string.Empty;

            if (professores.Any(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ||
               alunos.Any(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Erro: Este e-mail já está cadastrado no sistema!");
                return;
            }

            Console.Write("Especialidade: ");
            string especialidade = Console.ReadLine()?.Trim() ?? string.Empty;

            // Adiciona diretamente na lista estática global
            professores.Add(new Professor(nome, cpf, email, registro, especialidade));
            Console.WriteLine("Professor cadastrado com sucesso!");
        }

        public static void CadastrarAluno()
        {
            Console.WriteLine("--- Cadastro de Aluno ---");
            Console.Write("CPF: ");
            string cpf = Console.ReadLine()?.Trim() ?? string.Empty;

            if (alunos.Any(a => a.CPF == cpf) || professores.Any(p => p.CPF == cpf))
            {
                Console.WriteLine("Erro: CPF já cadastrado no sistema!");
                return;
            }

            Console.Write("Número de Matrícula: ");
            string mat = Console.ReadLine()?.Trim() ?? string.Empty;

            if (alunos.Any(a => a.NumeroMatricula.Equals(mat, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Erro: Número de matrícula do aluno já existe!");
                return;
            }

            Console.Write("Nome: ");
            string nome = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.Write("E-mail: ");
            string email = Console.ReadLine()?.Trim() ?? string.Empty;
            
            if (professores.Any(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ||
               alunos.Any(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Erro: Este e-mail já está cadastrado no sistema!");
                return;
            }

            // Adiciona diretamente na lista estática global
            alunos.Add(new Aluno(nome, cpf, email, mat));
            Console.WriteLine("Aluno cadastrado com sucesso!");
        }

       public static void ExibirNotificacoesPessoa(Pessoa p)
        {
            if (p.Notificacoes.Any())
            {
                Console.WriteLine("   Notificações:");
                foreach (var n in p.Notificacoes) Console.WriteLine($"     - {n}");
            }
        }

        public static void EnviarNotificacao()
        {
            Console.WriteLine("--- Enviar Notificação ---");
            List<Pessoa> todasPessoas = new List<Pessoa>();
            todasPessoas.AddRange(alunos);
            todasPessoas.AddRange(professores);

            if (!todasPessoas.Any())
            {
                Console.WriteLine("Nenhum aluno ou professor cadastrado.");
                return;
            }

            Console.WriteLine("Selecione o destinatário:");
            for (int i = 0; i < todasPessoas.Count; i++)
            {
                string tipo = todasPessoas[i] is Aluno ? "Aluno" : "Professor";
                Console.WriteLine($"{i + 1} - [{tipo}] {todasPessoas[i].Nome}");
            }

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > todasPessoas.Count) return;

            Console.Write("Digite a mensagem da notificação: ");
            string mensagem = Console.ReadLine() ?? string.Empty;

            todasPessoas[index - 1].ReceberNotificacao(mensagem);
            Console.WriteLine("Notificação enviada com sucesso!");
        }
        public static void ListarTodasPessoas()
      {
          Console.WriteLine("--- Lista de Todas as Pessoas Cadastradas ---");

            // Cria uma lista unificada contendo alunos e professores
            List<Pessoa> todasPessoas = new List<Pessoa>();
            todasPessoas.AddRange(alunos);
            todasPessoas.AddRange(professores);

            // Verifica se há alguém cadastrado
            if (!todasPessoas.Any())
            {
                Console.WriteLine("Nenhuma pessoa (aluno ou professor) cadastrada no sistema.");
                return;
            }

            // Exibe as pessoas formatadas
            foreach (var p in todasPessoas)
            {
                // Identifica se a pessoa atual é um Aluno ou Professor
                string tipo = p is Aluno ? "Aluno" : "Professor";

                Console.WriteLine($"[{tipo}] Matricula: {((Aluno)p).NumeroMatricula} | Nome: {p.Nome} | CPF: {p.CPF} | E-mail: {p.Email}");

                // Aproveita o método para exibir notificações de cada um, se houver
                ExibirNotificacoesPessoa(p);
                Console.WriteLine(new string('-', 40));
            }
        }

    }
}
