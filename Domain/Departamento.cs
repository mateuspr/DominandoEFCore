using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Curso.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        private List<Funcionario> _funcionarios;
        public List<Funcionario> Funcionarios
        {
            //get => _lazyLoader.Load(this, ref _funcionarios);
            get
            {
                _lazyLoader.Invoke(this, nameof(Funcionarios));
                return _funcionarios;
            }
            set => _funcionarios = value;
        }

        //private ILazyLoader _lazyLoader { get; set; }
        private Action<object, string> _lazyLoader { get; set; }

        public Departamento()
        {

        }

        private Departamento(Action<object, string> lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
    }
}