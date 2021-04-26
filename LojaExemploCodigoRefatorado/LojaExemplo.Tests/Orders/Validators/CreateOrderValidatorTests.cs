using AutoFixture;
using Bogus;
using FluentAssertions;
using LojaExemplo.Api.Core.Consts;
using LojaExemplo.Api.Orders.Commands;
using LojaExemplo.Api.Orders.Validators;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaExemplo.Tests.Orders.Validators
{
    public class CreateOrderValidatorTests
    {
        private CreateOrderValidator _createOrderValidator;
        private CreateOrderRequest _createOrderRequest;
        private readonly int _customerId = 1;
        private readonly string _zipCode = "86200000";
        private readonly string _promoCode = "10OFF";
        private readonly Fixture _fixture = new Fixture();
        private readonly Faker _faker = new Faker("pt-Br");

        [SetUp]
        public void Setup()
        {
            _createOrderValidator = new CreateOrderValidator();

            //Auto Fixture
            _createOrderRequest = _fixture.Create<CreateOrderRequest>();

            //Bogus
            _createOrderRequest = new CreateOrderRequest
            {
                ZipCode = _faker.Address.ZipCode("########")
            };

            //NBuilder
            //Criar massa de dados
            //https://nelson-souza.medium.com/nbuilder-e-faker-net-fb9546495e95

            _createOrderRequest = new CreateOrderRequest
            {
                CustomerId = _customerId,
                ZipCode = _zipCode,
                PromoCode = _promoCode,
                OrderProducts = new List<CreateOrderProductRequest>
                {
                    new CreateOrderProductRequest
                    {
                        Amount = 1,
                        ProductId = 1
                    }
                }
            };
        }

        [Test]
        public async Task CreateOrderValidator_ShouldFail()
        {
            // Arrange
            _createOrderRequest.ZipCode = "1";
            //_createOrderRequest.CustomerId = -1;

            // Act
            var validateResult = await _createOrderValidator.ValidateAsync(_createOrderRequest);

            // Assert
            Assert.AreEqual(false, validateResult.IsValid);
            validateResult.IsValid.Should().BeFalse();//Fluent Assertion - melhora legibilidade
        }


        [Test]
        public async Task CreateOrderValidator_ZipCodeInvalido_DeveValidar()
        {
            // Arrange
            _createOrderRequest.ZipCode = "31";
            //_createOrderRequest.CustomerId = -1;
            var errorMessageExpected = ValidationErrorMessageConst.InvalidZipcode;

            // Act
            var validateResult = await _createOrderValidator.ValidateAsync(_createOrderRequest);
            var errorMessage = validateResult.Errors.Select(error => error.ErrorMessage).First();

            // Assert
            validateResult.IsValid.Should().BeFalse();
            errorMessage.Should().Be(errorMessageExpected);
        }
    }
}
