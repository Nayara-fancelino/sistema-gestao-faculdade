using System;

static void CadastrarProfessor()
{
    Console.WriteLine("--- Cadastro de Professor ---");
    Console.Write("CPF: ");
    string cpf = Console.ReadLine()?.Trim();

    if (professores.Any(p => p.CPF == cpf) || alunos.Any(a => a.CPF == cpf))
    {
        Console.WriteLine("Erro: CPF já cadastrado no sistema!");
        return;
    }

    Console.Write("Registro: ");
    string registro = Console.ReadLine()?.Trim();

    if (professores.Any(p => p.Registro.Equals(registro, StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("Erro: Registro de professor já existe!");
        return;
    }

    Console.Write("Nome: ");
    string nome = Console.ReadLine()?.Trim();
    Console.Write("E-mail: ");
    string email = Console.ReadLine()?.Trim();

    if (professores.Any(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) ||
       alunos.Any(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("Erro: Este e-mail já está cadastrado no sistema!");
        return;
    }

    Console.Write("Especialidade: ");
    string especialidade = Console.ReadLine()?.Trim();

    professores.Add(new Professor(nome, cpf, email, registro, especialidade));
    Console.WriteLine("Professor cadastrado com sucesso!");
}

static void CadastrarAluno()
{
    Console.WriteLine("--- Cadastro de Aluno ---");
    Console.Write("CPF: ");
    string cpf = Console.ReadLine()?.Trim();

    if (alunos.Any(a => a.CPF == cpf) || professores.Any(p => p.CPF == cpf))
    {
        Console.WriteLine("Erro: CPF já cadastrado no sistema!");
        return;
    }

    Console.Write("Número de Matrícula: ");
    string mat = Console.ReadLine()?.Trim();

    if (alunos.Any(a => a.NumeroMatricula.Equals(mat, StringComparison.OrdinalIgnoreCase)))
    {
        Console.WriteLine("Erro: Número de matrícula do aluno já existe!");
        return;
    }

    Console.Write("Nome: ");
    string nome = Console.ReadLine()?.Trim();
    Console.Write("E-mail: ");
    string email = Console.ReadLine()?.Trim();

    alunos.Add(new Aluno(nome, cpf, email, mat));
    Console.WriteLine("Aluno cadastrado com sucesso!");
}
static void ExibirNotificacoesPessoa(Pessoa p)
{
    if (p.Notificacoes.Any())
    {
        Console.WriteLine("   Notificações:");
        foreach (var n in p.Notificacoes) Console.WriteLine($"     - {n}");
    }
}

static void EnviarNotificacao()
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
    string mensagem = Console.ReadLine();

    todasPessoas[index - 1].ReceberNotificacao(mensagem);
    Console.WriteLine("Notificação enviada com sucesso!");
}