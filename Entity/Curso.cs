namespace sistema_gestao_faculdade.Entity
{
    internal class Curso
    {
        public string Codigo { get; }
        public string Nome { get; }
        public TipoCurso Tipo { get; }
        public List<Disciplina> disciplinas { get; } = new List<Disciplina>();

        public Curso(string codigo, string nome, TipoCurso tipo)
        {
            Codigo = codigo;
            Nome = nome;
            Tipo = tipo;
        }

        public static string FormatarTipo(TipoCurso tipo)
        {
            return tipo switch
            {
                TipoCurso.Graduacao => "Graduação",
                TipoCurso.PosGraduacao => "Pós-Graduação",
                _ => tipo.ToString()
            };
        }
    }
}