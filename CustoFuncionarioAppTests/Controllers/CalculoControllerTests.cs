using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustoFuncionarioApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using CustoFuncionarioApp.Models;
using System.Threading.Tasks;

namespace CustoFuncionarioApp.Controllers.Tests
{
    [TestClass()]
    public class CalculoControllerTests
    {
        private CalculoController controller;

        [TestInitialize]
        public void inicializar()
        {
            controller = new CalculoController();
        }

        [TestMethod]
        [DataRow(7786.02, 1000.00, 500.00, 700.00)]
        [DataRow(8000.00, 0.00, 0.00, 0.00)]
        [DataRow(0.00, 300.00, 150.00, 100.00)]
        [DataRow(5000.00, 1000.00, 700.00, 900.00)]

        public void TestarCusto(double salarioBruto, double planoSaude, double seguroVida, double outrosBeneficios)
        {
            var custo = new Custo
            {
                SalarioBruto = Convert.ToDecimal(salarioBruto),
                PlanoSaude = Convert.ToDecimal(planoSaude),
                SeguroVida = Convert.ToDecimal(seguroVida),
                OutrosBeneficios = Convert.ToDecimal(outrosBeneficios)
            };

            Assert.AreEqual(Convert.ToDecimal(salarioBruto), custo.SalarioBruto);
        }

        [TestMethod]
        [DataRow(7786.02, 1000.00, 500.00, 700.00)]
        [DataRow(8000.00, 0.00, 0.00, 0.00)]
        [DataRow(0.00, 300.00, 150.00, 100.00)]
        [DataRow(5000.00, 1000.00, 700.00, 900.00)]
        public void Relatorio_EntradaValida_RetornaViewComModelo(double salarioBruto, double planoSaude, double seguroVida, double outrosBeneficios)
        {
            // Arrange
            var custo = new Custo
            {
                SalarioBruto = Convert.ToDecimal(salarioBruto),
                PlanoSaude = Convert.ToDecimal(planoSaude),
                SeguroVida = Convert.ToDecimal(seguroVida),
                OutrosBeneficios = Convert.ToDecimal(outrosBeneficios)
            };

            // Act
            var resultado = controller.Relatorio(custo) as ViewResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(custo, resultado.Model);
        }

        [TestMethod]
        public void Relatorio_EntradaNula_RetornaViewComModeloNulo()
        {
            // Act
            var resultado = controller.Relatorio(null) as ViewResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsNull(resultado.Model);
        }


        [TestMethod]
        public void requisicao_DeveRetornarTipoProdutos()
        {
            // Arrange
            var esperado = typeof(Custo);
            var custo = new Custo
            {
                SalarioBruto = 3000M,
                PlanoSaude = 500M,
                SeguroVida = 100M,
                OutrosBeneficios = 200M
            };

            // Act
            var resultado = controller.Relatorio(custo);
            var viewResult = resultado as ViewResult;
            var obtido = viewResult?.Model;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(esperado, obtido?.GetType());
        }


        [TestClass]
        public class CustoTests
        {
            [TestMethod]
            public void Testar_Calculo_CustoTotal()
            {
                // Arrange 
                var custo = new Custo
                {
                    SalarioBruto = 3000M,
                    PlanoSaude = 500M,
                    SeguroVida = 100M,
                    OutrosBeneficios = 200M
                };

                // Act 
                var custoTotal = custo.getCustoTotal();

                // Assert 
                decimal inssEsperado = custo.getINSS_Valor();
                decimal fgtsEsperado = custo.getFGTS();
                decimal decimoTerceiroEsperado = custo.get13oSalario();
                decimal feriasEsperado = custo.getFerias();
                decimal custoTotalEsperado = custo.SalarioBruto + inssEsperado + fgtsEsperado + decimoTerceiroEsperado + feriasEsperado + custo.PlanoSaude + custo.SeguroVida + custo.OutrosBeneficios;

                Assert.AreEqual(custoTotalEsperado, custoTotal);
            }


            [TestMethod()]
            public void RelatorioControllerView()
            {
                CalculoController controller = new CalculoController();
                var custo = new Custo();
                var resultado = controller.Relatorio(custo) as ViewResult;
                Assert.IsNotNull(resultado);
            }


            [TestMethod]
            public void TesteINSS()
            {
                // Arrange
                var custo = new Custo { SalarioBruto = 2500M };
                var aliquotaEsperada = 0.09M;
                var valorEsperado = custo.SalarioBruto * aliquotaEsperada;

                // Act
                var resultado = custo.getINSS_Valor();

                // Assert
                Assert.AreEqual(valorEsperado, resultado);
            }

            [TestMethod]
            public void TesteFGTS()
            {
                // Arrange
                var custo = new Custo { SalarioBruto = 4000M };
                var valorEsperado = custo.SalarioBruto * 0.08M;

                // Act
                var resultado = custo.getFGTS();

                // Assert
                Assert.AreEqual(valorEsperado, resultado);
            }

            [TestMethod]
            public void Teste13Salario()
            {
                // Arrange
                var custo = new Custo { SalarioBruto = 1000M };
                var valorEsperado = custo.SalarioBruto;

                // Act
                var resultado = custo.get13oSalario();

                // Assert
                Assert.AreEqual(valorEsperado, resultado);
            }

            [TestMethod]
            public void TesteFerias()
            {
                // Arrange
                var custo = new Custo { SalarioBruto = 2400M };
                var valorEsperado = custo.SalarioBruto + (custo.SalarioBruto / 3);

                // Act
                var resultado = custo.getFerias();

                // Assert
                Assert.AreEqual(valorEsperado, resultado);
            }
        }

        [TestMethod()]
        public void RelatorioTest()
        {
            Assert.Fail();
        }
    }
}

