using apiBank.src.Api;
using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;
using apiBank.src.Database.Domain;
using apiBank.src.Database.Repositories.Interfaces;
using Moq;
using TestApiBank.Data;

namespace TestApiBank.Tests
{
    public class QueryTests
    {
        private readonly IQueryable<ContaCorrente> contas;
        private readonly Mock<IContaCorrenteRepository> contaCorrenteRepositoryMock;

        public QueryTests()
        {
            contas = ContaCorrenteData.GetContas();
            contaCorrenteRepositoryMock = new Mock<IContaCorrenteRepository>();
        }

        [Fact]
        public void GetContasDeveRetornarListasDeContasResponses()
        {
            // Arrange
            var mockHandler = new Mock<IGetAllCCHandler>();
            var listaContaCorrente = new ListasDeContasResponses();
            listaContaCorrente.Payload = new List<ContaResponseItem>();

            foreach (var conta in contas)
            {
                var contaResponseItem = new ContaResponseItem
                {
                    Id = conta.Id,
                    Conta = conta.Conta,
                    Saldo = conta.Saldo
                };

                listaContaCorrente.Payload.Add(contaResponseItem);
            }

            mockHandler.Setup(handler => handler.Execute()).Returns(listaContaCorrente);
            var query = new Query();

            // Act
            var result = query.GetContas(mockHandler.Object);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ListasDeContasResponses>(result);
            // Add more assertions as needed
        }

        [Fact]
        public void GetContaDeveRetornarContaResponsePeloId()
        {
            // Arrange
            var mockHandler = new Mock<IGetByIdCCHandler>();
            var request = new GetByIdCCRequest
            {
                Id = Guid.Parse("34B3B9F6-9952-48CC-AB50-3CEA740F2F3C")
            };

            var contaResponse = contas.SingleOrDefault(c => c.Id == request.Id);
            var expectedResponse = new ContaResponse
            {
                Payload = new ContaResponseItem
                {
                    Id = contaResponse.Id,
                    Conta = contaResponse.Conta,
                    Saldo = contaResponse.Saldo,
                }
            };

            mockHandler.Setup(handler => handler.Execute(request)).Returns(expectedResponse);
            var query = new Query();

            // Act
            var result = query.GetConta(mockHandler.Object, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Payload.Id, request.Id);
            Assert.IsType<ContaResponse>(result);
        }

        [Fact]
        public void GetSaldoDeveRetornarContaResponsePeloNumeroDaConta()
        {
            // Arrange
            var mockHandler = new Mock<IGetByContaVerSaldoHandler>();
            var request = new GetByContaVerSaldoRequest
            {
                Conta = "0001"
            };

            var contaResponse = contas.SingleOrDefault(c => c.Conta == request.Conta);
            var expectedResponse = new ContaResponse
            {
                Payload = new ContaResponseItem
                {
                    Id = contaResponse.Id,
                    Conta = contaResponse.Conta,
                    Saldo = contaResponse.Saldo,
                }
            };

            mockHandler.Setup(handler => handler.Execute(request)).Returns(expectedResponse);
            var query = new Query();

            // Act
            var result = query.GetSaldo(mockHandler.Object, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Payload.Conta, request.Conta);
            Assert.IsType<ContaResponse>(result);
        }

        [Fact]
        public void GetSaldoContaInexistenteThrowsException()
        {
            // Arrange
            var mockHandler = new Mock<IGetByContaVerSaldoHandler>();
            var request = new GetByContaVerSaldoRequest
            {
                Conta = "9999" // Número de conta inexistente
            };

            // Configurar o mockHandler para lançar uma exceção quando o número da conta não existe
            mockHandler.Setup(handler => handler.Execute(request)).Throws(new Exception("Conta não encontrada"));
            var query = new Query();

            // Act & Assert
            Assert.Throws<Exception>(() => query.GetSaldo(mockHandler.Object, request));
        }

        [Fact]
        public void GetContaIdInexistenteThrowsException()
        {
            // Arrange
            var mockHandler = new Mock<IGetByIdCCHandler>();
            var request = new GetByIdCCRequest
            {
                Id = Guid.NewGuid()
            };

            // Configurar o mockHandler para lançar uma exceção quando o ID não existe
            mockHandler.Setup(handler => handler.Execute(request)).Throws(new Exception("Conta não encontrada"));
            var query = new Query();

            // Act & Assert
            Assert.Throws<Exception>(() => query.GetConta(mockHandler.Object, request));
        }
    }
}
