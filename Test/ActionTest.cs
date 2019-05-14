using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;

namespace Test
{
    [TestClass]
    public class ActionTest
    {
        [TestMethod]
        public void CreateAction()
        {
            var value = 1;
            var type = ActionType.Damage;
            var action = new Action(value, type);
            Assert.AreEqual(value, action.Value);
            Assert.AreEqual(type, action.Type);
        }
    }
}