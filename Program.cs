using sistema_gestao_faculdade.Entity;

public static class Program
{
    public static void Main()
    {
        List<Professor> professores = new List<Professor>();
        List<Aluno> alunos = new List<Aluno>();
        List<Curso> cursos = new List<Curso>();
        List<Disciplina> disciplinas = new List<Disciplina>();
        // List<Boletim> boletins = new List<Boletim>();
		List<Matricula> matriculas = new List<Matricula>();

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
                    CadastrarProfessor(professores, alunos);
                    break;

                case 3:
                    Console.WriteLine("Cadastrar aluno");
                    CadastrarAluno(alunos, professores);
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
                    MatricularAlunoCurso(alunos, cursos, matriculas);
                    break;

                case 7:
                    Console.WriteLine("Lançar nota");
                    LancarNota(alunos, cursos, disciplinas, matriculas);
                    break;

                case 8:
                    Console.WriteLine("Consultar pessoas");
                    ListarTodasPessoas(alunos, professores);
                    break;

                case 9:
                    Console.WriteLine("Consultar cursos");
                    ConsultarCursos(cursos, matriculas);
                    break;

                case 10:
                    Console.WriteLine("Consultar matrículas");
					ConsultarMatriculas(alunos, cursos, matriculas);
                    break;

                case 11:
                    Console.WriteLine("Consultar boletim");
					ConsultarBoletim(alunos, cursos, matriculas);
                    break;

                case 12:
                    Console.WriteLine("Enviar notificação");
                    EnviarNotificacao(alunos, professores);
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
                Console.ReadLine();
            }

        } while (opcao != 0);
    }

    public static void MatricularAlunoCurso(List<Aluno> alunos, List<Curso> cursos, List<Matricula> matriculas)
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
        var matricula = Console.ReadLine()!.Trim() ?? string.Empty;

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
        var curso = Console.ReadLine()!.Trim() ?? string.Empty;

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

        var boletim = new Boletim(alunoMatricula, cursoCodigo);
        alunoMatricula.boletins.Add(boletim);

        Matricula novaMatricula = new Matricula(alunoMatricula, cursoCodigo, boletim);
        matriculas.Add(novaMatricula);

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

    public static void LancarNota(List<Aluno> alunos, List<Curso> cursos, List<Disciplina> disciplinas, List<Matricula> matriculas)
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

        var matriculaEncontrada = matriculas.FirstOrDefault(m => m.Aluno.NumeroMatricula == alunoEncontrado.NumeroMatricula && m.Curso.Codigo == cursoEncontrado.Codigo);
        if (matriculaEncontrada == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nMatrícula não encontrada para esta disciplina.");

            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        var boletim = matriculaEncontrada.Boletim;
        if (boletim == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nBoletim não encontrado para esta matrícula.");

            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

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

    public static void CadastrarProfessor(List<Professor> professores, List<Aluno> alunos)
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

    public static void CadastrarAluno(List<Aluno> alunos, List<Professor> professores)
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

    public static void ExibirNotificacoesPessoa(Pessoa p)
    {
        if (p.Notificacoes.Any())
        {
            Console.WriteLine("   Notificações:");
            foreach (var n in p.Notificacoes) Console.WriteLine($"     - {n}");
        }
    }

    public static void EnviarNotificacao(List<Aluno> alunos, List<Professor> professores)
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

    public static void ListarTodasPessoas(List<Aluno> alunos, List<Professor> professores)
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
            string identificador = p is Aluno a ? $"Matrícula: {a.NumeroMatricula} | Curso: {a.cursos}" : $"Registro: {((Professor)p).Registro} | Especialidade: {((Professor)p).Especialidade}";

            Console.WriteLine($"[{tipo}] {identificador} | Nome: {p.Nome} | CPF: {p.CPF} | E-mail: {p.Email}");

            ExibirNotificacoesPessoa(p);
            Console.WriteLine(new string('-', 40));
        }
    }

    public static void ConsultarCursos(List<Curso> cursos, List<Matricula> matriculas)
    {
        if (cursos.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNenhum curso cadastrado.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.WriteLine("\n===== CONSULTAR CURSOS =====\n");

        Console.Write("Insira o código do curso que deseja consultar: ");
        string codigoCurso = Console.ReadLine()?.Trim() ?? string.Empty;

        var curso = cursos.FirstOrDefault(c => string.Equals(c.Codigo, codigoCurso, StringComparison.OrdinalIgnoreCase));
        if (curso == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nCurso não encontrado.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Código: {curso.Codigo}");
        Console.WriteLine($"Nome: {curso.Nome}");
        Console.WriteLine($"Tipo: {Curso.FormatarTipo(curso.Tipo)}");
        Console.WriteLine();
        Console.WriteLine("Disciplinas:");

        if (curso.disciplinas.Count == 0)
        {
            Console.WriteLine("Nenhuma disciplina vinculada.");
        }
        else
        {
            foreach (var disciplina in curso.disciplinas)
            {
                Console.WriteLine($"{disciplina.Nome}");
                Console.WriteLine($"Professor: {disciplina.Professor.Nome}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Alunos matriculados:");
        var alunosMatriculados = matriculas
            .Where(m => m.Curso.Codigo == curso.Codigo)
            .Select(m => m.Aluno.Nome)
            .Distinct()
            .ToList();

        if (alunosMatriculados.Count == 0)
        {
            Console.WriteLine("Nenhum aluno matriculado.");
        }
        else
        {
            foreach (var nomeAluno in alunosMatriculados)
            {
                Console.WriteLine($"{nomeAluno}");
            }
        }
    }

    public static void ConsultarBoletim(List<Aluno> alunos, List<Curso> cursos, List<Matricula> matriculas)
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

        if (matriculas.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNenhuma matrícula cadastrada.");

            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.WriteLine("\n===== CONSULTAR BOLETIM =====\n");

        Console.Write("Matricula do aluno: ");
        var matricula = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Código do curso: ");
        var codigoCurso = Console.ReadLine()?.Trim() ?? string.Empty;

        var alunoEncontrado = alunos.FirstOrDefault(x => x.NumeroMatricula == matricula);
        if (alunoEncontrado == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAluno não encontrado.");

            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        var cursoEncontrado = alunoEncontrado.cursos.FirstOrDefault(c => c.Codigo == codigoCurso);
        if (cursoEncontrado == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nCurso informado não encontrado.");

            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        var matriculaEncontrada = matriculas.FirstOrDefault(m => m.Aluno.NumeroMatricula == alunoEncontrado.NumeroMatricula && m.Curso.Codigo == cursoEncontrado.Codigo);
        if (matriculaEncontrada == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nMatrícula não encontrada para o aluno {alunoEncontrado.Nome} no curso {cursoEncontrado.Nome}.");

            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        var boletim = matriculaEncontrada.Boletim;
        Console.WriteLine("========= BOLETIM =========");
		Console.WriteLine();
        Console.WriteLine($"Aluno: {alunoEncontrado.Nome}");
        Console.WriteLine($"Matrícula: {alunoEncontrado.NumeroMatricula}");
		Console.WriteLine();
        Console.WriteLine($"Curso: {cursoEncontrado.Nome}");
        Console.WriteLine($"Tipo: {Curso.FormatarTipo(cursoEncontrado.Tipo)}");

        if (!cursoEncontrado.disciplinas.Any())
        {
            Console.WriteLine("Este curso ainda não possui disciplinas vinculadas.");
            Console.WriteLine("===========================");
            return;
        }

        foreach (var disciplina in cursoEncontrado.disciplinas)
        {
			Console.WriteLine();
            Console.WriteLine(disciplina.Nome);

            if (boletim.Notas.TryGetValue(disciplina.Codigo, out var nota))
            {
                Console.WriteLine($"Nota: {nota:F2}");
                Console.WriteLine($"Situação: {boletim.ObterSituacao(nota)}");
            }
            else
            {
                Console.WriteLine("Nota: Não lançada");
                Console.WriteLine("Situação: Não avaliado");
            }
        }

        Console.WriteLine("===========================");
    }

    public static void ConsultarMatriculas(List<Aluno> alunos, List<Curso> cursos, List<Matricula> matriculas)
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

        if (matriculas.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNenhuma matrícula cadastrada.");

            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.WriteLine("\n===== CONSULTAR MATRÍCULAS =====\n");

        foreach (var matricula in matriculas)
        {
            var aluno = matricula.Aluno;
            var curso = matricula.Curso;

            Console.WriteLine($"Aluno: {aluno.Nome}");
            Console.WriteLine($"Matrícula: {aluno.NumeroMatricula}");
            Console.WriteLine($"Curso: {curso.Nome}");
            Console.WriteLine($"Tipo: {Curso.FormatarTipo(curso.Tipo)}");
            Console.WriteLine("---------------------------");
        }
    }

	
}
