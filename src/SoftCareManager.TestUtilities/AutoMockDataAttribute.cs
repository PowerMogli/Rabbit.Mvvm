using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Ploeh.AutoFixture.Xunit;

namespace SoftCareManager.TestUtilities
{
    public class AutoMockDataAttribute : AutoDataAttribute
    {
        public AutoMockDataAttribute()
            : base(new Fixture().Customize(new AutoFakeItEasyCustomization()))
        {

        }
    }
}