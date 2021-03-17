using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_transver.DesignPattern
{
    public class BuilderDP
    {
        public class Contract
        {

        }

        public interface IBuilder
        {
            public void BuildTarif();
            public void BuildRisque();

            public Contract Contract();
        }

        public class MaMaisonBuilder : IBuilder
        {
            private Contract maMaison;

            public MaMaisonBuilder()
            {
                maMaison = new Contract();
            }

            public void BuildRisque()
            {
                throw new NotImplementedException();
            }

            public void BuildTarif()
            {
                throw new NotImplementedException();
            }

            public Contract Contract()
            {
                return maMaison;
            }
        }

        public class Factory
        {
            public void ContructContract(IBuilder builder)
            {
                builder.BuildRisque();
                builder.BuildTarif();
            }
        }
    }
}
