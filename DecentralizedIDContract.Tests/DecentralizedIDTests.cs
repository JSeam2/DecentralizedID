using Moq;
using Stratis.SmartContracts;
using Stratis.SmartContracts.CLR;
using System;
using System.Reflection;
using Xunit;

// TODO
namespace DecentralizedIDContract.Tests
{
    public class DecentralizedIDTests
    {
        private readonly Mock<IContractLogger> mockContractLogger;
        private readonly Mock<ISmartContractState> mockContractState;
        private readonly Mock<IPersistentState> mockPersistentState;
        private readonly Mock<IInternalTransactionExecutor> mockInternalExecutor;

        public DecentralizedIDTests()
        {
            this.mockContractLogger = new Mock<IContractLogger>();
            this.mockContractState = new Mock<ISmartContractState>();
            this.mockPersistentState = new Mock<IPersistentState>();
            this.mockInternalExecutor = new Mock<IInternalTransactionExecutor>();

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
