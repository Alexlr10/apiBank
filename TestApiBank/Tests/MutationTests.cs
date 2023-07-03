using apiBank.src.Api;
using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Responses;
using Moq;
using Xunit;

namespace TestApiBank.Tests
{
    public class MutationTests
    {
        private readonly Mock<IUpsertCCHandler> upsertCCHandlerMock;
        private readonly Mock<ISacarContaHandler> sacarContaHandlerMock;
        private readonly Mock<IDepositarContaHandler> depositarContaHandlerMock;

        public MutationTests()
        {
            upsertCCHandlerMock = new Mock<IUpsertCCHandler>();
            sacarContaHandlerMock = new Mock<ISacarContaHandler>();
            depositarContaHandlerMock = new Mock<IDepositarContaHandler>();
        }

        [Fact]
        public void UpsertContaDelegateExecuteToHandlerReturnsUpsertCCResponse()
        {
            // Arrange
            var request = new UpsertCCRequest
            {
                Id = Guid.NewGuid(),
                Conta = "0004",
                Saldo = 4000.0
            };

            var expectedResponse = new UpsertCCResponse
            {
                Payload = new UpsertCCResponsePayload
                {
                    Id = request.Id.Value,
                    Conta = request.Conta,
                    Saldo = request.Saldo
                }
            };

            upsertCCHandlerMock.Setup(handler => handler.Execute(request)).Returns(expectedResponse);
            var mutation = new Mutation();

            // Act
            var result = mutation.UpsertConta(upsertCCHandlerMock.Object, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Payload.Id, result.Payload.Id);
            Assert.Equal(expectedResponse.Payload.Conta, result.Payload.Conta);
            Assert.Equal(expectedResponse.Payload.Saldo, result.Payload.Saldo);
        }

        [Fact]
        public void SaqueEmContaDelegateExecuteToHandlerReturnsUpsertCCResponse()
        {
            // Arrange
            var request = new MovimentarContaRequest
            {
                Conta = "0001",
                Valor = 500.0
            };

            var expectedResponse = new UpsertCCResponse
            {
                Payload = new UpsertCCResponsePayload
                {
                    Id = Guid.NewGuid(),
                    Conta = request.Conta,
                    Saldo = 500.0
                }
            };

            sacarContaHandlerMock.Setup(handler => handler.Execute(request)).Returns(expectedResponse);
            var mutation = new Mutation();

            // Act
            var result = mutation.SaqueEmConta(sacarContaHandlerMock.Object, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Payload.Id, result.Payload.Id);
            Assert.Equal(expectedResponse.Payload.Conta, result.Payload.Conta);
            Assert.Equal(expectedResponse.Payload.Saldo, result.Payload.Saldo);
        }

        [Fact]
        public void DepositoEmContaDelegateExecuteToHandlerReturnsUpsertCCResponse()
        {
            // Arrange
            var request = new MovimentarContaRequest
            {
                Conta = "0001",
                Valor = 1000.0
            };

            var expectedResponse = new UpsertCCResponse
            {
                Payload = new UpsertCCResponsePayload
                {
                    Id = Guid.NewGuid(),
                    Conta = request.Conta,
                    Saldo = 2000.0
                }
            };

            depositarContaHandlerMock.Setup(handler => handler.Execute(request)).Returns(expectedResponse);
            var mutation = new Mutation();

            // Act
            var result = mutation.DepositoEmConta(depositarContaHandlerMock.Object, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Payload.Id, result.Payload.Id);
            Assert.Equal(expectedResponse.Payload.Conta, result.Payload.Conta);
            Assert.Equal(expectedResponse.Payload.Saldo, result.Payload.Saldo);
        }
    }
}
