using FluentValidation;
using FluentValidation.Results;

namespace Domain.ValueObjects
{
    public class Endereco : AbstractValidator<Endereco>
    {
        public string Logradouro { get; }
        public string Numero { get; }
        public string Cidade { get; }
        public string UF { get; }
        public string CEP { get; }

        public Endereco(string uf, string cidade, string cep, string logradouro, string numero)
        {
            Logradouro = logradouro;
            Numero = numero;
            Cidade = cidade;
            UF = uf;
            CEP = cep;
        }

        #region Validaçoes

        public virtual ValidationResult ValidationResult { get; set; }

        public virtual bool Validar()
        {
            ValidarLogradouro();
            ValidarCidade();
            ValidarUF();
            ValidarCEP();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        private void ValidarLogradouro()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("Logradouro não pode ser vazio.")
                .MaximumLength(50).WithMessage("Logradouro pode ter no maximo 50 caracteres.");
        }

        private void ValidarCidade()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("Cidade não pode ser vazio.")
                .MaximumLength(100).WithMessage("Cidade pode ter no maximo 100 caracteres.");
        }

        private void ValidarUF()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("UF não pode ser vazio.")
                .MaximumLength(2).WithMessage("UF pode ter no maximo 2 caracteres.");
        }

        private void ValidarCEP()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("CEP não pode ser vazio.")
                .MaximumLength(8).WithMessage("CEP pode ter no maximo 8 caracteres.");
        }

        #endregion
    }
}