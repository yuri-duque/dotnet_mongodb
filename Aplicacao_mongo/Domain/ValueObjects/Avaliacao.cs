using FluentValidation;
using FluentValidation.Results;
using System;

namespace Domain.ValueObjects
{
    public class Avaliacao : AbstractValidator<Avaliacao>
    {
        public int Estrelas { get; private set; }
        public string Comentario { get; private set; }

        public Avaliacao() { }

        public Avaliacao(int estrelas, string comentario)
        {
            Estrelas = estrelas;
            Comentario = comentario;
        }

        #region Validaçoes

        public virtual ValidationResult ValidationResult { get; set; }

        public virtual bool Validar()
        {
            ValidarEstrelas();
            ValidarComentario();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        private void ValidarEstrelas()
        {
            RuleFor(x => x.Estrelas)
                .GreaterThan(0).WithMessage("Número de estrelas deve ser maior que zero")
                .LessThanOrEqualTo(5).WithMessage("Numero de estrelas deve ser menor ou igual a cinco");
        }

        private void ValidarComentario()
        {
            RuleFor(x => x.Comentario)
                .NotEmpty().WithMessage("Comentario não pode ser vazio")
                .MaximumLength(100).WithMessage("Comentario pode ter no maximo 100 caracteres");
        }

        #endregion
    }
}
