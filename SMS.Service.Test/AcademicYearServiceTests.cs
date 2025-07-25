using Microsoft.EntityFrameworkCore;
using Moq;
using SMS.API.Data;
using SMS.API.Services;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.Test
{
    public class AcademicYearServiceTests
    {
        private readonly AcademicYearService _academicYearService;
        private readonly Mock<ApplicationDbContext> _dbContextMock;
        private readonly Mock<DbSet<AcademicYear>> _dbSetMock;

        public AcademicYearServiceTests()
        {
            _dbSetMock = new Mock<DbSet<AcademicYear>>();
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _dbContextMock.Setup(x => x.AcademicYears).Returns(_dbSetMock.Object);
            _academicYearService = new AcademicYearService(_dbContextMock.Object);
        }

        [Fact]
        public async Task CreateAcademicYearAsync_ShouldAddAcademicYear()
        {
            var academicYear = new AcademicYear
            {
                AcademicYearId = 1,
                YearName = "2024-2025",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(10),
                IsCurrent = true,
                Description = "Test Year"
            };

            _dbSetMock.Setup(x => x.AddAsync(academicYear, default)).ReturnsAsync((AcademicYear ay, System.Threading.CancellationToken ct) => { return new Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AcademicYear>(null); });

            var result = await _academicYearService.CreateAcademicYearAsync(academicYear);

            _dbSetMock.Verify(x => x.AddAsync(academicYear, default), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
            Assert.Equal(academicYear, result);
        }

        [Fact]
        public async Task DeleteAcademicYearAsync_ShouldRemoveAcademicYear()
        {
            var academicYearId = 1;
            var academicYear = new AcademicYear { AcademicYearId = academicYearId };

            _dbSetMock.Setup(x => x.FindAsync(academicYearId)).ReturnsAsync(academicYear);

            var result = await _academicYearService.DeleteAcademicYearAsync(academicYearId);

            _dbSetMock.Verify(x => x.Remove(academicYear), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task GetAcademicYearByIdAsync_ShouldReturnAcademicYear()
        {
            var academicYearId = 1;
            var academicYear = new AcademicYear { AcademicYearId = academicYearId };

            _dbSetMock.Setup(x => x.FindAsync(academicYearId)).ReturnsAsync(academicYear);

            var result = await _academicYearService.GetAcademicYearByIdAsync(academicYearId);

            Assert.Equal(academicYear, result);
        }

        [Fact]
        public async Task GetAllAcademicYearsAsync_ShouldReturnPagedList()
        {
            var academicYears = new List<AcademicYear>
            {
                new AcademicYear { AcademicYearId = 1 },
                new AcademicYear { AcademicYearId = 2 }
            }.AsQueryable();

            _dbSetMock.As<IQueryable<AcademicYear>>().Setup(m => m.Provider).Returns(academicYears.Provider);
            _dbSetMock.As<IQueryable<AcademicYear>>().Setup(m => m.Expression).Returns(academicYears.Expression);
            _dbSetMock.As<IQueryable<AcademicYear>>().Setup(m => m.ElementType).Returns(academicYears.ElementType);
            _dbSetMock.As<IQueryable<AcademicYear>>().Setup(m => m.GetEnumerator()).Returns(academicYears.GetEnumerator());

            var result = await _academicYearService.GetAllAcademicYearsAsync(1, 10);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateAcademicYearAsync_ShouldUpdateAcademicYear()
        {
            var academicYearId = 1;
            var existingAcademicYear = new AcademicYear { AcademicYearId = academicYearId, YearName = "2023-2024" };
            var updatedAcademicYear = new AcademicYear { AcademicYearId = academicYearId, YearName = "2024-2025" };

            _dbSetMock.Setup(x => x.FindAsync(academicYearId)).ReturnsAsync(existingAcademicYear);

            var result = await _academicYearService.UpdateAcademicYearAsync(academicYearId, updatedAcademicYear);

            Assert.Equal(updatedAcademicYear.YearName, result.YearName);
            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}