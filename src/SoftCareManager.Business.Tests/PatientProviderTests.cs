using FakeItEasy;
using SoftCareManager.Contracts.Services;
using SoftCareManager.TestUtilities;
using Xunit;
using Xunit.Extensions;

namespace MyFirstUnitTests
{
    public class Class1
    {
        [Theory]
        [AutoMockData]
        public void PassingTest(IPatientProvider patientProvider)
        {
            A.CallTo(() => patientProvider.GetPatients("naip:Berlin")).MustHaveHappened();
        }
    }
}