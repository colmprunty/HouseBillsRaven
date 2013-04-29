using NUnit.Framework;
using Raven.Client.Embedded;

//ReSharper disable InconsistentNaming
namespace HouseBillsTests
{
    [TestFixture]
    public class CreationTests
    {
        [Test]
        public void creating_a_debt_for_all_divides_by_the_TOTAL_number_of_users_and_gives_portion_of_debt_to_each()
        {
            var _store = new EmbeddableDocumentStore()
            {
                Configuration =
                {
                    RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true,
                    RunInMemory = true
                }
            };

        }
    }
}
