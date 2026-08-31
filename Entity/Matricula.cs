using sistema_gestao_faculdade.Entity;

public class Matricula
{
    public int Id { get; set; }
    public Aluno Aluno { get; set; }
    public Curso Curso { get; set; }
    public Boletim Boletim { get; set; }

    public Matricula(Aluno aluno, Curso curso)
        : this(aluno, curso, new Boletim(aluno, curso))
    {
    }

    public Matricula(Aluno aluno, Curso curso, Boletim boletim)
    {
        Random random = new();
        Id = random.Next(1, 1000); // Gera um ID aleatório pro banco

        Aluno = aluno;
        Curso = curso;
        Boletim = boletim;
    }
}
