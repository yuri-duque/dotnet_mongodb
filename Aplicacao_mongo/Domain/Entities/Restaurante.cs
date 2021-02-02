using Domain.Enums;
using Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Restaurante : AbstractValidator<Restaurante>
    {
        public string Id { get; }
        public string Nome { get; }
        public ECozinha Cozinha { get; }
        public Endereco Endereco { get; private set; }
        public IList<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();

        #region Construtores

        public Restaurante(string id, string nome, ECozinha cozinha)
        {
            Id = id;
            Nome = nome;
            Cozinha = cozinha;
        }

        public Restaurante(string nome, ECozinha cozinha)
        {
            Nome = nome;
            Cozinha = cozinha;
        }

        #endregion

        #region Validaçoes

        public virtual ValidationResult ValidationResult { get; set; }

        public virtual bool Validar()
        {
            ValidarNome();
            ValidationResult = Validate(this);

            ValidarEndereco();

            return ValidationResult.IsValid;
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Nome não pode ser vazio.")
                .MaximumLength(30).WithMessage("Nome pode ter no maximo 30 caracteres.");
        }

        private void ValidarEndereco()
        {
            if (Endereco.Validar())
                return;

            ValidationResult.Errors.Union(Endereco.ValidationResult.Errors);
        }

        #endregion

        #region Declarações

        public void SetEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }

        public void InserirAvaliacao(Avaliacao avaliacao)
        {
            Avaliacoes.Add(avaliacao);
        }

        #endregion
    }
}
