namespace sistema_gestao_faculdade.Entity
{
    internal class Disciplina
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
            Console.WriteLine($"Curso: {curso.Codigo} - {curso.Nome}");
            Console.WriteLine($"Disciplina: {Codigo} - {Nome}");
            Console.WriteLine($"Professor: {Professor.Nome}");
            Console.WriteLine();
        }
    }
}