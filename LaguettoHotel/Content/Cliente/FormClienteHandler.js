document.querySelector('#Nome').addEventListener('blur', validaCampos);
document.querySelector('#NomeSocial').addEventListener('blur', validaCampos);
document.querySelector('#CPF').addEventListener('blur', validaCampos);
document.querySelector('#Rg').addEventListener('blur', validaCampos);
document.querySelector('#Datanascimento').addEventListener('blur', validaCampos);
document.querySelector('#Sexo').addEventListener('blur', validaCampos);
document.querySelector('#CEP').addEventListener('blur', validaCampos);
document.querySelector('#Email').addEventListener('blur', validaCampos);
document.querySelector('#Celular').addEventListener('blur', validaCampos);




function validaCampos() {
    document.querySelector('form').addEventListener('submit', e => {
        e.preventDefault();

        var campos = [
            'Nome',
            'CPF',
            /*'Email',*/
            'CEP',
            /*'Numero',*/
            /*'Senha',*/
            'Celular'
        ];

        var erros = [];

        campos.forEach(elem => {

            valor = document.querySelector(`#${elem}`).value;

            if (!valor.length == 0) {

                erros[`${elem}`] = null;

                switch (elem) {
                    case 'Nome':
                        var reg = /[a-zA-Z\u00C0-\u00FF ]+/i
                        if (!reg.test(valor)) {
                            erros[`${elem}`] = `${elem} inválido.`;
                        }
                        break;
                    case 'CPF':
                        var reg = /^([0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}|[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2})$/

                        if (!reg.test(valor)) {
                            erros[`${elem}`] = `${elem} inválido.`;
                            break;
                        }

                        cpf = valor;

                        // Elimina possivel mascara
                        cpf = cpf.replace(/\D/g, '');

                        // Verifica se o numero de digitos informados é igual a 11 
                        if (cpf.length != 11) {
                            erros[`${elem}`] = `${elem} inválido.`;
                            break;
                        }
                        // Verifica se nenhuma das sequências invalidas abaixo 
                        // foi digitada. Caso afirmativo, retorna falso
                        else if (cpf == '00000000000' ||
                            cpf == '11111111111' ||
                            cpf == '22222222222' ||
                            cpf == '33333333333' ||
                            cpf == '44444444444' ||
                            cpf == '55555555555' ||
                            cpf == '66666666666' ||
                            cpf == '77777777777' ||
                            cpf == '88888888888' ||
                            cpf == '99999999999') {
                            erros[`${elem}`] = `${elem} inválido.`;
                            break;
                            // Calcula os digitos verificadores para verificar se o
                            // CPF é válido
                        } else {

                            for (t = 9; t < 11; t++) {

                                for (d = 0, c = 0; c < t; c++) {
                                    d += cpf[c] * ((t + 1) - c);
                                }
                                d = ((10 * d) % 11) % 10;
                                if (cpf[c] != d) {
                                    erros[`${elem}`] = `${elem} inválido.`;
                                    break;
                                }
                            }
                        }
                        break;
                    case 'Email':
                        var reg = /^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$/
                        if (!reg.test(valor)) {
                            erros[`${elem}`] = `${elem} inválido.`;
                        }
                        break;
                }
            }
            else {
                erros[`${elem}`] = `${elem} não pode ser vazio.`;
            }
        });

        console.log(erros);

        var elements = '';
        campos.forEach(item => {
            var valor = erros[`${item}`];
            if (valor != null) {
                elements += `${valor}`;

            }
        });

        console.log(elements);

        if (!elements.length == 0 && !$('.erros').hasClass('d-none')) {
            $(".erros").addClass('d-none');
            $(".erros").html(elements);
        } else {
            $(".erros").removeClass('d-none');
        }
    });

}
