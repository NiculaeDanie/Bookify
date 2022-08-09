using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Design_Patterns
{
    public class BlobConvertor
    {
        private static BlobConvertor instance;
        private static readonly object padlock = new object();

        private BlobConvertor()
        {

        }
        public static BlobConvertor Instance 
        { 
            get 
            {
                if(instance == null)
                {
                    lock(padlock)
                    {
                        if(Instance == null)
                        {
                            instance = new BlobConvertor();
                        }    
                    }
                }
                return instance;
            } 
        }
    }
}
