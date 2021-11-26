$(document).ready(() => {
    
    $('.toast').toast('show');

    $('#CPF').inputmask("999.999.999-99");
    $('#CEP').inputmask("99999-999");
    $('#Telefone').inputmask("(99) 99999-9999");


    $("#CEP").blur(function () {

        function limpa_formulário_cep() {
            // Limpa valores do formulário de cep.
            $("#rua").val("");
            $("#bairro").val("");
            $("#cidade").val("");
            $("#uf").val("");
            $("#ibge").val("");
        }

        var cep = $(this).val().replace(/\D/g, '');

        if (cep != "") {

            var validacep = /^[0-9]{8}$/;

            if (validacep.test(cep)) {

                $("#Endereco").val("...");
                $("#Bairro").val("...");
                $("#Cidade").val("...");
                $("#Uf").val("...");

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        $("#Endereco").val(dados.logradouro);
                        $("#Bairro").val(dados.bairro);
                        $("#Cidade").val(dados.localidade);
                        $("#Uf").val(dados.uf);
                    }
                    else {
                        limpa_formulário_cep();
                        alert("CEP não encontrado.");
                    }
                });
            }
            else {
                limpa_formulário_cep();
                alert("Formato de CEP inválido.");
            }
        }
        else {
            limpa_formulário_cep();
        }
    });
});