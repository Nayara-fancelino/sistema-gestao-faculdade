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
            break;

        case 2:
            Console.WriteLine("Cadastrar professor");
            break;

        case 3:
            Console.WriteLine("Cadastrar aluno");
            break;

        case 4:
            Console.WriteLine("Cadastrar disciplina");
            break;

        case 5:
            Console.WriteLine("Vincular disciplina a um curso");
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
