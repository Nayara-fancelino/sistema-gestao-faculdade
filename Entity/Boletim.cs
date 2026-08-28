namespace sistema_gestao_faculdade.Entity
{
    internal class Boletim
    {
        public Aluno Aluno { get; }
        public Curso Curso { get; }
        public Disciplina Disciplina { get; }
        public double Nota { get; private set; }
        public bool Aprovado { get; private set; }
        public string Situacao => Aprovado ? "Aprovado" : "Reprovado";
        public Boletim(Aluno aluno, Curso curso, Disciplina disciplina)
        {
            Aluno = aluno;
            Curso = curso;
            Disciplina = disciplina;
            Nota = 0;
        }

        public void LancarNota(double nota, TipoCurso tipoCurso)
        {
            ValidarNota(nota, tipoCurso);
            Nota = nota;
        }

        private void ValidarNota(double nota, TipoCurso tipoCurso)
        {
            if (tipoCurso == TipoCurso.Graduacao)
            {
                if (nota >= 7)
                    TornarAprovado();
                else
                    TornarReprovado();
            }

            if (tipoCurso == TipoCurso.PosGraduacao)
            {
                if (nota >= 8)
                    TornarAprovado();
                else
                    TornarReprovado();
            }
        }

        private void TornarAprovado()
        {
            Aprovado = true;
        }

        private void TornarReprovado()
        {
            Aprovado = false;
        }
    }
}
