namespace sistema_gestao_faculdade.Entity
{

    public class Aluno : Pessoa
    {
        public string NumeroMatricula { get; set; }
        public List<Curso> cursos { get; set; }
        public List<Boletim> boletins { get; set; }

        public Aluno(string nome, string cpf, string email, string numeroMatricula)
            : base(nome, cpf, email)
        {
            NumeroMatricula = numeroMatricula;
            cursos = new List<Curso>();
            boletins = new List<Boletim>();
        }
    }
}