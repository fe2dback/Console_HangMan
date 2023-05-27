using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_H
{
    class Score
    {
        public int result = 0;
        public static int bestscore = 0;
        public void score()
        {
            this.result++;
        }


        public void dead()
        {
            if (this.result > bestscore)
            {
                bestscore = this.result;
            }
            this.result = 0;    
        }


    }
}
