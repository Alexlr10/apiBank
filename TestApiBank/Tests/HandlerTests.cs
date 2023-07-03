using apiBank.src.BusinessRules.Handlers;
using apiBank.src.BusinessRules.Requests;
using apiBank.src.BusinessRules.Validators.Interfaces;
using apiBank.src.Database.Domain;
using apiBank.src.Database.Repositories.Interfaces;
using FluentValidation.Results;
using Moq;
using TestApiBank.Data;

namespace TestApiBank.Tests
{
    public class HandlerTests
    {
        private readonly IQueryable<ContaCorrente> contas;
        private readonly Mock<IContaCorrenteRepository> contaCorrenteRepositoryMock;

        private readonly Mock<IContaCorrenteValidator> validatorMock;
        private readonly UpsertCCHandler upsertCCHandler;
        private readonly SacarContaHandler sacarHandler;
        private readonly DepositarContaHandler depositarHandler;

        public HandlerTests()
        {
            contas = ContaCorrenteData.GetContas();
            contaCorrenteRepositoryMock = new Mock<IContaCorrenteRepository>();
            validatorMock = new Mock<IContaCorrenteValidator>();
            upsertCCHandler = new UpsertCCHandler(contaCorrenteRepositoryMock.Object, validatorMock.Object);
            sacarHandler = new SacarContaHandler(contaCorrenteRepositoryMock.Object);
            depositarHandler = new DepositarContaHandler(contaCorrenteRepositoryMock.Object);
        }

        [Fact]
        public void ExecuteDepositoComSucessoDeveRetornarResponseCorreto()
        {
            // Arrange
            var request = new MovimentarContaRequest
            {
                Conta = "123456",
                Valor = 100.0
            };

            var conta = new ContaCorrente
            {
                Id = Guid.NewGuid(),
                Conta = request.Conta,
                Saldo = 500.0
            };

            contaCorrenteRepositoryMock.Setup(repo => repo.GetByConta(request.Conta)).Returns(conta);
            contaCorrenteRepositoryMock.Setup(repo => repo.Atualizar(conta)).Returns(conta);

            // Act
            var response = depositarHandler.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Payload);
            Assert.Equal(conta.Id.Value, response.Payload.Id);
            Assert.Equal(conta.Conta, response.Payload.Conta);
        }

        [Fact]
        public void Execute_ContaNaoEncontrada_DeveLancarException()
        {
            // Arrange
            var request = new MovimentarContaRequest
            {
                Conta = "123456",
                Valor = 100.0
            };

            contaCorrenteRepositoryMock.Setup(repo => repo.GetByConta(request.Conta)).Returns((ContaCorrente)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => depositarHandler.Execute(request));
            Assert.Equal("Conta não encontrada", exception.Message);
        }


        [Fact]
        public void ExecuteComSolicitacaoValidaDeveSacarValorDaContaCorrenteERetornarResponse()
        {
            // Arrange
            var request = new MovimentarContaRequest
            {
                Conta = "1234",
                Valor = 50.0
            };

            var contaEntity = new ContaCorrente { Id = Guid.NewGuid(), Conta = request.Conta, Saldo = 100.0 };

            contaCorrenteRepositoryMock.Setup(repo => repo.GetByConta(request.Conta)).Returns(contaEntity);

            // Act
            var response = sacarHandler.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Errors);
            Assert.NotNull(response.Payload);
            Assert.Equal(request.Conta, response.Payload.Conta);
            Assert.Equal(request.Valor, response.Payload.Saldo);
            contaCorrenteRepositoryMock.Verify(repo => repo.Atualizar(contaEntity), Times.Once);
        }

        [Fact]
        public void ExecuteContaNaoEncontradaDeveLancarExcecao()
        {
            // Arrange
            var request = new MovimentarContaRequest
            {
                Conta = "123456",
                Valor = 100.0
            };

            contaCorrenteRepositoryMock.Setup(repo => repo.GetByConta(request.Conta)).Returns((ContaCorrente)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => sacarHandler.Execute(request));
            Assert.Equal("Conta não encontrada", exception.Message);
        }

        [Fact]
        public void ExecuteSaldoInsuficienteParaSaqueDeveLancarExcecao()
        {
            // Arrange
            var request = new MovimentarContaRequest
            {
                Conta = "123456",
                Valor = 100.0
            };

            var conta = new ContaCorrente
            {
                Id = Guid.NewGuid(),
                Conta = "123456",
                Saldo = 50.0
            };

            contaCorrenteRepositoryMock.Setup(repo => repo.GetByConta(request.Conta)).Returns(conta);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => sacarHandler.Execute(request));
            Assert.Equal("Valor em saldo insuficiente para saque", exception.Message);
        }


        [Fact]
        public void ExecuteComSolicitacaoValidaDeveSacarValorDaConta()
        {
            // Arrange
            var request = new UpsertCCRequest
            {
                Id = null,
                Conta = "1234",
                Saldo = 1000.0
            };

            var validatorResult = new ValidationResult();
            var contaEntity = new ContaCorrente { Id = Guid.NewGuid(), Conta = request.Conta, Saldo = request.Saldo };

            validatorMock.Setup(validator => validator.Validate(request)).Returns(validatorResult);
            contaCorrenteRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((ContaCorrente)null);
            contaCorrenteRepositoryMock.Setup(repo => repo.Save(It.IsAny<ContaCorrente>())).Returns(contaEntity);

            // Act
            var response = upsertCCHandler.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Errors);
            Assert.NotNull(response.Payload);
            Assert.Equal(request.Conta, response.Payload.Conta);
            Assert.Equal(request.Saldo, response.Payload.Saldo);
            Assert.Equal(contaEntity.Id.Value, response.Payload.Id);
        }

        [Fact]
        public void ExecuteComSolicitacaoValidaDeveSalvarContaCorrenteERetornarResponse()
        {
            // Arrange
            var request = new UpsertCCRequest
            {
                Id = null,
                Conta = "1234",
                Saldo = 100.0
            };

            var validatorResult = new ValidationResult();
            var contaEntity = new ContaCorrente { Id = Guid.NewGuid(), Conta = request.Conta, Saldo = request.Saldo };

            validatorMock.Setup(validator => validator.Validate(request)).Returns(validatorResult);
            contaCorrenteRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((ContaCorrente)null);
            contaCorrenteRepositoryMock.Setup(repo => repo.Save(It.IsAny<ContaCorrente>())).Returns(contaEntity);

            // Act
            var response = upsertCCHandler.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Errors);
            Assert.NotNull(response.Payload);
            Assert.Equal(request.Conta, response.Payload.Conta);
            Assert.Equal(request.Saldo, response.Payload.Saldo);
            Assert.Equal(contaEntity.Id.Value, response.Payload.Id);
        }

        [Fact]
        public void ExecuteComSolicitacaoInvalidaDeveRetornarRespostaDeErro()
        {
            // Arrange
            var request = new UpsertCCRequest();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Conta", "A conta é obrigatória."));
            validationResult.Errors.Add(new ValidationFailure("Saldo", "O saldo é obrigatório."));

            validatorMock.Setup(validator => validator.Validate(request)).Returns(validationResult);

            // Act
            var response = upsertCCHandler.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Errors);
            Assert.Null(response.Payload);
            Assert.Equal(2, response.Errors.Count);
            Assert.Equal("Conta", response.Errors[0].PropertyName);
            Assert.Equal("A conta é obrigatória.", response.Errors[0].ErrorMessage);
            Assert.Equal("Saldo", response.Errors[1].PropertyName);
            Assert.Equal("O saldo é obrigatório.", response.Errors[1].ErrorMessage);
        }

        [Fact]
        public void ExecuteComIdExistenteDeveLancarExcecao()
        {
            // Arrange
            var request = new UpsertCCRequest
            {
                Id = Guid.NewGuid(),
                Conta = "1234",
                Saldo = 100.0
            };

            var validatorResult = new ValidationResult();
            var contaEntity = new ContaCorrente { Id = request.Id, Conta = request.Conta, Saldo = request.Saldo };

            validatorMock.Setup(validator => validator.Validate(request)).Returns(validatorResult);
            contaCorrenteRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((ContaCorrente)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => upsertCCHandler.Execute(request));
            Assert.Equal("Conta não encontrada", exception.Message);
        }

        [Fact]
        public void ExecuteComContaExistenteDeveLancarExcecao()
        {
            // Arrange
            var request = new UpsertCCRequest
            {
                Id = Guid.NewGuid(),
                Conta = "1234",
                Saldo = 100.0
            };

            var validatorResult = new ValidationResult();
            var contaEntity = new ContaCorrente { Id = request.Id, Conta = request.Conta, Saldo = request.Saldo };

            validatorMock.Setup(validator => validator.Validate(request)).Returns(validatorResult);
            contaCorrenteRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(contaEntity);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => upsertCCHandler.Execute(request));
            Assert.Equal("Número da conta já existente", exception.Message);
        }

        [Fact]
        public void GetContasRetornaTodasAsContas()
        {
            // Arrange
            contaCorrenteRepositoryMock.Setup(repo => repo.GetAll()).Returns(contas);

            var handler = new GetAllCCHandler(contaCorrenteRepositoryMock.Object);

            // Act                                          
            var resultado = handler.Execute();

            // Assert
            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Payload);
            Assert.Equal(resultado.Payload.Count, 3);
        }

        [Fact]
        public void GetContasRetornaContaPorId()
        {
            // Arrange
            var contaId = Guid.Parse("34B3B9F6-9952-48CC-AB50-3CEA740F2F3C");
            var conta = contas.SingleOrDefault(c => c.Id == contaId);
            contaCorrenteRepositoryMock.Setup(repo => repo.GetById(contaId)).Returns(conta);
            var handler = new GetByIdCCHandler(contaCorrenteRepositoryMock.Object);
            var request = new GetByIdCCRequest
            {
                Id = contaId
            };

            // Act                                          
            var resultado = handler.Execute(request);

            // Assert
            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Payload);
            Assert.Equal(resultado.Payload.Id, contaId);
        }

        [Fact]
        public void GetContasRetornaContaPorNumeroConta()
        {
            // Arrange
            var numeroConta = "0001";
            var conta = contas.SingleOrDefault(c => c.Conta == numeroConta);
            contaCorrenteRepositoryMock.Setup(repo => repo.GetByConta(numeroConta)).Returns(conta);
            var handler = new GetByContaVerSaldoHandler(contaCorrenteRepositoryMock.Object);
            var request = new GetByContaVerSaldoRequest
            {
                Conta = numeroConta
            };

            // Act                                          
            var resultado = handler.Execute(request);

            // Assert
            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Payload);
            Assert.True(resultado.Payload.Saldo >= 0);
            Assert.Equal(resultado.Payload.Conta, numeroConta);
        }

        [Fact]
        public void GetContasRetornaErroQuandoIdNaoExiste()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            contaCorrenteRepositoryMock.Setup(repo => repo.GetById(contaId)).Returns((ContaCorrente)null);
            var handler = new GetByIdCCHandler(contaCorrenteRepositoryMock.Object);
            var request = new GetByIdCCRequest
            {
                Id = contaId
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => handler.Execute(request));
            Assert.Equal("Conta não encontrada", exception.Message);
        }

        [Fact]
        public void GetContasRetornaErroQuandoContaNaoExiste()
        {
            // Arrange
            var numeroConta = "9999";
            contaCorrenteRepositoryMock.Setup(repo => repo.GetByConta(numeroConta)).Returns((ContaCorrente)null);
            var handler = new GetByContaVerSaldoHandler(contaCorrenteRepositoryMock.Object);
            var request = new GetByContaVerSaldoRequest
            {
                Conta = numeroConta
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => handler.Execute(request));
            Assert.Equal("Conta não encontrada", exception.Message);
        }
    }
}
