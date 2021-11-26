using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaguettoHotel.Models
{
    public class ClienteViewModel
    {
        /*************** IDpessoa ***************/
        [Key]
        [Display(Name = "ID")]
        public int IDpessoa { get; set; }

        /*************** Nome Completo ***************/
        [Required(ErrorMessage = "Por favor preencha o seu Nome Completo!")]
        [MaxLength(length: 150, ErrorMessage = "Nome excede o máximo de caracteres permitido!")]
        [MinLength(length: 3, ErrorMessage = "Nome precisa no mínimo conter 3 caracteres!")]
        public string Nome { get; set; }

        /*************** CPF ***************/
        [Display(Name = "CPF", Prompt = "Insira seu CPF", Description = "Descrição")]
        [Required(ErrorMessage = "Por favor preencha o seu CPF!")]
        public string CPF { get; set; }

        /*************** Email ***************/
        [Display(Name = "Email", Prompt = "Insira o seu Email", Description = "Descrição")]
        [Required(ErrorMessage = "Por favor informe seu Email!")]
        [MaxLength(length: 150, ErrorMessage = "Email excede o máximo de caracteres permitido!")]
        [MinLength(length: 3, ErrorMessage = "Email precisa no mínimo conter 3 caracteres!")]
        public string Email { get; set; }

        /*************** Senha ***************/
        [Display(Name = "Senha", Prompt = "Insira sua Senha", Description = "Descrição")]
        [Required(ErrorMessage = "Por favor Informe sua senha de acesso")]
        [MaxLength(length: 150, ErrorMessage = "Senha excede o máximo de 20 caracteres permitidos!")]
        [MinLength(length: 3, ErrorMessage = "Senha precisa no mínimo conter 8 caracteres!")]
        public string Senha { get; set; }

        /*************** CEP ***************/
        [Display(Name = "CEP", Prompt = "Insira seu CEP", Description = "Descrição")]
        [Required(ErrorMessage = "Por favor informe seu CEP!")]
        [MaxLength(length: 150, ErrorMessage = "Nome Filiacao 2 excede o máximo de caracteres permitido!")]
        [MinLength(length: 3, ErrorMessage = "Nome Filiacao 2 precisa no mínimo conter 3 caracteres!")]
        public string CEP { get; set; }

        /*************** Endereço ***************/
        [Display(Name = "Endereço", Prompt = "Insira seu Endereço", Description = "Descrição")]
        [MaxLength(length: 150, ErrorMessage = "Nome excede o máximo de caracteres permitido!")]
        [MinLength(length: 3, ErrorMessage = "Nome precisa no mínimo conter 3 caracteres!")]
        public string Endereco { get; set; }

        /*************** Bairro ***************/
        [Display(Name = "Bairro", Prompt = "Insira seu Bairro", Description = "Descrição")]
        [Required(ErrorMessage = "Insira seu Bairro!")]
        [MaxLength(length: 50, ErrorMessage = "O campo Bairro excede o máximo de cartacteres permitido")]
        public string Bairro { get; set; }

        /*************** Cidade ***************/
        [Display(Name = "Cidade", Prompt = "Insira seu Cidade", Description = "Descrição")]
        [Required(ErrorMessage = "Insira sua Cidade!")]
        [MaxLength(length: 50, ErrorMessage = "O campo Cidade excede o máximo de cartacteres permitido")]
        public string Cidade { get; set; }

        /*************** Uf ***************/
        [Display(Name = "Estado", Prompt = "Selecione seu Estado", Description = "Descrição")]
        [Required(ErrorMessage = "Selecione seu estado")]
        [MaxLength(length: 2, ErrorMessage = "O campo Estado excede o máximo de cartacteres permitido")]
        public string Uf { get; set; }

        /*************** Telefone ***************/
        [Display(Name = "Telefone", Prompt = "Insira seu número de Telefone", Description = "Descrição")]
        [Required(ErrorMessage = "Por favor informe seu Telefone.")]
        public string Telefone { get; set; }

    }
}