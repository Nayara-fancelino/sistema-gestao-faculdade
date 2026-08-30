using sistema_gestao_faculdade.Entity;

List<Professor> professores = new List<Professor>()
{
    new Professor("Eduarda Campos", "12345678900", "CamposProf@gmail.com", "2026020801", "Desenvolvedor de Software"),
    new Professor("Guilherme Ralla", "12345678901", "RallaProf@gmail.com", "2026020802", "Desenvolvedor Back-End")
};

List<Aluno> alunos = new List<Aluno>()
{
    new Aluno("Pedro Henrique", "12345678902", "PedroAluno01@gmail.com", "20260200"),
    new Aluno("Leticia Amorin", "12345678903", "AmorinAluno02@gmail.com", "20260201"),
};
List<Curso> cursos = new List<Curso>()
{
    new Curso("ADS", "Análise e Desenvolvimento de Sistemas", TipoCurso.Graduacao),
    new Curso("TI", "Tecnologia da Informação", TipoCurso.Graduacao),
    new Curso("PDFS", "Pós-Graduação em Desenvolvimento Full Stack", TipoCurso.PosGraduacao),
    new Curso("PES", "Pós-Graduação em Engenharia de Software", TipoCurso.PosGraduacao)
};
List<Disciplina> disciplinas = new List<Disciplina>() 
{ 
    new Disciplina("POO", "Programação Orientada a Objetos", 250, professores[1])
};


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
            break;

        case 3:
            Console.WriteLine("Cadastrar aluno");
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
static void MatricularAlunoCurso(List<Aluno> alunos, List<Curso> cursos)
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
    var matricula = Console.ReadLine().Trim().ToUpper();

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
    var curso = Console.ReadLine().Trim().ToUpper();

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
static void LancarNota(List<Aluno> alunos, List<Curso> cursos, List<Disciplina> disciplinas)
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
    var matricula = Console.ReadLine().Trim().ToUpper();

    var alunoEncontrado = alunos.FirstOrDefault(x => x.NumeroMatricula == matricula);
    if (alunoEncontrado == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nAluno não encontrado.");

        Console.ForegroundColor = ConsoleColor.White;
        return;
    }

    Console.Write("Curso: ");
    var curso = Console.ReadLine().ToUpper();

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
    var disciplina = Console.ReadLine().ToUpper();

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