﻿using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace Domain.Entities
{
    public class Restaurante : AbstractValidator<Restaurante>
    {
        public Restaurante(string id, string nome, ECozinha cozinha)
        {
            Id = id;
            Nome = nome;
            Cozinha = cozinha;
        }

        public string Id { get; }
        public string Nome { get; }
        public ECozinha Cozinha { get; }
        public Endereco Endereco { get; private set; }

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

        #endregion
    }
}
