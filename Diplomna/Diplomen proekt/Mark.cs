using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomen_proekt
{
    public class Mark
    {
        public string TeacherEGN {get; set;}
        public string StudentEmail { get; set; }
        public string Writing { get; set; }
        public int TestPoint { get; set; }
        public string Markk { get; set; }
        public int WritingPoints { get; set; }
        public string TestTime { get; set; }

        public Mark(string teacherEgn, string studentemail, string writing,int testPoint ,string mark, int writingpoints, string testtime)
        {
            this.TeacherEGN = teacherEgn;
            this.StudentEmail = studentemail;
            this.Writing = writing;
            this.TestPoint = testPoint;
            this.Markk = mark;
            this.WritingPoints = writingpoints;
            this.TestTime = testtime;
        }
    }
}
