using sistema_gestao_faculdade.Entity;
using System.Globalization;

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
            CadastrarCurso(cursos);
            break;

        case 2:
            Console.WriteLine("Cadastrar professor");
            break;

        case 3:
            Console.WriteLine("Cadastrar aluno");
            break;

        case 4:
            Console.WriteLine("Cadastrar disciplina");
            CadastrarDiscplina(disciplinas, professores);
            break;

        case 5:
            Console.WriteLine("Vincular disciplina a um curso");
            VincularDisciplinaCurso(cursos, disciplinas);
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


// 1 - Cadastrar Curso
static void CadastrarCurso(List<Curso> cursos)
{
    Console.WriteLine("\n===== CADASTRAR CURSO =====\n");

    Console.Write("Código: ");
    var codigo = Console.ReadLine().Trim().ToUpper();

    var cursoEncontrado = cursos.FirstOrDefault(x => x.Codigo == codigo);
    if (cursos.Contains(cursoEncontrado))
    {
        Console.WriteLine("\nEste curso já está cadastrado.");
        return;
    }

    Console.Write("Nome do Curso: ");
    var nomeCurso = Console.ReadLine();

    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
    string textoFormatado = textInfo.ToTitleCase(nomeCurso.ToLower());

    Console.WriteLine("\n1 - Graduação\n2 - Pós-Graduação");

    Console.Write("Tipo: ");
    if (!int.TryParse(Console.ReadLine().Trim(), out int tipo))
    {
        Console.WriteLine("Digite um número válido.");
        return;
    }

    TipoCurso tipoCurso = (TipoCurso)tipo;

    Curso curso = new Curso(codigo, textoFormatado, tipoCurso);
    cursos.Add(curso);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nCurso {nomeCurso} cadastrado com sucesso!");

    Console.ForegroundColor = ConsoleColor.White;
}

// 4 - Cadastrar Disciplina
static void CadastrarDiscplina(List<Disciplina> disciplinas, List<Professor> professores)
{
    if (professores.Count == 0)
    {
        Console.WriteLine("\nNenhuma professor cadastrado.");
        return;
    }

    Console.WriteLine("\n===== CADASTRAR DISCIPLINA =====\n");

    Console.Write("Código: ");
    var codigo = Console.ReadLine().Trim().ToUpper();

    if (disciplinas.Any(x => x.Codigo == codigo))
    {
        Console.WriteLine("\nDisciplina já cadastrada.");
        return;
    }

    Console.Write("Nome: ");
    var nome = Console.ReadLine();

    // Cada letra de cara palavra maiuscula
    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
    string textoFormatado = textInfo.ToTitleCase(nome.ToLower());

    Console.Write("Carga Hóraria: ");
    if (!int.TryParse(Console.ReadLine(), out int cargaHoraria))
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
    Console.WriteLine($"\nDisciplina {nome} cadastrada.");

    Console.ForegroundColor = ConsoleColor.White;
}

// 5 - Vincular disciplina a um curso
static void VincularDisciplinaCurso(List<Curso> cursos, List<Disciplina> disciplinas)
{
    if (disciplinas.Count == 0 || cursos.Count == 0)
    {
        Console.WriteLine("\nNenhuma disciplina ou curso cadastrado.");
        return;
    }

    Console.WriteLine("\n===== VINCULAR DISCIPLINA AO CURSO =====\n");

    Console.Write("Código: ");
    string CodigoCurso = Console.ReadLine().Trim().ToUpper();

    var cursoEncontrado = cursos.FirstOrDefault(x => x.Codigo == CodigoCurso);
    if (cursoEncontrado is null)
    {
        Console.WriteLine($"\nCódigo {CodigoCurso} não encontrado.");
        return;
    }

    Console.Write("Disciplina: ");
    string CodigoDisciplina = Console.ReadLine().Trim().ToUpper();

    var disciplinaEncontrada = disciplinas.FirstOrDefault(x => x.Codigo == CodigoDisciplina);
    if (disciplinaEncontrada is null)
    {
        Console.WriteLine($"\nCódigo da disciplina não encontrado.");
        return;
    }

    if (cursoEncontrado.disciplinas.Any(x => x.Codigo == CodigoDisciplina))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
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

// 6 - Matricular aluno em curso
static void MatricularAlunoCurso(List<Aluno> alunos, List<Curso> cursos)
{
    if (alunos.Count == 0 || cursos.Count == 0)
    {
        Console.WriteLine("\nNenhum aluno ou curso cadastrado.");
        return;
    }

    Console.WriteLine("\n===== MATRICULAR ALUNO EM CURSO =====\n");

    Console.Write("Matricula do Aluno: ");

    if (!int.TryParse(Console.ReadLine().Trim(), out int matricula))
    {
        Console.WriteLine("\nDigite um número válido.");
        return;
    }

    var alunoMatricula = alunos.FirstOrDefault(x => x.Matricula == matricula);
    if (alunoMatricula == null)
    {
        Console.WriteLine("\nAluno não encontrado.");
        return;
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nAluno encontrado!");

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"\nAluno: {alunoMatricula.Nome}");
    Console.WriteLine($"Matricula: {alunoMatricula.Matricula}");

    Console.Write("\nCurso: ");
    var curso = Console.ReadLine().ToUpper().Trim();

    var cursoCodigo = cursos.FirstOrDefault(x => x.Codigo == curso);
    if (cursoCodigo == null)
    {
        Console.WriteLine("\nCurso não encontrado.");
        return;
    }

    if (alunoMatricula.cursos.Any(x => x.Codigo == curso))
    {
        Console.WriteLine($"\nO aluno já está cadastrado no curso {cursoCodigo.Nome}.");
        return;
    }

    alunoMatricula.cursos.Add(cursoCodigo);

    foreach (var disciplina in cursoCodigo.disciplinas)
    {
        alunoMatricula.boletins.Add(new Boletim(alunoMatricula, cursoCodigo, disciplina));
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nAluno {alunoMatricula.Nome} cadastrado no curso {cursoCodigo.Nome}!");

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
    Console.WriteLine($"Aluno: {alunoMatricula.Nome}");
    Console.WriteLine($"Matrícula: {alunoMatricula.Matricula}");
    Console.WriteLine($"\nCurso: {cursoCodigo.Nome}");
    Console.WriteLine($"Tipo: {Curso.FormatarTipo(cursoCodigo.Tipo)}");
    Console.WriteLine();
}

// 7 - Lançar nota
static void LancarNota(List<Aluno> alunos, List<Curso> cursos, List<Disciplina> disciplinas)
{
    if (alunos.Count == 0 || cursos.Count == 0)
    {
        Console.WriteLine("\nNenhum aluno ou curso cadastrado.");
        return;
    }

    Console.WriteLine("\n===== LANÇAR NOTA =====\n");

    Console.Write("Matricula do Aluno: ");
    if (!int.TryParse(Console.ReadLine(), out int matricula))
    {
        Console.WriteLine("\nDigite um número válido.");
        return;
    }

    var alunoEncontrado = alunos.FirstOrDefault(x => x.Matricula == matricula);
    if (alunoEncontrado == null)
    {
        Console.WriteLine("\nAluno não encontrado.");
        return;
    }

    Console.Write("Curso: ");
    var curso = Console.ReadLine().ToUpper();

    var cursoEncontrado = cursos.FirstOrDefault(x => x.Codigo == curso);
    if (cursoEncontrado == null)
    {
        Console.WriteLine("\nCurso não encontrado.");
        return;
    }

    if (!alunoEncontrado.cursos.Contains(cursoEncontrado))
    {
        Console.WriteLine($"\nAluno {alunoEncontrado.Nome} não está matriculado no curso {cursoEncontrado.Nome}.");
        return;
    }

    Console.Write("\nDisciplina: ");
    var disciplina = Console.ReadLine().ToUpper();

    var disciplinaEncontrada = disciplinas.FirstOrDefault(x => x.Codigo == disciplina);
    if (disciplinaEncontrada == null)
    {
        Console.WriteLine("\nDisciplina não encontrada.");
        return;
    }

    if (!cursoEncontrado.disciplinas.Contains(disciplinaEncontrada))
    {
        Console.WriteLine($"\nA disciplina {disciplinaEncontrada.Nome} não foi matriculado ao curso {cursoEncontrado.Nome}.");
        return;
    }

    var boletim = alunoEncontrado.boletins.FirstOrDefault(
        x => x.Curso == cursoEncontrado &&
        x.Disciplina == disciplinaEncontrada);

    if (boletim == null)
    {
        Console.WriteLine("\nBoletim não encontrado para está matrícula.");
        return;
    }

    var tipoCurso = cursoEncontrado.Tipo;

    Console.Write("Nota: ");
    if (!double.TryParse(Console.ReadLine(), out double nota))
    {
        Console.WriteLine("\nDigite um valor válido.");
        return;
    }

    if (nota < 0 || nota > 10)
    {
        Console.WriteLine("\nDigite uma nota entra 0 e 10.");
        return;
    }

    boletim.LancarNota(nota, tipoCurso);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nA nota foi atribuida.");

    Console.ForegroundColor = ConsoleColor.White;
}