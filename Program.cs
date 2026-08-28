using sistema_gestao_faculdade.Entity;
using System.Globalization;

List<Curso> cursos = new List<Curso>();
List<Disciplina> disciplinas = new List<Disciplina>();

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
            break;

        case 7:
            Console.WriteLine("Lançar nota");
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