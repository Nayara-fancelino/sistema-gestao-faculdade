using sistema_gestao_faculdade.Entity;

List<Professor> professores = new List<Professor>();
List<Aluno> alunos = new List<Aluno>();
List<Curso> cursos = new List<Curso>();
List<Disciplina> disciplinas = new List<Disciplina>();
List<Boletim> boletins = new List<Boletim>();

int opcao;

do
{
    Console.Clear();

    Console.WriteLine("========= GESTÃO DA FACULDADE =========");
    Console.WriteLine("1 - Cadastrar curso");
    Console.WriteLine("2 - Cadastrar professor");
    Console.WriteLine("3 - Cadastrar aluno");
    Console.WriteLine("4 - Cadastrar disciplina");
    Console.WriteLine("5 - Vincular disciplina a um curso");
    Console.WriteLine("6 - Matricular aluno em curso");
    Console.WriteLine("7 - Lançar nota");
    Console.WriteLine("8 - Consultar pessoas");
    Console.WriteLine("9 - Consultar cursos");
    Console.WriteLine("10 - Consultar matrículas");
    Console.WriteLine("11 - Consultar boletim");
    Console.WriteLine("12 - Enviar notificação");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("=======================================");
    Console.Write("Escolha uma opção: ");

    if (!int.TryParse(Console.ReadLine(), out opcao))
    {
        Console.WriteLine("Opção inválida.");
        opcao = -1;
    }

    switch (opcao)
    {
        case 1:
            Console.WriteLine("Cadastrar curso");
            Curso.CadastrarCurso(cursos);
            break;

        case 2:
            Console.WriteLine("Cadastrar professor");
            CadastrarProfessor();
            break;

        case 3:
            Console.WriteLine("Cadastrar aluno");
            CadastrarAluno();
            break;

        case 4:
            Console.WriteLine("Cadastrar disciplina");
            Disciplina.CadastrarDisciplina(professores, disciplinas);
            break;

        case 5:
            Console.WriteLine("Vincular disciplina a um curso");
            Curso.VincularDisciplinaCurso(cursos, disciplinas);
            break;

        case 6:
            Console.WriteLine("Matricular aluno em curso");
            MatricularAlunoCurso(alunos, cursos);
            break;

        case 7:
            Console.WriteLine("Lançar nota");
            LancarNota(alunos, cursos, disciplinas);
            break;

        case 8:
            Console.WriteLine("Consultar pessoas");
            ListarTodasPessoas();
            break;

        case 9:
            Console.WriteLine("Consultar cursos");
            break;

        case 10:
            Console.WriteLine("Consultar matrículas");
            break;

        case 11:
            Console.WriteLine("Consultar boletim");
            break;

        case 12:
            Console.WriteLine("Enviar notificação");
            EnviarNotificacao();
            break;
        case 13:
            Console.WriteLine("Boletim");
            break;
        case 0:
            Console.WriteLine("Encerrando o sistema...");
            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }

    if (opcao != 0)
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

} while (opcao != 0);


// 6 - Matricular aluno em curso
void MatricularAlunoCurso(List<Aluno> alunos, List<Curso> cursos)
{
    if (alunos.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNenhum aluno cadastrado.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    if (cursos.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNenhum curso cadastrado.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    Console.WriteLine("\n===== MATRICULAR ALUNO EM CURSO =====\n");

    Console.Write("Matricula do Aluno: ");
    var matricula = Console.ReadLine().Trim() ?? string.Empty;

    var alunoMatricula = alunos.FirstOrDefault(x => x.NumeroMatricula == matricula);
    if (alunoMatricula == null)
    {
        Console.WriteLine("\nAluno não encontrado.");
        return;
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nAluno encontrado!");

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"\nAluno: {alunoMatricula.Nome}");
    Console.WriteLine($"Matricula: {alunoMatricula.NumeroMatricula}");

    Console.Write("\nCurso: ");
    var curso = Console.ReadLine().Trim() ?? string.Empty;

    var cursoCodigo = cursos.FirstOrDefault(x => x.Codigo == curso);
    if (cursoCodigo == null)
    {
        Console.WriteLine("\nCurso não encontrado.");
        return;
    }

    if (!cursoCodigo.disciplinas.Any())
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNão é possível matricular o aluno. Este curso ainda não possui disciplinas.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    if (alunoMatricula.cursos.Any(x => x.Codigo == curso))
    {
        Console.WriteLine($"\nO aluno já está cadastrado no curso {cursoCodigo.Nome}.");
        return;
    }

    alunoMatricula.cursos.Add(cursoCodigo);

    alunoMatricula.boletins.Add(new Boletim(alunoMatricula, cursoCodigo));

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nAluno {alunoMatricula.Nome} cadastrado no curso {cursoCodigo.Nome}!");

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
    Console.WriteLine($"Aluno: {alunoMatricula.Nome}");
    Console.WriteLine($"Matrícula: {alunoMatricula.NumeroMatricula}");
    Console.WriteLine($"\nCurso: {cursoCodigo.Nome}");
    Console.WriteLine($"Tipo: {Curso.FormatarTipo(cursoCodigo.Tipo)}");
    Console.WriteLine();
}

// 7 - Lançar nota
void LancarNota(List<Aluno> alunos, List<Curso> cursos, List<Disciplina> disciplinas)
{
    if (alunos.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNenhum aluno cadastrado.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    if (cursos.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNenhum curso cadastrado.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    Console.WriteLine("\n===== LANÇAR NOTA =====\n");

    Console.Write("Matricula do Aluno: ");
    var matricula = Console.ReadLine().Trim() ?? string.Empty;

    var alunoEncontrado = alunos.FirstOrDefault(x => x.NumeroMatricula == matricula);
    if (alunoEncontrado == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nAluno não encontrado.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    Console.Write("Curso: ");
    var curso = Console.ReadLine().Trim() ?? string.Empty;

    var cursoEncontrado = cursos.FirstOrDefault(x => x.Codigo == curso);
    if (cursoEncontrado == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nCurso não encontrado.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    if (!alunoEncontrado.cursos.Contains(cursoEncontrado))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nAluno {alunoEncontrado.Nome} não está matriculado no curso {cursoEncontrado.Nome}.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    if (cursoEncontrado.disciplinas.Count == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nNenhuma disciplina cadastrada.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    Console.Write("\nDisciplina: ");
    var disciplina = Console.ReadLine().Trim() ?? string.Empty;

    var disciplinaEncontrada = disciplinas.FirstOrDefault(x => x.Codigo == disciplina);
    if (disciplinaEncontrada == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nDisciplina não encontrada.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    if (!cursoEncontrado.disciplinas.Contains(disciplinaEncontrada))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nA disciplina {disciplinaEncontrada.Nome} não foi matriculado ao curso {cursoEncontrado.Nome}.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    var boletim = alunoEncontrado.boletins.FirstOrDefault(x => x.Curso.Codigo == cursoEncontrado.Codigo);

    if (boletim == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nBoletim não encontrado para esta matrícula.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    var tipoCurso = cursoEncontrado.Tipo;

    Console.Write("Nota: ");
    if (!double.TryParse(Console.ReadLine(), out double nota))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nDigite um valor válido.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    if (nota < 0 || nota > 10)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nDigite uma nota entre 0 e 10.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    boletim.LancarNota(disciplinaEncontrada, nota);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nA nota foi atribuida.");

    Console.ForegroundColor = ConsoleColor.White;
}

void CadastrarProfessor()
{
    Console.WriteLine("--- Cadastro de Professor ---");
    Console.Write("CPF: ");
    string cpf = Console.ReadLine()?.Trim() ?? string.Empty;

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

    professores.Add(new Professor(nome, cpf, email, registro, especialidade));
    Console.WriteLine("Professor cadastrado com sucesso!");
}

void CadastrarAluno()
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

    alunos.Add(new Aluno(nome, cpf, email, mat));
    Console.WriteLine("Aluno cadastrado com sucesso!");
}

void ExibirNotificacoesPessoa(Pessoa p)
{
    if (p.Notificacoes.Any())
    {
        Console.WriteLine("   Notificações:");
        foreach (var n in p.Notificacoes) Console.WriteLine($"     - {n}");
    }
}

void EnviarNotificacao()
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

void ListarTodasPessoas()
{
    Console.WriteLine("--- Lista de Todas as Pessoas Cadastradas ---");

    List<Pessoa> todasPessoas = new List<Pessoa>();
    todasPessoas.AddRange(alunos);
    todasPessoas.AddRange(professores);

    if (!todasPessoas.Any())
    {
        Console.WriteLine("Nenhuma pessoa (aluno ou professor) cadastrada no sistema.");
        return;
    }

    foreach (var p in todasPessoas)
    {
        string tipo = p is Aluno ? "Aluno" : "Professor";
        string identificador = p is Aluno a ? $"Matrícula: {a.NumeroMatricula}" : $"Registro: {((Professor)p).Registro}";

        Console.WriteLine($"[{tipo}] {identificador} | Nome: {p.Nome} | CPF: {p.CPF} | E-mail: {p.Email}");

        ExibirNotificacoesPessoa(p);
        Console.WriteLine(new string('-', 40));
    }
}
