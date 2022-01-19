using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Moq;
using Softdesign.Api.Infrastructure.Interfaces;
using Softdesign.Api.Infrastructure.Repositories;
using System.Collections.Generic;
using Softdesign.API.Domain.Models;
using Softdesign.Api.Infrastructure.Services;
using System.Linq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Threading.Tasks;

namespace Softdesign.Api.Test
{
    [TestClass]
    public class ApplicationTest
    {
        [TestMethod]
        [Fact]
        [Trait(nameof(IApplicationService), "Success")]
        public void GetApplications_Success()
        {
            var applicationRepoMock = new Mock<IApplicationRepository>();
            applicationRepoMock.Setup(repo => repo.Get()).ReturnsAsync(GetListApplication());

            var applications = new List<Application>()
            {
                new Application()
                {
                    ApplicationId = 1,
                    Url = "test.com",
                    PathLocal = "test",
                    DebuggingMode = true
                },
                new Application()
                {
                    ApplicationId = 2,
                    Url = "test2.com",
                    PathLocal = "test2",
                    DebuggingMode = false
                }
            };

            var handler = new ApplicationService(applicationRepoMock.Object);
            var applicationResult = handler.Get();

            Assert.IsTrue(applicationResult.Result.Count > 0);
            Assert.IsTrue(applicationResult.Result.FirstOrDefault().ApplicationId.Equals(applications.FirstOrDefault().ApplicationId));
            Assert.IsInstanceOfType(applicationResult, typeof(Task<List<Application>>));
        }

        [TestMethod]
        [Fact]
        [Trait(nameof(IApplicationService), "Success")]
        public void GetApplicationByID_Success()
        {
            var applicationRepoMock = new Mock<IApplicationRepository>();
            applicationRepoMock.Setup(repo => repo.GetById(1)).ReturnsAsync(GetApplication());

            var application = new Application()
            {
                ApplicationId = 1,
                Url = "test.com",
                PathLocal = "test",
                DebuggingMode = true
            };

            var handler = new ApplicationService(applicationRepoMock.Object);
            var applicationResult = handler.GetById(1);

            Assert.IsNotNull(applicationResult.Result.ApplicationId);
            Assert.IsTrue(applicationResult.Result.ApplicationId.Equals(application.ApplicationId));
            Assert.IsInstanceOfType(applicationResult, typeof(Task<Application>));
        }

        [TestMethod]
        [Fact]
        [Trait(nameof(IApplicationService), "Success")]
        public void PostApplication_Success()
        {
            var application = new Application()
            {
                ApplicationId = 1,
                Url = "test.com",
                PathLocal = "test",
                DebuggingMode = true
            };

            var applicationRepoMock = new Mock<IApplicationRepository>();
            applicationRepoMock.Setup(repo => repo.Post(application)).ReturnsAsync(true);

            var handler = new ApplicationService(applicationRepoMock.Object);
            var applicationResult = handler.Post(application);

            Assert.IsNotNull(applicationResult.Result.ToString());
            Assert.IsTrue(applicationResult.Result);
            Assert.IsInstanceOfType(applicationResult, typeof(Task<bool>));
        }

        [TestMethod]
        [Fact]
        [Trait(nameof(IApplicationService), "Success")]
        public void PathApplication_Success()
        {
            var application = new Application()
            {
                ApplicationId = 1,
                Url = "test.com",
                PathLocal = "test",
                DebuggingMode = true
            };

            var applicationRepoMock = new Mock<IApplicationRepository>();
            applicationRepoMock.Setup(repo => repo.Path(application)).ReturnsAsync(true);

            var handler = new ApplicationService(applicationRepoMock.Object);
            var applicationResult = handler.Path(application);

            Assert.IsNotNull(applicationResult.Result.ToString());
            Assert.IsTrue(applicationResult.Result);
            Assert.IsInstanceOfType(applicationResult, typeof(Task<bool>));
        }

        [TestMethod]
        [Fact]
        [Trait(nameof(IApplicationService), "Success")]
        public void DeleteApplication_Success()
        {
            var application = new Application()
            {
                ApplicationId = 1,
                Url = "test.com",
                PathLocal = "test",
                DebuggingMode = true
            };

            var applicationRepoMock = new Mock<IApplicationRepository>();
            applicationRepoMock.Setup(repo => repo.Delete(application.ApplicationId)).ReturnsAsync(true);

            var handler = new ApplicationService(applicationRepoMock.Object);
            var applicationResult = handler.Delete(application.ApplicationId);

            Assert.IsNotNull(applicationResult.Result.ToString());
            Assert.IsTrue(applicationResult.Result);
            Assert.IsInstanceOfType(applicationResult, typeof(Task<bool>));
        }

        private List<Application> GetListApplication()
        {
            var applications = new List<Application>()
            {
                new Application()
                {
                    ApplicationId = 1,
                    Url = "test.com",
                    PathLocal = "test",
                    DebuggingMode = true
                },
                new Application()
                {
                    ApplicationId = 2,
                    Url = "test2.com",
                    PathLocal = "test2",
                    DebuggingMode = false
                }
            };

            return applications;
        }

        private Application GetApplication()
        {
            var application = new Application()
            {
                ApplicationId = 1,
                Url = "test.com",
                PathLocal = "test",
                DebuggingMode = true
            };

            return application;
        }
    }
}
