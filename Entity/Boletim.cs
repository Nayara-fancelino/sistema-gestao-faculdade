namespace sistema_gestao_faculdade.Entity
{
    public class Boletim
    {
        public Aluno Aluno { get; }
        public Curso Curso { get; }
        public Dictionary<string, double> Notas { get; } = new();

        public Boletim(Aluno aluno, Curso curso)
        {
            Aluno = aluno;
            Curso = curso;
        }

        public void LancarNota(Disciplina disciplina, double nota)
        {
            Notas[disciplina.Codigo] = nota;
        }

        public string ObterSituacao(double nota)
        {
            if (Curso.Tipo == TipoCurso.Graduacao) return nota >= 7 ? "Aprovado" : "Reprovado";

            return nota >= 8 ? "Aprovado" : "Reprovado";
        }
    }
}