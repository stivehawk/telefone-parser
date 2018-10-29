using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TelefoneParser.Tests
{
    [TestClass]
    public class TelefoneTests
    {
        /* Teste de conteúdo parseado */
        [DataTestMethod]
        [DataRow("911111111", null)]
        [DataRow("77 911111111", null)]

        [DataRow("55 77 911111111", "55")] // Sem máscara
        [DataRow("+55 (77) 911111111", "55")] // Com máscara
        public void CodigoPais_Parseado(string telefone, string codigoEsperado)
        {
            var codigoPais = Telefone.Parse(telefone).CodigoPais;
            Assert.IsTrue(string.Equals(codigoPais, codigoEsperado));
        }

        [DataTestMethod]
        [DataRow("911111111", null)]

        [DataRow("77 911111111", "77")] // Sem máscara
        [DataRow("(77) 91111-1111", "77")] // Com máscara

        [DataRow("55 77 911111111", "77")] // Sem máscara
        [DataRow("+55 (77) 91111-1111", "77")] // Com máscara
        public void CodigoEstado_Parseado(string telefone, string codigoEsperado)
        {
            var codigoEstado = Telefone.Parse(telefone).CodigoEstado;
            Assert.IsTrue(string.Equals(codigoEstado, codigoEsperado));
        }

        [DataTestMethod]
        [DataRow("12345678", "12345678")] // Sem máscara
        [DataRow("1234-5678", "12345678")] // Com máscara

        [DataRow("911111111", "911111111")] // Sem máscara
        [DataRow("91111-1111", "911111111")] // Com máscara

        [DataRow("77 911111111", "911111111")] // Sem máscara
        [DataRow("(77) 91111-1111", "911111111")] // Com máscara

        [DataRow("55 77 911111111", "911111111")] // Sem máscara
        [DataRow("+55 (77) 91111-1111", "911111111")] // Com máscara
        public void Numero_Parseado(string telefone, string numeroEsperado)
        {
            var codigoPais = Telefone.Parse(telefone).Numero;
            Assert.IsTrue(string.Equals(codigoPais, numeroEsperado));
        }

        
        /* Testes de validade */
        [DataTestMethod]
        [DataRow("12345678")] // Telefone fixo
        [DataRow("912345678")] // Telefone celular
        public void NumeroValido_Verdadeiro(string telefone)
        {
            Assert.IsTrue(Telefone.Parse(telefone).NumeroValido);
        }

        [DataTestMethod]
        [DataRow("1234567")] // Digitos a menos
        [DataRow("123456789")] // Celular inválido
        public void NumeroValido_Falso(string telefone)
        {
            Assert.IsFalse(Telefone.Parse(telefone).NumeroValido);
        }

        [DataTestMethod]
        [DataRow("11 911111111")]
        public void CodigoEstadoValido_Verdadeiro(string telefone)
        {
            Assert.IsTrue(Telefone.Parse(telefone).CodigoEstadoValido);
        }

        [DataTestMethod]
        [DataRow("50 911111111")] // DDD inválido
        public void CodigoEstadoValido_Falso(string telefone)
        {
            Assert.IsFalse(Telefone.Parse(telefone).CodigoEstadoValido);
        }

        [DataTestMethod]
        [DataRow("12345678")] // Telefone fixo
        [DataRow("912345678")] // Telefone celular

        [DataRow("91111-1111")] // Celular formatado 1
        [DataRow("9.1111.1111")] // Celular formatado 2

        [DataRow("77 911111111")] // Celular com DDD
        [DataRow("(77) 91111-1111")] // Celular com DDD formatado
        [DataRow("77911111111")] // Celular com DDD sem formatação

        [DataRow("55 77 911111111")] // Celular com código de país + DDD
        [DataRow("+55 (77) 91111-1111")] // Celular com código de país + DDD formatado
        [DataRow("5577911111111")] // Celular com código de país + DDD sem formatação
        public void TelefoneValido_Verdadeiro(string telefone)
        {
            Assert.IsTrue(Telefone.Parse(telefone).TelefoneValido);
        }

        [DataTestMethod]
        [DataRow("1234567")] // Digitos a menos
        [DataRow("123456789")] // Celular inválido

        [DataRow("77 1234567")] // Digitos a menos
        [DataRow("77 123456789")] // Celular inválido
        [DataRow("50 123456789")] // DDD inválido

        [DataRow("55 77 1234567")] // Digitos a menos
        [DataRow("55 77 123456789")] // Celular inválido
        [DataRow("55 50 123456789")] // DDD inválido
        public void TelefoneValido_Falso(string telefone)
        {
            Assert.IsFalse(Telefone.Parse(telefone).TelefoneValido);
        }
    }
}