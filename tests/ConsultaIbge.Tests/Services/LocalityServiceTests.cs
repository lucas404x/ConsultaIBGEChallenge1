using ConsultaIbge.Application.Dtos.Locality;
using ConsultaIbge.Application.Interfaces;
using ConsultaIbge.Application.Services;
using ConsultaIbge.Domain.Entities;
using ConsultaIbge.Domain.Exceptions.Locality;
using ConsultaIbge.Domain.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ConsultaIbge.Tests.Services;

public class LocalityServiceTests : IDisposable
{
    private const string LOCALITY_ID = "1234567";
    private const string LOCALITY_STATE = "STATE";
    private const string LOCALITY_CITY = "CITY";

    private readonly ILocalityRepository _localityRepositoryMock;
    private readonly ILocalityService _localityService;

    public LocalityServiceTests()
    {
        _localityRepositoryMock = Substitute.For<ILocalityRepository>();
        _localityService = new LocalityService(_localityRepositoryMock);
    }

    public void Dispose()
    {
        _localityRepositoryMock.Dispose();
    }

    [Fact]
    public async void UpdateLocality_IdDoesNotExist_ReturnsFail()
    {
        var dto = new LocalityUpdateDto(LOCALITY_ID, LOCALITY_STATE, LOCALITY_CITY);
        _localityRepositoryMock.ExistsAsync(dto.Id).Returns(false);
        await Assert.ThrowsAsync<LocalityNotFoundException>(() => _localityService.Update(dto));
        await _localityRepositoryMock.Received(1).ExistsAsync(dto.Id);
        _localityRepositoryMock.DidNotReceive().Update(Arg.Any<Locality>());
    }

    [Fact]
    public async void UpdateLocality_ValidId_ReturnsSuccess()
    {
        var dto = new LocalityUpdateDto(LOCALITY_ID, LOCALITY_STATE, LOCALITY_CITY);
        var localityFake = new Locality(LOCALITY_ID, LOCALITY_STATE, $"{LOCALITY_CITY}_1");

        _localityRepositoryMock.ExistsAsync(dto.Id).Returns(true);
        _localityRepositoryMock.When(x => x.Update(Arg.Any<Locality>())).Do(x => localityFake = x.Arg<Locality>());
        _localityRepositoryMock.UnitOfWork.CommitAsync().Returns(true);

        var result = await _localityService.Update(dto);

        Assert.Multiple(() =>
        {
            Assert.True(result);
            Assert.Equal(localityFake.Id, dto.Id);
            Assert.Equal(localityFake.City, dto.City);
            _localityRepositoryMock.Received(1).ExistsAsync(dto.Id);
            _localityRepositoryMock.Received(1).Update(Arg.Any<Locality>());
            // For some reason the above call to UnitOfWork counts as well,
            // so it's necessary include that call here.
            _localityRepositoryMock.Received(2).UnitOfWork.CommitAsync();
        });
    }

    [Fact]
    public async void RemoveLocality_IdDoesNotExist_ReturnsFail()
    {
        _localityRepositoryMock.GetByIdAsync(LOCALITY_ID).ReturnsNull();
        await Assert.ThrowsAsync<LocalityNotFoundException>(() => _localityService.Delete(LOCALITY_ID));
        await _localityRepositoryMock.Received(1).GetByIdAsync(LOCALITY_ID);
        _localityRepositoryMock.DidNotReceive().Delete(Arg.Any<Locality>());
    }

    [Fact]
    public async void RemoveLocality_ValidId_ReturnsSuccess()
    {
        var localityFake = new Locality(LOCALITY_ID, LOCALITY_STATE, LOCALITY_CITY);

        _localityRepositoryMock.GetByIdAsync(LOCALITY_ID).Returns(localityFake);
        _localityRepositoryMock.When(x => x.Delete(Arg.Any<Locality>())).Do(x => localityFake = null);
        _localityRepositoryMock.UnitOfWork.CommitAsync().Returns(true);

        var result = await _localityService.Delete(LOCALITY_ID);

        Assert.Multiple(() =>
        {
            Assert.True(result);
            Assert.Null(localityFake);
            _localityRepositoryMock.Received(1).GetByIdAsync(LOCALITY_ID);
            _localityRepositoryMock.Received(1).Delete(Arg.Any<Locality>());
            // For some reason the above call to UnitOfWork counts as well,
            // so it's necessary include that call here.
            _localityRepositoryMock.Received(2).UnitOfWork.CommitAsync();
        });
    }
}
