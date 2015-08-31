using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Media;

namespace Diplomen_proekt
{
    public partial class Form1 : Form
    {
        const string DataSourse = "10.31.246.218";
        //регистрация за студент година
        public int b = 0;
        public int year = 1950;
        public int TestClickCount = 0;
        //точки от входен тест A1
        public int eA1points;

        //точки от входен тест A2
        public int eA2points;

        //точки от входен тест B1
        public int eB1points;

        //Списък с учителите в СУБД-то
        List<Teacher> teachers=new List<Teacher>();

        //Списък с ученици в СУБД-то
        List<Student> students = new List<Student>();

        //Списък с оценките които преподавател пише на даден ученик
        List<Mark> marks = new List<Mark>();

        //Списък с мениджърите в СУБД-то
        List<Manager> managers = new List<Manager>();

        //Масив с позициите на teachers който се използва при избора на преподавател за регистрацията на студент
        List<int> massPosInteachers = new List<int>();

        //Списък с верните отговори за теста на А2;
        List<String> ListA2WithCurAnswer = new List<string>();

        //Списък с верните отговори за теста на А1
        List<String> ListA1WithCurAnswer = new List<string>();

        //Списък с верните отговори за теста на B1
        List<String> ListB1WithCurAnswer = new List<string>();

        //Списък с верните отговори за теста на B2
        List<String> ListB2WithCurAnswer = new List<string>();

        //Списък с въпросите и отговорите на А2
        List<GrammarQuestion> GramarQuestionA2 = new List<GrammarQuestion>();

        //Списък с въпросите и отговорите на А1
        List<GrammarQuestion> GramarQuestionA1 = new List<GrammarQuestion>();

        //Списък с въпросите и отговорите на B1
        List<GrammarQuestion> GramarQuestionB1 = new List<GrammarQuestion>();

        //Списък с въпросите и отговорите на B2
        List<GrammarQuestion> GramarQuestionB2 = new List<GrammarQuestion>();

        public Form1()
        {
            InitializeComponent();
            content_container.SelectedTab = Home_page;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            teachers = select_Teacher();
            students = select_Student();
            managers = select_Manager();
            marks = select_Mark();
        }

        private void btn_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        

        private void btn_Reg_Log_Click(object sender, EventArgs e)
        {
            txt_email.Text = "E-mail";
            txt_pass.Text = "Password";
            content_container.SelectedTab = reg_log_page;
        }

        private void txt_email_MouseDown(object sender, MouseEventArgs e)
        {
            txt_email.Text = "";
        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {
            txt_email.ForeColor = System.Drawing.Color.Black;
        }

        private void txt_pass_MouseDown(object sender, MouseEventArgs e)
        {
            txt_pass.Text = "";
        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {
            txt_pass.ForeColor = System.Drawing.Color.Black;
            txt_pass.PasswordChar = '*';
        }

        private void btn_create_acc_Click(object sender, EventArgs e)
        {
            btn_Reg_Log.Enabled = false;
            btn_Contact.Enabled = false;
            btn_Teachers.Enabled = false;
            btn_Home.Enabled = false;
            content_container.SelectedTab = new_reg_page;
        }

        private void new_reg_panel_Enter(object sender, EventArgs e)
        {
            while (year<2015)
            {
                yearBirth.Items.Add(year.ToString());
                year++;
            }
        }

        private void btn_stAddPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pic_stNewRegPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                string[] checkPath=openFileDialog1.FileName.Split('.');
                if (checkPath[1]=="img" || checkPath[1]=="png" || checkPath[1] == "jpg")
                {
                    stImage_path_lb.Text = openFileDialog1.FileName;
                    pic_stNewRegPhoto.BackgroundImage = new Bitmap(stImage_path_lb.Text);   
                }
                else
                {
                    MessageBox.Show("Please enter picture !");
                }
                
            }
        }

        private void btn_stNewRegTest_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = entryA1V_page;
        }

        private void entryA1_page_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int eA1Vpoints=0;
            //Voccabulary
            if (eA1V_1_3.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_2_3.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_3_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_4_3.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_5_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_6_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_7_1.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_8_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_9_3.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_10_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_11_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_12_3.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_13_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_14_2.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };
            if (eA1V_15_1.Checked)
            { eA1Vpoints = eA1Vpoints + 2; };

           // MessageBox.Show(eA1Vpoints.ToString());

            eA1points = eA1points + eA1Vpoints;

            content_container.SelectedTab = entryA1G_page;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int eA1Gpoints = 0;

            if (eA1G_1_1.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_2_2.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_3_3.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_4_3.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_5_3.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_6_1.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_7_2.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_8_1.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_9_2.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_10_3.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_11_2.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_12_2.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_13_3.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_14_1.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };
            if (eA1G_15_2.Checked)
            { eA1Gpoints = eA1Gpoints + 2; };

            //MessageBox.Show(eA1Gpoints.ToString());

            eA1points = eA1points + eA1Gpoints;

            content_container.SelectedTab = entryA1R_page;
        }

        private void btn_eA1R_finish_Click(object sender, EventArgs e)
        {
            int eA1Rpoint = 0;
            if (eA1R_1.Text == "FALSE")
            {
                eA1Rpoint = eA1Rpoint + 2;
            }
            if (eA1R_2.Text == "FALSE")
            {
                eA1Rpoint = eA1Rpoint + 2;
            }
            if (eA1R_3.Text == "TRUE")
            {
                eA1Rpoint = eA1Rpoint + 2;
            }
            if (eA1R_4.Text == "TRUE")
            {
                eA1Rpoint = eA1Rpoint + 2;
            }
            if (eA1R_5.Text == "FALSE")
            {
                eA1Rpoint = eA1Rpoint + 2;
            }
            //MessageBox.Show(eA1Rpoint.ToString());
            

            eA1points = eA1points + eA1Rpoint;

            if (eA1points < 60)
            {
                content_container.SelectedTab = new_reg_page;
                btn_Teachers.Enabled = true;
                level_panel.Visible = true;
                stNewRegLevel.Text = "A1";
            }
            else
            {
                content_container.SelectedTab = entryA2V_page;
            }
        }

        private void btn_eA2V_next_Click(object sender, EventArgs e)
        {
            int eA2Vpoints = 0;
            //Voccabulary
            if (eA2V_1_3.Checked)
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_2_1.Checked)
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_3_2.Checked)    
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_4_3.Checked)     
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_5_1.Checked)      
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_6_2.Checked)      
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_7_3.Checked)      
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_8_1.Checked)      
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_9_2.Checked)      
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_10_1.Checked)    
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_11_1.Checked)    
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_12_3.Checked)     
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_13_1.Checked)     
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_14_1.Checked)     
                { eA2Vpoints = eA2Vpoints + 2; };
            if (eA2V_15_1.Checked)     
                { eA2Vpoints = eA2Vpoints + 2; };

            //MessageBox.Show(eA2Vpoints.ToString());

            eA2points = eA2points + eA2Vpoints;

            content_container.SelectedTab = entryA2G_page;
        }

        private void btn_eA2G_next_Click(object sender, EventArgs e)
        {
            int eA2Gpoints = 0;

            if (eA2G_1_2.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_2_3.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_3_1.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_4_3.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_5_2.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_6_1.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_7_3.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_8_3.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_9_2.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_10_1.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_11_3.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_12_1.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_13_2.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_14_1.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };
            if (eA2G_15_1.Checked)
            { eA2Gpoints = eA2Gpoints + 2; };

            //MessageBox.Show(eA2Gpoints.ToString());

            eA2points = eA2points + eA2Gpoints;

            content_container.SelectedTab = entryA2R_page;
        }

        private void btn_eA2R_finish_Click(object sender, EventArgs e)
        {
            int eA2Rpoint = 0;

            if (eA2R_1.Text == "TRUE")
            {
                eA2Rpoint = eA2Rpoint + 2;
            }
            if (eA2R_2.Text == "FALSE")
            {
                eA2Rpoint = eA2Rpoint + 2;
            }
            if (eA2R_3.Text == "TRUE")
            {
                eA2Rpoint = eA2Rpoint + 2;
            }
            if (eA2R_4.Text == "FALSE")
            {
                eA2Rpoint = eA2Rpoint + 2;
            }
            if (eA2R_5.Text == "FALSE")
            {
                eA2Rpoint = eA2Rpoint + 2;
            }

           // MessageBox.Show(eA2Rpoint.ToString());

            eA2points = eA2points + eA2Rpoint;
            if (eA2points < 60)
            {
                content_container.SelectedTab = new_reg_page;
                btn_Teachers.Enabled = true;
                level_panel.Visible = true;
                stNewRegLevel.Text = "A2";
            }
            else
            {
                content_container.SelectedTab = entryA2V_page;
            }
        }

        private void btn_eB1V_next_Click(object sender, EventArgs e)
        {
            int eB1Vpoints = 0;
            //Voccabulary
            if (eB1V_1_2.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_2_1.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_3_1.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_4_2.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_5_3.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_6_1.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_7_3.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_8_1.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_9_2.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_10_3.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_11_2.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_12_3.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_13_3.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_14_1.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };
            if (eB1V_15_2.Checked)
               { eB1Vpoints = eB1Vpoints + 2; };

            //MessageBox.Show(eB1Vpoints.ToString());

            eB1points = eB1points + eB1Vpoints;

            content_container.SelectedTab = entryB1G_page;
        }

        private void btn_eB1G_next_Click(object sender, EventArgs e)
        {
            int eB1Gpoints = 0;

            if (eB1G_1_1.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_2_2.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_3_1.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_4_2.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_5_3.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_6_3.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_7_1.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_8_3.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_9_1.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_10_3.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_11_1.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_12_3.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_13_1.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_14_1.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };
            if (eB1G_15_3.Checked)
               { eB1Gpoints = eB1Gpoints + 2; };

            //MessageBox.Show(eB1Gpoints.ToString());

            eB1points = eB1points + eB1Gpoints;

            content_container.SelectedTab = entryB1R_page;
        }

        private void btn_eB1R_finish_Click(object sender, EventArgs e)
        {
            int eB1Rpoint = 0;
                 
            if (eB1R_1.Text == "FALSE")
            {    
                eB1Rpoint = eB1Rpoint + 2;
            }    
            if (eB1R_2.Text == "FALSE")
            {    
                eB1Rpoint = eB1Rpoint + 2;
            }    
            if (eB1R_3.Text == "FALSE")
            {   
                eB1Rpoint = eB1Rpoint + 2;
            }    
            if (eB1R_4.Text == "TRUE")
            {    
                eB1Rpoint = eB1Rpoint + 2;
            }    
            if (eB1R_5.Text == "TRUE")
            {    
                eB1Rpoint = eB1Rpoint + 2;
            }

            //MessageBox.Show(eB1Rpoint.ToString());

            eB1points = eB1points + eB1Rpoint;
            
            content_container.SelectedTab = new_reg_page;
            btn_Teachers.Enabled = true;
            level_panel.Visible = true;
            stNewRegLevel.Text = "B1";
        }

        public bool OnlyNumer(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i])==false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool OnlyLetter(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i])==false)
                {
                    return false;
                }
            }
            return true;
        }

        private void btn_CreateAcc_Click(object sender, EventArgs e)
        {
            bool notFreeEm = true;
            foreach (var student in students)
            {
                if (student.Email == txt_stNewRegEmail.Text)
                {
                    notFreeEm = false;
                }
            }
            if (level_panel.Visible == false)
            {
                MessageBox.Show("First do the test to set your level");
            }
            else
            {
                string dataForInput = dayBirth.Text + "/" + (monthBirth.SelectedIndex + 1).ToString() + "/" + yearBirth.Text;
                DateTime result;
                bool success = DateTime.TryParse(dataForInput, out result);
                if (OnlyLetter(txt_stFirstName.Text) == false || OnlyLetter(txt_stLastName.Text) == false 
                   || OnlyNumer(txt_stPhoneNum.Text) == false || OnlyLetter(txt_stCity.Text) == false
                    || OnlyNumer(dayBirth.Text) == false || OnlyNumer(yearBirth.Text) == false || success == false)
                {
                    MessageBox.Show("Invalid information");
                }
                else if (validEmail(txt_stNewRegEmail.Text)==false)
                {
                    MessageBox.Show("Please enter valid email");
                }
                else if (txt_stFirstName.Text == "" || txt_stLastName.Text == ""
                   || txt_stPhoneNum.Text == "" || txt_stCity.Text == ""
                    || dayBirth.Text == "" || yearBirth.Text == ""||teach_choose.Text=="")
                {
                    MessageBox.Show("Please fill all fields");
                }
                else if (stImage_path_lb.Text=="")
                {
                    MessageBox.Show("Please add photo of yourself");
                }
                else if (notFreeEm == false)
                {
                    MessageBox.Show("There is another person with this email !");
                }
                else
                {
                    addStudent();
                    students = select_Student();
                    marks = select_Mark();
                    first_menu.Visible = false;
                    student_menu.Visible = true;
                    
                    //stud_pic.BackgroundImage = pic_stNewRegPhoto.Image;
                    stud_pic.BackgroundImage = Image.FromFile(stImage_path_lb.Text);
                    stud_name.Text = txt_stFirstName.Text;
                    stud_fam.Text = txt_stLastName.Text;
                    stud_email.Text = txt_stNewRegEmail.Text;
                    if (stNewRegLevel.Text == "A1")
                    {
                        content_container.SelectedTab = A1Modul1_page;
                    }
                    else if (stNewRegLevel.Text == "A2")
                    {
                        content_container.SelectedTab = A2Modul2_page;
                    }
                    else if (stNewRegLevel.Text == "B1")
                    {
                        content_container.SelectedTab = B1Modul1_page;
                    }
                    else if (stNewRegLevel.Text == "B2")
                    {
                        content_container.SelectedTab = B2Modul1_page;
                    }
                    foreach (var mark in marks)
                    {
                        if (mark.StudentEmail == txt_stNewRegEmail.Text)
                        {
                            if (mark.TestTime == "True")
                            {
                                btn_Test.Enabled = true;
                            }
                            else
                            {
                                btn_Test.Enabled = false;
                            }
                            break;
                        }
                    }
                    pic_stNewRegPhoto.BackgroundImage = null;
                    txt_stFirstName.Text = "";
                    txt_stLastName.Text = "";
                    txt_stNewRegEmail.Text = "";
                    txt_stNewRegPass.Text = "";
                    txt_stCity.Text = "";
                    txt_stPhoneNum.Text = "";
                    dayBirth.Text = "Day";
                    monthBirth.Text = "Month";
                    yearBirth.Text = "Year";
                    btn_stNewRegTest.Enabled = true;
                    level_panel.Visible = false;
                    stNewRegLevel.Text = "";
                    teach_choose.Text = "";
                    TestClickCount--;
                    teach_choose.Controls.Clear();
                }
                
            }
        }

        private bool validEmail(string email)
        {
            for (int i = 0; i < email.Length; i++)
            {
                if (email[i]=='@')
                {
                    if (i==0)
                    {
                        return false;
                    }
                    else 
                    {
                        if (i==email.Length-7&&email[i+1]=='a'&&email[i+2]=='b'&&email[i+3]=='v'&&email[i+4]=='.'&&email[i+5]=='b'&&email[i+6]=='g')
                        {
                            return true;
                        }
                        else if (i == email.Length - 10 && email[i + 1] == 'g' && email[i + 2] == 'm' && email[i + 3] == 'a' && email[i + 4] == 'i' && email[i + 5] == 'l' && email[i + 6] == '.' && email[i + 7] == 'c' && email[i + 8] == 'o' && email[i + 9] == 'm')
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void new_reg_page_Enter(object sender, EventArgs e)
        {
            if (TestClickCount == 1)
            {
                btn_stNewRegTest.Enabled = false;
                TestClickCount = 0;
            }
            TestClickCount++;
        }

        public void addStudent()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {
                            FileStream file = new FileStream(stImage_path_lb.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            byte[] image = new byte[file.Length];
                            file.Read(image, 0, Convert.ToInt32(file.Length));
                            file.Close();
                            string birthDay = yearBirth.Text + "-" + (monthBirth.SelectedIndex + 1) + "-" + dayBirth.Text + " 12:00";
                            string query = "INSERT INTO dbo.Student (Name, Family, email, password, city, phone, stdlevel, photo, birthday) VALUES('" + txt_stFirstName.Text + "','" + txt_stLastName.Text + "','" + txt_stNewRegEmail.Text + "','" + txt_stNewRegPass.Text + "','" + txt_stCity.Text + "','" + txt_stPhoneNum.Text + "','" + stNewRegLevel.Text + "',@pic ,'" + birthDay + "')";
                            SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Students (Name, Family, email, password, city, phone, stdlevel, photo, birthday) VALUES('" + txt_stFirstName.Text + "','" + txt_stLastName.Text + "','" + txt_stNewRegEmail.Text + "','" + txt_stNewRegPass.Text + "','" + txt_stCity.Text + "','" + txt_stPhoneNum.Text + "','" + stNewRegLevel.Text + "',@pic ,'" + birthDay + "')", con);
                            SqlParameter param = new SqlParameter("@pic", SqlDbType.VarBinary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
                            cmd.Parameters.Add(param);
                            cmd.ExecuteNonQuery();

                            string teachEgn = teachers[massPosInteachers[teach_choose.SelectedIndex]].EGN;
                            SqlCommand cmd2 = new SqlCommand("INSERT INTO dbo.Marks (TeacherEGN, StudentEmail,writing,testPoint ,mark, writingPoints, TestTime) VALUES('" + teachEgn + "','" + txt_stNewRegEmail.Text + "','','','','',NULL)", con);
                            cmd2.ExecuteNonQuery();

                            //SqlCommand cmd3 = new SqlCommand("DELETE from dbo.Marks WHERE TeacherEGN='3333333333'",con);
                            //cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        List<Teacher> select_Teacher()
        {
            List<Teacher> result = new List<Teacher>();
           
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source="+DataSourse+",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.Teachers;", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                byte[] myData = new byte[0];
                                int br = dt.Rows.Count;
                                myData = (byte[])dt.Rows[i][4];
                                Teacher teach = new Teacher(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][6].ToString(), myData, dt.Rows[i][3].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][7].ToString());
                                result.Add(teach);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");
                            
                        }
                        con.Close();
                        return result;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return result; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        List<Manager> select_Manager()
        {
            List<Manager> result = new List<Manager>();

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.Managers;", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                byte[] myData = new byte[0];
                                int br = dt.Rows.Count;
                                myData = (byte[])dt.Rows[i][3];
                                Manager manager = new Manager(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), myData, dt.Rows[i][4].ToString());
                                result.Add(manager);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                        return result;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        List<Student> select_Student()
        {
            List<Student> result = new List<Student>();

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.Students;", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                byte[] myData = new byte[0];
                                int br = dt.Rows.Count;
                                myData = (byte[])dt.Rows[i][7];
                                Student stud = new Student(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), myData, dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][8].ToString());
                                result.Add(stud);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                        return result;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        List<Mark> select_Mark()
        {
            List<Mark> result = new List<Mark>();

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.Marks;", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int writingPoints;
                                if (dt.Rows[i][5].ToString() == "")
                                {
                                    writingPoints = 0;
                                }
                                else
                                {
                                    writingPoints = int.Parse(dt.Rows[i][5].ToString());
                                }
                                int testPoint;
                                if (dt.Rows[i][3].ToString() == "")
                                {
                                    testPoint = 0;
                                }
                                else
                                {
                                    testPoint = int.Parse(dt.Rows[i][3].ToString());
                                }
                                Mark mark = new Mark(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), testPoint, dt.Rows[i][4].ToString(), writingPoints, dt.Rows[i][6].ToString());
                                result.Add(mark);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                        return result;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btn_Teachers_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = viewTeacher_page;
            teachers_container.Controls.Clear();
            if (btn_Reg_Log.Enabled == false)
            {
                btn_backToReg.Visible = true;
            }
            foreach (var teacher in teachers)
            {
                Button but = new Button();
                but.Name = teacher.EGN;
                but.Text = teacher.Name+" "+teacher.Family;
                but.AutoSize = true;
                but.Click += but_Click;
                teachers_container.Controls.Add(but);
            }
        }

        void but_Click(object sender, EventArgs e)
        {
            Button but = sender as Button;
            foreach (var teacher in teachers)
            {
                if (teacher.EGN == but.Name)
                {
                    MemoryStream str = new MemoryStream(teacher.Photo);
                    tch_photo.BackgroundImage = Image.FromStream(str);
                    tch_name.Text = teacher.Name;
                    tch_fam.Text = teacher.Family;
                    tch_level.Text = teacher.Leveltch;
                    tch_quali.Text = teacher.Qualification;
                    tch_email.Text = teacher.Email;



                    try
                    {
                        using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                        "Network Library=DBMSSOCN;" +
                        "Initial Catalog=DiplomnaDB;" +
                        "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                        {
                            try
                            {
                                con.Open();
                                if (con.State == ConnectionState.Open)
                                {

                                    SqlDataAdapter sda = new SqlDataAdapter("SELECT workDay,workHour FROM dbo.WorkTime WHERE TeacherEGN = '" + teacher.EGN + "';", con);
                                    DataTable dt = new DataTable();
                                    sda.Fill(dt);
                                    tch_dayless.Text = dt.Rows[0][0].ToString();
                                    tch_hourless.Text = dt.Rows[0][1].ToString();
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Ne se polu4ava");

                                }
                                con.Close();
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                }
            }
        }
        
        private void button1_Click_2(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tch_name.Text = openFileDialog1.FileName;
                students = select_Student();
            }
            
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            foreach (var teacher in teachers)
            {
                if (teacher.Email == txt_email.Text && teacher.EGN == txt_pass.Text)
                {
                    if (teacher.Leveltch == "A1")
                    {
                        content_container.SelectedTab = A1Modul1_page;
                    }
                    if (teacher.Leveltch == "A2")
                    {
                        content_container.SelectedTab = A2Modul1_page;
                    }
                    if (teacher.Leveltch == "B1")
                    {
                        content_container.SelectedTab = B1Modul1_page;
                    }
                    if (teacher.Leveltch == "B2")
                    {
                        content_container.SelectedTab = B2Modul1_page;
                    }
                    first_menu.Visible = false;
                    teacher_menu.Visible = true;
                    MemoryStream str = new MemoryStream(teacher.Photo);
                    teach_pic.BackgroundImage = Image.FromStream(str);
                    teach_name.Text = teacher.Name;
                    teach_fam.Text = teacher.Family;
                    teach_egn.Text = teacher.EGN;
                    break;
                }
            }
            foreach (var student in students)
            {
                if (student.Email == txt_email.Text && student.Password == txt_pass.Text)
                {
                    first_menu.Visible = false;
                    student_menu.Visible = true;
                    MemoryStream str = new MemoryStream(student.Photo);
                    stud_pic.BackgroundImage = Image.FromStream(str);
                    stud_name.Text = student.Name;
                    stud_fam.Text = student.Family;
                    stud_email.Text = student.Email;
                    if (student.Level == "A1")
	                {
		                content_container.SelectedTab = A1Modul1_page;
	                }
                    if (student.Level == "A2")
	                {
		                content_container.SelectedTab = A2Modul1_page;
	                }
                    if (student.Level == "B1")
	                {
		                content_container.SelectedTab = B1Modul1_page;
	                }
                    if (student.Level == "B2")
	                {
		                content_container.SelectedTab = B2Modul1_page;
	                }
                    marks = select_Mark();
                    foreach (var mark in marks)
                    {
                        if (mark.StudentEmail == student.Email)
                        {
                            if (mark.TestTime == "True")
                            {
                                btn_Test.Enabled = true;
                            }
                            else
                            {
                                btn_Test.Enabled = false;
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            foreach (var manager in managers)
            {
                if (manager.Email == txt_email.Text && manager.Password == txt_pass.Text)
                {
                    content_container.SelectedTab = wellcomeManag_page;
                    welcManName_lb.Text = manager.Name + " " + manager.Family;
                    first_menu.Visible = false;
                    maneger_menu.Visible = true;
                    MemoryStream str = new MemoryStream(manager.Photo);
                    maneg_pic.BackgroundImage = Image.FromStream(str);
                    maneg_name.Text = manager.Name;
                    maneg_family.Text = manager.Family;
                    maneg_email.Text = manager.Email;
                    break;
                }
            }

        }

        private void stNewRegLevel_TextChanged(object sender, EventArgs e)
        {
            int posInteachers = 0;
            foreach (var teacher in teachers)
            {
                if (stNewRegLevel.Text == teacher.Leveltch)
                {
                    teach_choose.Items.Add(teacher.Name + " " + teacher.Family);
                    massPosInteachers.Add(posInteachers);
                }
                posInteachers++;
            }
        }

        private void btn_students_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = TeacherStudView_page;
            Student_conteinter.Controls.Clear();
            writingcheck_conteinter.Controls.Clear();
            marks = select_Mark();
            int stdbr= 0;
            foreach (var stud in marks)
            {
                if (stud.TeacherEGN == teach_egn.Text)
                {
                    foreach (var student in students)
                    {
                        if (student.Email == stud.StudentEmail)
                        {
                            Button stdbut = new Button();
                            stdbut.Name = stud.StudentEmail;
                            stdbut.Text = student.Name + " " + student.Family;
                            stdbut.AutoSize = true;
                            stdbut.Click += stdbut_Click;
                            stdbut.Font = new Font("Georgia", 15);
                            Student_conteinter.Controls.Add(stdbut);

                            Button stdwrit = new Button();
                            stdwrit.Name = "std" + stdbr;
                            stdwrit.AutoSize = true;
                            stdwrit.Font = new Font("Georgia", 15);
                            
                            if (stud.Writing == "")
                            {
                                stdwrit.ForeColor = Color.Red;
                                stdwrit.Text = "NOT READY";
                            }
                            else
                            {
                                stdwrit.ForeColor = Color.LawnGreen;
                                stdwrit.Text = "READY";
                            }

                            

                            stdbr++;
                            writingcheck_conteinter.Controls.Add(stdwrit);
                        }
                    }
                }
            }
        }

        void stdbut_Click(object sender, EventArgs e)
        {
            Button stdbut = sender as Button;
            foreach (var student in students)
            {
                if (stdbut.Name == student.Email)
                {
                    MemoryStream str = new MemoryStream(student.Photo);
                    student_pic.BackgroundImage = Image.FromStream(str);

                    student_Name.Text = student.Name;
                    student_Family.Text = student.Family;
                    student_city.Text = student.City;
                    student_email.Text = student.Email;
                    student_phone.Text = student.Phone;
                    foreach (var writing in marks)
                    {
                        if (writing.StudentEmail == student.Email)
                        {
                            writingText.Text = writing.Writing;
                            if (writing.Markk == "")
                            {
                                studMarkVis.Text = "";
                            }
                            else
                            {
                                studMarkVis.Text = writing.Markk;
                            }
                            break;
                        }
                        
                    }
                    
                    break;
                }
            }
        }

        public void addWritingPoints(string studentEmail,int points)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {
                            string stlevel="";
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT testPoint FROM dbo.Marks WHERE StudentEmail = '" + studentEmail + "';", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            foreach (var student in students)
	                        {
		                        if (student.Email == studentEmail)
	                            {
	                                	stlevel = student.Level; 
	                            }
	                        }
                            int pointFromTest = int.Parse(dt.Rows[0][0].ToString());
                            int result = points + pointFromTest;
                            string studMark = "";
                            if (stlevel == "A1" || stlevel == "A2")
                            {
                                if (result<15)
                                {
                                    studMark = "F";
                                }
                                if (result>=15 && result<17)
                                {
                                    studMark = "E";
                                }
                                if (result>=17 && result<20)
                                {
                                    studMark = "C";
                                }
                                if (result>=20 && result<25)
                                {
                                    studMark = "B";
                                }
                                if (result>=25 && result<28)
                                {
                                    studMark = "A";
                                }
                            }
                            if (stlevel == "B1" || stlevel == "B2")
                            {
                                if (result < 19)
                                {
                                    studMark = "F";
                                }
                                if (result >= 19 && result < 22)
                                {
                                    studMark = "E";
                                }
                                if (result >= 22 && result < 27)
                                {
                                    studMark = "C";
                                }
                                if (result >= 27 && result < 33)
                                {
                                    studMark = "B";
                                }
                                if (result >= 33 && result < 38)
                                {
                                    studMark = "A";
                                }
                            }

                            SqlCommand cmd = new SqlCommand("UPDATE dbo.Marks SET writingPoints='" + points.ToString() + "',mark='" + studMark + "' WHERE StudentEmail='" + studentEmail + "';", con);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_enterPoints_Click(object sender, EventArgs e)
        {
            int integer;
            if (int.TryParse(studWpoints_thx.Text,out integer)&& int.Parse(studWpoints_thx.Text) < 11)
            {
                addWritingPoints(student_email.Text, int.Parse(studWpoints_thx.Text));
                marks = select_Mark();
            }
            else
            {
                MessageBox.Show("Please enter numbers not letters and the number must be between 0 and 10");
                studWpoints_thx.Text = "";
            }
        }

        private void btn_maneger_studview_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = managerStudview_page;
            mstudcontein_A1.Controls.Clear();
            mstudcontein_A1del.Controls.Clear();
            mstudcontein_A2.Controls.Clear();
            mstudcontein_A2del.Controls.Clear();
            mstudcontein_B1.Controls.Clear();
            mstudcontein_B1del.Controls.Clear();
            mstudcontein_B2.Controls.Clear();
            mstudcontein_B2del.Controls.Clear();
            
            foreach (var student in students)
            {
                if (student.Level == "A1")
                {
                    Button mngdbut = new Button();
                    mngdbut.Name = student.Email + ".";
                    mngdbut.Text = student.Name + " " + student.Family;
                    mngdbut.AutoSize = true;
                    mngdbut.Font = new Font("Georgia", 15);
                    mstudcontein_A1.Controls.Add(mngdbut);
                    mngdbut.MouseHover += mngdbut_MouseHover;

                    Button del = new Button();
                    del.Name = student.Email;
                    del.AutoSize = true;
                    del.Text = "DELETE";
                    del.BackColor = Color.Red;
                    del.ForeColor = Color.Black;
                    del.Font = new Font("Georgia", 15);
                    del.Click += del_Click;
                    mstudcontein_A1del.Controls.Add(del);
                }
                else if (student.Level == "A2")
                {
                    Button mngdbut = new Button();
                    mngdbut.Name = student.Email + ".";
                    mngdbut.Text = student.Name + " " + student.Family;
                    mngdbut.AutoSize = true;
                    mngdbut.Font = new Font("Georgia", 15);
                    mngdbut.MouseHover += mngdbut_MouseHover;
                    mstudcontein_A2.Controls.Add(mngdbut);

                    Button del = new Button();
                    del.Name = student.Email;
                    del.AutoSize = true;
                    del.Text = "DELETE";
                    del.BackColor = Color.Red;
                    del.ForeColor = Color.Black;
                    del.Font = new Font("Georgia", 15);
                    del.Click += del_Click;
                    mstudcontein_A2del.Controls.Add(del);
                }
                else if (student.Level == "B1")
                {
                    Button mngdbut = new Button();
                    mngdbut.Name = student.Email + ".";
                    mngdbut.Text = student.Name + " " + student.Family;
                    mngdbut.AutoSize = true;
                    mngdbut.Font = new Font("Georgia", 15);
                    mngdbut.MouseHover += mngdbut_MouseHover;
                    mstudcontein_B1.Controls.Add(mngdbut);

                    Button del = new Button();
                    del.Name = student.Email;
                    del.AutoSize = true;
                    del.Text = "DELETE";
                    del.BackColor = Color.Red;
                    del.ForeColor = Color.Black;
                    del.Font = new Font("Georgia", 15);
                    del.Click += del_Click;
                    mstudcontein_B1del.Controls.Add(del);
                }
                else if (student.Level == "B2")
                {
                    Button mngdbut = new Button();
                    mngdbut.Name = student.Email + ".";
                    mngdbut.Text = student.Name + " " + student.Family;
                    mngdbut.AutoSize = true;
                    mngdbut.Font = new Font("Georgia", 15);
                    mngdbut.MouseHover += mngdbut_MouseHover;
                    mstudcontein_B2.Controls.Add(mngdbut);

                    Button del = new Button();
                    del.Name = student.Email;
                    del.AutoSize = true;
                    del.Text = "DELETE";
                    del.BackColor = Color.Red;
                    del.ForeColor = Color.Black;
                    del.Font = new Font("Georgia", 15);
                    del.Click += del_Click;
                    mstudcontein_B2del.Controls.Add(del);
                }
            }
        }

        void mngdbut_MouseHover(object sender, EventArgs e)
        {
            Button mngdbut = sender as Button;
            toolTip1.Show(mngdbut.Name, mngdbut);
        }

        void del_Click(object sender, EventArgs e)
        {
            Button delA1 = sender as Button;
            foreach (var stud in students)
            {
                if (delA1.Name == stud.Email)
                {
                    DeleteStudent(stud.Email);
                    students = select_Student();
                    marks = select_Mark();
                    btn_maneger_studview.PerformClick();
                }
            }
        }

        void delA1_Click(object sender, EventArgs e)
        {
           
        }

        public void DeleteStudent(string studentEmail)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {
                            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Students WHERE email='" + studentEmail + "';", con);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                        btn_maneger_studview.PerformClick();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_maneg_teacview_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = managerTeachview_page;
            mfirstlb.Controls.Clear();
            mTeach_container.Controls.Clear();
            mappDate_container.Controls.Clear();
            mManag_containter.Controls.Clear();
            mdelteach_container.Controls.Clear();

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT EGN,dbo.Teachers.Name,dbo.Teachers.Family,dbo.Managers.Name,dbo.Managers.Family,appointDate FROM dbo.Managers,dbo.Teachers,dbo.WorkTime WHERE dbo.WorkTime.TeacherEGN=dbo.Teachers.EGN and dbo.WorkTime.ManagerEmail=dbo.Managers.email;", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Label flb = new Label();
                                flb.Text = "Преподавателят ";
                                flb.AutoSize = true;
                                flb.ForeColor = Color.White;
                                flb.Font = new Font("Georgia", 15);
                                mfirstlb.Controls.Add(flb);

                                Label thclb = new Label();
                                thclb.Text = dt.Rows[i][1]+" "+dt.Rows[i][2];
                                thclb.AutoSize = true;
                                thclb.ForeColor = Color.White;
                                thclb.Font = new Font("Georgia", 15);
                                mTeach_container.Controls.Add(thclb);

                                Label appdate = new Label();
                                appdate.Text = "е нает на "+dt.Rows[i][5]+" от ";
                                appdate.AutoSize = true;
                                appdate.ForeColor = Color.White;
                                appdate.Font = new Font("Georgia", 15);
                                mappDate_container.Controls.Add(appdate);

                                Label manlb = new Label();
                                manlb.Text = dt.Rows[i][3]+" "+dt.Rows[i][4];
                                manlb.AutoSize = true;
                                manlb.ForeColor = Color.White;
                                manlb.Font = new Font("Georgia", 15);
                                mManag_containter.Controls.Add(manlb);

                                Label mdelteach = new Label();
                                mdelteach.Name = dt.Rows[i][0].ToString();
                                mdelteach.Text = "Click to Delete";
                                mdelteach.AutoSize = true;
                                mdelteach.ForeColor = Color.Red;
                                mdelteach.Font = new Font("Georgia", 15);
                                mdelteach_container.Controls.Add(mdelteach);
                                mdelteach.Click += mdelteach_Click;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void mdelteach_Click(object sender, EventArgs e)
        {
            Label mdelteach = sender as Label;
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {
                            string n=mdelteach.Name;
                            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Teachers WHERE EGN='" + mdelteach.Name + "';", con);
                            cmd.ExecuteNonQuery();
                            
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                        btn_maneg_teacview.PerformClick();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            teachers = select_Teacher();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            content_container.SelectedTab = addTeacher_page;
            LevelsOfTeach.Items.Clear();
            hoursOfTeach_cmb.Items.Clear();
            dayOfTeach_cmb.Items.Clear();

            LevelsOfTeach.Items.Add("A1");
            LevelsOfTeach.Items.Add("A2");
            LevelsOfTeach.Items.Add("B1");
            LevelsOfTeach.Items.Add("B2");

            hoursOfTeach_cmb.Items.Add("8");
            hoursOfTeach_cmb.Items.Add("10");
            hoursOfTeach_cmb.Items.Add("12");
            hoursOfTeach_cmb.Items.Add("14");
            hoursOfTeach_cmb.Items.Add("16");

            dayOfTeach_cmb.Items.Add("Monday");
            dayOfTeach_cmb.Items.Add("Tuesday");
            dayOfTeach_cmb.Items.Add("Thursday");
            dayOfTeach_cmb.Items.Add("Wednesday");
            dayOfTeach_cmb.Items.Add("Friday");
            dayOfTeach_cmb.Items.Add("Saturday");
            dayOfTeach_cmb.Items.Add("Sunday");
        }

        private void btn_addPicTeach_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] checkPath = openFileDialog1.FileName.Split('.');
                if (checkPath[1] == "img" || checkPath[1] == "png" || checkPath[1] == "jpg")
                {
                    teachImagePath_lb.Text = openFileDialog1.FileName;
                    addTeach_pic.BackgroundImage = new Bitmap(teachImagePath_lb.Text);
                }
                else
                {
                    MessageBox.Show("Please enter picture !");
                }
                

            }
        }

        bool SalaryCheck(string text)
        {
            int brSym = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]) == false)
                {
                    if (text[i] == '$' && brSym < 1)
                    {
                        brSym++;
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }

        private void btn_addTeacher_Click(object sender, EventArgs e)
        {
            string todayDate = DateTime.Today.ToString().Substring(0, 9);
            if (teachImagePath_lb.Text == "" || LevelsOfTeach.Text == "" || addtchEgn_txt.Text =="" || addtchEmail_txt.Text=="" || addtchName_txt.Text =="" 
                || addtchFam_txt.Text =="" || dayOfTeach_cmb.Text=="" || hoursOfTeach_cmb.Text==""||addtchsal_txt.Text =="")
            {
                MessageBox.Show("Моля попълнете всички полета");
            }
            else if (OnlyNumer(addtchEgn_txt.Text) == false || OnlyLetter(addtchName_txt.Text) == false
                || OnlyLetter(addtchFam_txt.Text) == false || SalaryCheck(addtchsal_txt.Text) == false)
            {
                MessageBox.Show("Невалидна информация в полетата");
            }
            else if (validEmail(addtchEmail_txt.Text)==false)
            {
                MessageBox.Show("Моля въведете съществуващ email");
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                    "Network Library=DBMSSOCN;" +
                    "Initial Catalog=DiplomnaDB;" +
                    "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                    {
                        try
                        {
                            con.Open();
                            if (con.State == ConnectionState.Open)
                            {
                                SqlCommand check = new SqlCommand();
                                SqlDataAdapter sda = new SqlDataAdapter("SELECT workHour,workDay FROM dbo.WorkTime", con);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() == hoursOfTeach_cmb.Text && dt.Rows[i][1].ToString() == dayOfTeach_cmb.Text)
                                    {
                                        throw new System.InvalidOperationException("Друг преподавател има час в зададените ден и час");
                                    }
                                }
                                FileStream file = new FileStream(teachImagePath_lb.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                byte[] image = new byte[file.Length];
                                file.Read(image, 0, Convert.ToInt32(file.Length));
                                file.Close();
                                string query = "INSERT INTO dbo.Teachers(EGN, Name, Family, LevelTc, photo, qualification, email, salary) VALUES('" + addtchEgn_txt.Text + "','" + addtchName_txt.Text + "','" + addtchFam_txt.Text + "','" + LevelsOfTeach.Text + "',@pic,'" + addtchqual_txt.Text + "','" + addtchEmail_txt.Text + "','" + addtchsal_txt.Text + "')";
                                SqlCommand cmd = new SqlCommand(query, con);
                                SqlParameter param = new SqlParameter("@pic", SqlDbType.VarBinary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
                                cmd.Parameters.Add(param);
                                cmd.ExecuteNonQuery();

                                SqlCommand cmd2 = new SqlCommand("INSERT INTO dbo.WorkTime (TeacherEGN, ManagerEmail,workHour, workDay, appointDate) VALUES('" + addtchEgn_txt.Text + "','" + maneg_email.Text + "','" + hoursOfTeach_cmb.Text + "','" + dayOfTeach_cmb.Text + "','" + todayDate + "')", con);
                                cmd2.ExecuteNonQuery();

                                teachers = select_Teacher();
                                teachImagePath_lb.Text = "";
                                LevelsOfTeach.Text = "";
                                addtchEgn_txt.Text = "";
                                addtchEmail_txt.Text = "";
                                addtchName_txt.Text = "";
                                addtchFam_txt.Text = "";
                                dayOfTeach_cmb.Text = "";
                                hoursOfTeach_cmb.Text = "";
                                addtchqual_txt.Text = "";
                                addtchsal_txt.Text = "";
                                addTeach_pic.BackgroundImage = null;
                                addTeach_pic.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Ne se polu4ava");

                            }
                            con.Close();
                            btn_maneg_teacview.PerformClick();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        catch (InvalidOperationException ex1)
                        {
                            MessageBox.Show(ex1.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_maneg_prog_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = managerProgram_page;
            string [,] table= new string [7,5];
            
            List<FlowLayoutPanel> flowPanel = new List<FlowLayoutPanel>();
            flowPanel.Add(mprog8_conteiner);
            flowPanel.Add(mprog10_conteiner);
            flowPanel.Add(mprog12_conteiner);
            flowPanel.Add(mprog14_conteiner);
            flowPanel.Add(mprog16_conteiner);
            for (int i = 0; i < flowPanel.Count; i++)
            {
                flowPanel[i].Controls.Clear();
            }
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT Name,Family,workHour,workDay FROM dbo.Teachers,dbo.WorkTime WHERE EGN = TeacherEGN;", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            int i = 0;
                            int j = 0;
                            for (int temp = 0; temp < dt.Rows.Count; temp++)
                            {
                                if (int.Parse(dt.Rows[temp][2].ToString()) == 8)
                                {
                                    j = 0;
                                }
                                else if (int.Parse(dt.Rows[temp][2].ToString()) == 10)
                                {
                                    j = 1;
                                }
                                else if (int.Parse(dt.Rows[temp][2].ToString()) == 12)
                                {
                                    j = 2;
                                }
                                else if (int.Parse(dt.Rows[temp][2].ToString()) == 14)
                                {
                                    j = 3;
                                }
                                else if (int.Parse(dt.Rows[temp][2].ToString()) == 16)
                                {
                                    j = 4;
                                }

                                if (dt.Rows[temp][3].ToString() == "Monday")
                                {
                                    i = 0;
                                }
                                if (dt.Rows[temp][3].ToString() == "Tuesday")
                                {
                                    i = 1;
                                }
                                if (dt.Rows[temp][3].ToString() == "Wednesday")
                                {
                                    i = 2;
                                }
                                if (dt.Rows[temp][3].ToString() == "Thursday")
                                {
                                    i = 3;
                                }
                                if (dt.Rows[temp][3].ToString() == "Friday")
                                {
                                    i = 4;
                                }
                                if (dt.Rows[temp][3].ToString() == "Saturday")
                                {
                                    i = 5;
                                }
                                if (dt.Rows[temp][3].ToString() == "Sunday")
                                {
                                    i = 6;
                                }

                                table[i, j] = dt.Rows[temp][0]+" "+dt.Rows[temp][1];
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Label teacher = new Label();
                    teacher.Font = new Font("Georgia", 15);
                    teacher.AutoSize = true;
                    if (table[j, i] == null)
                    {
                        teacher.Text = "----------";
                    }
                    else
                    {
                        teacher.Text = table[j, i];
                    }
                    
                    flowPanel[i].Controls.Add(teacher);
                }
            }
        }

        List<GrammarQuestion> Select_GramQuestion(string Level)
        {
            List<GrammarQuestion> result = new List<GrammarQuestion>();

            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.GrammarQuestion WHERE QuestionLevel = '"+Level+"';", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string answers=dt.Rows[i][2].ToString();
                                string[] listWithanswers = answers.Split(',');
                                GrammarQuestion question = new GrammarQuestion(dt.Rows[i][1].ToString(), listWithanswers,dt.Rows[i][3].ToString());
                                result.Add(question);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                        return result;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btn_marks_Click(object sender, EventArgs e)
        {
            string LevelTest = "";
            btn_std_module1.Enabled = false;
            btn_std_module2.Enabled = false;
            btn_Test.Enabled = false;

            foreach (var student in students)
            {
                if (student.Email == stud_email.Text)
                {
                    LevelTest = student.Level;
                }
            }
            if (LevelTest == "A2")
            {
                content_container.SelectedTab = TEST_A2;
                List<int> ListwithTextID = new List<int>();
                GramarQuestionA2 = Select_GramQuestion("A2");
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                    "Network Library=DBMSSOCN;" +
                    "Initial Catalog=DiplomnaDB;" +
                    "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                    {
                        try
                        {
                            con.Open();
                            if (con.State == ConnectionState.Open)
                            {
                                //FOR READING
                                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.exReading WHERE txtLevel = 'A2';", con);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                Random rnd = new Random();
                                int numberOfText = rnd.Next(0, dt.Rows.Count);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ListwithTextID.Add(int.Parse(dt.Rows[i][0].ToString()));
                                }

                                SqlDataAdapter questionSel = new SqlDataAdapter("SELECT * FROM dbo.exReadingQuestion WHERE TextId='" + ListwithTextID[numberOfText] + "';", con);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() == ListwithTextID[numberOfText].ToString())
                                    {
                                        A2ReadingText.Text = dt.Rows[i][1].ToString();
                                    }
                                }
                                DataTable dt2 = new DataTable();
                                questionSel.Fill(dt2);
                                A2ReadQuest1.Text = dt2.Rows[0][1].ToString();
                                A2ReadQuest2.Text = dt2.Rows[1][1].ToString();
                                A2ReadQuest3.Text = dt2.Rows[2][1].ToString();
                                A2ReadQuest4.Text = dt2.Rows[3][1].ToString();

                                string Answers = dt2.Rows[0][2].ToString();
                                string[] listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A2ReadQuest1answ.Items.Add(answer);
                                }

                                Answers = dt2.Rows[1][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A2ReadQuest2answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[2][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A2ReadQuest3answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[3][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A2ReadQuest4answ.Items.Add(answer);
                                }

                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    ListA2WithCurAnswer.Add(dt2.Rows[i][3].ToString());
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ne se polu4ava");

                            }
                            con.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Random randomNum = new Random();
                List<int> RandomNumbers = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    int number;

                    do
                    {
                        number = randomNum.Next(0, GramarQuestionA2.Count);
                    }
                    while (RandomNumbers.Contains(number));

                    RandomNumbers.Add(number);
                }

                A2GramQuest1.Text = GramarQuestionA2[RandomNumbers[0]].Question;
                ListA2WithCurAnswer.Add(GramarQuestionA2[RandomNumbers[0]].CurrectAnswer);
                A2GramQuest2.Text = GramarQuestionA2[RandomNumbers[1]].Question;
                ListA2WithCurAnswer.Add(GramarQuestionA2[RandomNumbers[1]].CurrectAnswer);
                A2GramQuest3.Text = GramarQuestionA2[RandomNumbers[2]].Question;
                ListA2WithCurAnswer.Add(GramarQuestionA2[RandomNumbers[2]].CurrectAnswer);
                A2GramQuest4.Text = GramarQuestionA2[RandomNumbers[3]].Question;
                ListA2WithCurAnswer.Add(GramarQuestionA2[RandomNumbers[3]].CurrectAnswer);
                A2GramQuest5.Text = GramarQuestionA2[RandomNumbers[4]].Question;
                ListA2WithCurAnswer.Add(GramarQuestionA2[RandomNumbers[4]].CurrectAnswer);

                string[] listAnswer = GramarQuestionA2[RandomNumbers[0]].Answers;
                foreach (var answer in listAnswer)
                {
                    A2GramQuest1answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA2[RandomNumbers[1]].Answers;
                foreach (var answer in listAnswer)
                {
                    A2GramQuest2answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA2[RandomNumbers[2]].Answers;
                foreach (var answer in listAnswer)
                {
                    A2GramQuest3answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA2[RandomNumbers[3]].Answers;
                foreach (var answer in listAnswer)
                {
                    A2GramQuest4answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA2[RandomNumbers[4]].Answers;
                foreach (var answer in listAnswer)
                {
                    A2GramQuest5answ.Items.Add(answer);
                }
            }

            if (LevelTest == "A1")
            {
                content_container.SelectedTab = TEST_A1;
                List<int> ListwithTextID = new List<int>();
                GramarQuestionA1 = Select_GramQuestion("A1");
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                    "Network Library=DBMSSOCN;" +
                    "Initial Catalog=DiplomnaDB;" +
                    "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                    {
                        try
                        {
                            con.Open();
                            if (con.State == ConnectionState.Open)
                            {
                                //FOR READING
                                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.exReading WHERE txtLevel = 'A1';", con);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                Random rnd = new Random();
                                int numberOfText = rnd.Next(0, dt.Rows.Count);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ListwithTextID.Add(int.Parse(dt.Rows[i][0].ToString()));
                                }

                                SqlDataAdapter questionSel = new SqlDataAdapter("SELECT * FROM dbo.exReadingQuestion WHERE TextId='" + ListwithTextID[numberOfText] + "';", con);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() == ListwithTextID[numberOfText].ToString())
                                    {
                                        A1ReadingText.Text = dt.Rows[i][1].ToString();
                                    }
                                }
                                DataTable dt2 = new DataTable();
                                questionSel.Fill(dt2);
                                A1ReadQuest1.Text = dt2.Rows[0][1].ToString();
                                A1ReadQuest2.Text = dt2.Rows[1][1].ToString();
                                A1ReadQuest3.Text = dt2.Rows[2][1].ToString();
                                A1ReadQuest4.Text = dt2.Rows[3][1].ToString();

                                string Answers = dt2.Rows[0][2].ToString();
                                string[] listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A1ReadQuest1answ.Items.Add(answer);
                                }

                                Answers = dt2.Rows[1][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A1ReadQuest2answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[2][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A1ReadQuest3answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[3][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    A1ReadQuest4answ.Items.Add(answer);
                                }

                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    ListA1WithCurAnswer.Add(dt2.Rows[i][3].ToString());
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ne se polu4ava");

                            }
                            con.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Random randomNum = new Random();
                List<int> RandomNumbers = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    int number;

                    do
                    {
                        number = randomNum.Next(0, GramarQuestionA1.Count);
                    }
                    while (RandomNumbers.Contains(number));

                    RandomNumbers.Add(number);
                }

                A1GramQuest1.Text = GramarQuestionA1[RandomNumbers[0]].Question;
                ListA1WithCurAnswer.Add(GramarQuestionA1[RandomNumbers[0]].CurrectAnswer);
                A1GramQuest2.Text = GramarQuestionA1[RandomNumbers[1]].Question;
                ListA1WithCurAnswer.Add(GramarQuestionA1[RandomNumbers[1]].CurrectAnswer);
                A1GramQuest3.Text = GramarQuestionA1[RandomNumbers[2]].Question;
                ListA1WithCurAnswer.Add(GramarQuestionA1[RandomNumbers[2]].CurrectAnswer);
                A1GramQuest4.Text = GramarQuestionA1[RandomNumbers[3]].Question;
                ListA1WithCurAnswer.Add(GramarQuestionA1[RandomNumbers[3]].CurrectAnswer);
                A1GramQuest5.Text = GramarQuestionA1[RandomNumbers[4]].Question;
                ListA1WithCurAnswer.Add(GramarQuestionA1[RandomNumbers[4]].CurrectAnswer);

                string[] listAnswer = GramarQuestionA1[RandomNumbers[0]].Answers;
                foreach (var answer in listAnswer)
                {
                    A1GramQuest1answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA1[RandomNumbers[1]].Answers;
                foreach (var answer in listAnswer)
                {
                    A1GramQuest2answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA1[RandomNumbers[2]].Answers;
                foreach (var answer in listAnswer)
                {
                    A1GramQuest3answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA1[RandomNumbers[3]].Answers;
                foreach (var answer in listAnswer)
                {
                    A1GramQuest4answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionA1[RandomNumbers[4]].Answers;
                foreach (var answer in listAnswer)
                {
                    A1GramQuest5answ.Items.Add(answer);
                }
            }

            if (LevelTest == "B1")
            {
                content_container.SelectedTab = TEST_B1a;
                List<int> ListwithTextID = new List<int>();
                GramarQuestionB1 = Select_GramQuestion("B1");
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                    "Network Library=DBMSSOCN;" +
                    "Initial Catalog=DiplomnaDB;" +
                    "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                    {
                        try
                        {
                            con.Open();
                            if (con.State == ConnectionState.Open)
                            {
                                //FOR READING
                                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.exReading WHERE txtLevel = 'B1';", con);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                Random rnd = new Random();
                                int numberOfText = rnd.Next(0, dt.Rows.Count);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ListwithTextID.Add(int.Parse(dt.Rows[i][0].ToString()));
                                }

                                SqlDataAdapter questionSel = new SqlDataAdapter("SELECT * FROM dbo.exReadingQuestion WHERE TextId='" + ListwithTextID[numberOfText] + "';", con);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() == ListwithTextID[numberOfText].ToString())
                                    {
                                        B1ReadingText.Text = dt.Rows[i][1].ToString();
                                    }
                                }
                                DataTable dt2 = new DataTable();
                                questionSel.Fill(dt2);
                                B1ReadQuest1.Text = dt2.Rows[0][1].ToString();
                                B1ReadQuest2.Text = dt2.Rows[1][1].ToString();
                                B1ReadQuest3.Text = dt2.Rows[2][1].ToString();
                                B1ReadQuest4.Text = dt2.Rows[3][1].ToString();

                                string Answers = dt2.Rows[0][2].ToString();
                                string[] listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B1ReadQuest1answ.Items.Add(answer);
                                }

                                Answers = dt2.Rows[1][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B1ReadQuest2answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[2][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B1ReadQuest3answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[3][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B1ReadQuest4answ.Items.Add(answer);
                                }

                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    ListB1WithCurAnswer.Add(dt2.Rows[i][3].ToString());
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ne se polu4ava");

                            }
                            con.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Random randomNum = new Random();
                List<int> RandomNumbers = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    int number;

                    do
                    {
                        number = randomNum.Next(0, GramarQuestionB1.Count);
                    }
                    while (RandomNumbers.Contains(number));

                    RandomNumbers.Add(number);
                }

                B1GramQuest1.Text = GramarQuestionB1[RandomNumbers[0]].Question;
                ListB1WithCurAnswer.Add(GramarQuestionB1[RandomNumbers[0]].CurrectAnswer);
                B1GramQuest2.Text = GramarQuestionB1[RandomNumbers[1]].Question;
                ListB1WithCurAnswer.Add(GramarQuestionB1[RandomNumbers[1]].CurrectAnswer);
                B1GramQuest3.Text = GramarQuestionB1[RandomNumbers[2]].Question;
                ListB1WithCurAnswer.Add(GramarQuestionB1[RandomNumbers[2]].CurrectAnswer);
                B1GramQuest4.Text = GramarQuestionB1[RandomNumbers[3]].Question;
                ListB1WithCurAnswer.Add(GramarQuestionB1[RandomNumbers[3]].CurrectAnswer);
                B1GramQuest5.Text = GramarQuestionB1[RandomNumbers[4]].Question;
                ListB1WithCurAnswer.Add(GramarQuestionB1[RandomNumbers[4]].CurrectAnswer);

                string[] listAnswer = GramarQuestionB1[RandomNumbers[0]].Answers;
                foreach (var answer in listAnswer)
                {
                    B1GramQuest1answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB1[RandomNumbers[1]].Answers;
                foreach (var answer in listAnswer)
                {
                    B1GramQuest2answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB1[RandomNumbers[2]].Answers;
                foreach (var answer in listAnswer)
                {
                    B1GramQuest3answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB1[RandomNumbers[3]].Answers;
                foreach (var answer in listAnswer)
                {
                    B1GramQuest4answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB1[RandomNumbers[4]].Answers;
                foreach (var answer in listAnswer)
                {
                    B1GramQuest5answ.Items.Add(answer);
                }
            }

            if (LevelTest == "B2")
            {
                content_container.SelectedTab = TEST_B2a;
                List<int> ListwithTextID = new List<int>();
                GramarQuestionB2 = Select_GramQuestion("B2");
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                    "Network Library=DBMSSOCN;" +
                    "Initial Catalog=DiplomnaDB;" +
                    "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                    {
                        try
                        {
                            con.Open();
                            if (con.State == ConnectionState.Open)
                            {
                                //FOR READING
                                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.exReading WHERE txtLevel = 'B2';", con);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                Random rnd = new Random();
                                int numberOfText = rnd.Next(0, dt.Rows.Count);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ListwithTextID.Add(int.Parse(dt.Rows[i][0].ToString()));
                                }

                                SqlDataAdapter questionSel = new SqlDataAdapter("SELECT * FROM dbo.exReadingQuestion WHERE TextId='" + ListwithTextID[numberOfText] + "';", con);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString() == ListwithTextID[numberOfText].ToString())
                                    {
                                        B2ReadingText.Text = dt.Rows[i][1].ToString();
                                    }
                                }
                                DataTable dt2 = new DataTable();
                                questionSel.Fill(dt2);
                                B2ReadQuest1.Text = dt2.Rows[0][1].ToString();
                                B2ReadQuest2.Text = dt2.Rows[1][1].ToString();
                                B2ReadQuest3.Text = dt2.Rows[2][1].ToString();
                                B2ReadQuest4.Text = dt2.Rows[3][1].ToString();

                                string Answers = dt2.Rows[0][2].ToString();
                                string[] listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B2ReadQuest1answ.Items.Add(answer);
                                }

                                Answers = dt2.Rows[1][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B2ReadQuest2answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[2][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B2ReadQuest3answ.Items.Add(answer);
                                }
                                Answers = dt2.Rows[3][2].ToString();
                                listWithAnswer = Answers.Split(',');
                                foreach (var answer in listWithAnswer)
                                {
                                    B2ReadQuest4answ.Items.Add(answer);
                                }

                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    ListB2WithCurAnswer.Add(dt2.Rows[i][3].ToString());
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ne se polu4ava");

                            }
                            con.Close();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Random randomNum = new Random();
                List<int> RandomNumbers = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    int number;
                    do
                    {
                        number = randomNum.Next(0, GramarQuestionB2.Count);
                    }
                    while (RandomNumbers.Contains(number));

                    RandomNumbers.Add(number);
                }

                B2GramQuest1.Text = GramarQuestionB2[RandomNumbers[0]].Question;
                ListB2WithCurAnswer.Add(GramarQuestionB2[RandomNumbers[0]].CurrectAnswer);
                B2GramQuest2.Text = GramarQuestionB2[RandomNumbers[1]].Question;
                ListB2WithCurAnswer.Add(GramarQuestionB2[RandomNumbers[1]].CurrectAnswer);
                B2GramQuest3.Text = GramarQuestionB2[RandomNumbers[2]].Question;
                ListB2WithCurAnswer.Add(GramarQuestionB2[RandomNumbers[2]].CurrectAnswer);
                B2GramQuest4.Text = GramarQuestionB2[RandomNumbers[3]].Question;
                ListB2WithCurAnswer.Add(GramarQuestionB2[RandomNumbers[3]].CurrectAnswer);
                B2GramQuest5.Text = GramarQuestionB2[RandomNumbers[4]].Question;
                ListB2WithCurAnswer.Add(GramarQuestionB2[RandomNumbers[4]].CurrectAnswer);

                string[] listAnswer = GramarQuestionB2[RandomNumbers[0]].Answers;
                foreach (var answer in listAnswer)
                {
                    B2GramQuest1answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB2[RandomNumbers[1]].Answers;
                foreach (var answer in listAnswer)
                {
                    B2GramQuest2answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB2[RandomNumbers[2]].Answers;
                foreach (var answer in listAnswer)
                {
                    B2GramQuest3answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB2[RandomNumbers[3]].Answers;
                foreach (var answer in listAnswer)
                {
                    B2GramQuest4answ.Items.Add(answer);
                }

                listAnswer = GramarQuestionB2[RandomNumbers[4]].Answers;
                foreach (var answer in listAnswer)
                {
                    B2GramQuest5answ.Items.Add(answer);
                }
            }
        }

        public void FinishTest(string studentsEm, string writingText, int pointFromTest)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {
                            SqlCommand updatePoint = new SqlCommand("UPDATE dbo.Marks SET testPoint='" + pointFromTest + "' WHERE StudentEmail='" + studentsEm + "';", con);
                            updatePoint.ExecuteNonQuery();
                            SqlCommand updateWriting = new SqlCommand("UPDATE dbo.Marks SET writing='" + writingText + "' WHERE StudentEmail='" + studentsEm + "';", con);
                            updateWriting.ExecuteNonQuery();

                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FinishTestB(string studentsEm, string writingText, int pointFromSecHalf)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT testPoint FROM dbo.Marks WHERE StudentEmail = '" + studentsEm + "';", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            int pointFromFirstHalf = int.Parse(dt.Rows[0][0].ToString());
                            int pointForAllTest = pointFromFirstHalf + pointFromSecHalf;
                            SqlCommand updatePoint = new SqlCommand("UPDATE dbo.Marks SET testPoint='" + pointForAllTest + "' WHERE StudentEmail='" + studentsEm + "';", con);
                            updatePoint.ExecuteNonQuery();
                            SqlCommand updateWriting = new SqlCommand("UPDATE dbo.Marks SET writing='" + writingText + "' WHERE StudentEmail='" + studentsEm + "';", con);
                            updateWriting.ExecuteNonQuery();

                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            int point = 0;
            if (A2ReadQuest1answ.Text == ListA2WithCurAnswer[0].ToString())
            {
                point=point+2;
            }
            if (A2ReadQuest2answ.Text == ListA2WithCurAnswer[1].ToString())
            {
                point = point + 2;
            }
            if (A2ReadQuest3answ.Text == ListA2WithCurAnswer[2].ToString())
            {
                point = point + 2;
            }
            if (A2ReadQuest4answ.Text == ListA2WithCurAnswer[3].ToString())
            {
                point = point + 2;
            }

            if (A2GramQuest1answ.Text == ListA2WithCurAnswer[4].ToString())
            {
                point = point + 2;
            }
            if (A2GramQuest2answ.Text == ListA2WithCurAnswer[5].ToString())
            {
                point = point + 2;
            }
            if (A2GramQuest3answ.Text == ListA2WithCurAnswer[6].ToString())
            {
                point = point + 2;
            }
            if (A2GramQuest4answ.Text == ListA2WithCurAnswer[7].ToString())
            {
                point = point + 2;
            }
            if (A2GramQuest5answ.Text == ListA2WithCurAnswer[8].ToString())
            {
                point = point + 2;
            }

            FinishTest(stud_email.Text, TestWritA2_text.Text, point);
            content_container.SelectedTab = Thanks_page;
        }

        private void EndTestA1_btn_Click(object sender, EventArgs e)
        {
            int point = 0;
            if (A1ReadQuest1answ.Text == ListA1WithCurAnswer[0].ToString())
            {
                point = point + 2;                      
            }                                 
            if (A1ReadQuest2answ.Text == ListA1WithCurAnswer[1].ToString())
            {
                point = point + 2;                      
            }                                 
            if (A1ReadQuest3answ.Text == ListA1WithCurAnswer[2].ToString())
            {
                point = point + 2;                    
            }                                 
            if (A1ReadQuest4answ.Text == ListA1WithCurAnswer[3].ToString())
            {
                point = point + 2;                     
            }                                 
                                              
            if (A1GramQuest1answ.Text == ListA1WithCurAnswer[4].ToString())
            {
                point = point + 2;                    
            }                                 
            if (A1GramQuest2answ.Text == ListA1WithCurAnswer[5].ToString())
            {
                point = point + 2;                     
            }                                 
            if (A1GramQuest3answ.Text == ListA1WithCurAnswer[6].ToString())
            {
                point = point + 2;                     
            }                                 
            if (A1GramQuest4answ.Text == ListA1WithCurAnswer[7].ToString())
            {
                point = point + 2;                  
            }                                 
            if (A1GramQuest5answ.Text == ListA1WithCurAnswer[8].ToString())
            {
                point = point + 2;
            }

            FinishTest(stud_email.Text, TestWritingA1_text.Text, point);
            content_container.SelectedTab = Thanks_page;
        }

        private void btn_B1Next_Click(object sender, EventArgs e)
        {

            int point = 0;
            if (B1ReadQuest1answ.Text == ListB1WithCurAnswer[0].ToString())
            {
                point = point + 2;
            }
            if (B1ReadQuest2answ.Text == ListB1WithCurAnswer[1].ToString())
            {
                point = point + 2;
            }
            if (B1ReadQuest3answ.Text == ListB1WithCurAnswer[2].ToString())
            {
                point = point + 2;
            }
            if (B1ReadQuest4answ.Text == ListB1WithCurAnswer[3].ToString())
            {
                point = point + 2;
            }

            if (B1GramQuest1answ.Text == ListB1WithCurAnswer[4].ToString())
            {
                point = point + 2;
            }
            if (B1GramQuest2answ.Text == ListB1WithCurAnswer[5].ToString())
            {
                point = point + 2;
            }
            if (B1GramQuest3answ.Text == ListB1WithCurAnswer[6].ToString())
            {
                point = point + 2;
            }
            if (B1GramQuest4answ.Text == ListB1WithCurAnswer[7].ToString())
            {
                point = point + 2;
            }
            if (B1GramQuest5answ.Text == ListB1WithCurAnswer[8].ToString())
            {
                point = point + 2;
            }

            FinishTest(stud_email.Text, "", point);
            content_container.SelectedTab = TEST_B1b;
            B1ListenQuest1answ.Items.Add("True");
            B1ListenQuest1answ.Items.Add("False");
            B1ListenQuest1answ.Items.Add("Doesn't say");

            B1ListenQuest2answ.Items.Add("True");
            B1ListenQuest2answ.Items.Add("False");
            B1ListenQuest2answ.Items.Add("Doesn't say");

            B1ListenQuest3answ.Items.Add("True");
            B1ListenQuest3answ.Items.Add("False");
            B1ListenQuest3answ.Items.Add("Doesn't say");

            B1ListenQuest4answ.Items.Add("True");
            B1ListenQuest4answ.Items.Add("False");
            B1ListenQuest4answ.Items.Add("Doesn't say");

            B1ListenQuest5answ.Items.Add("True");
            B1ListenQuest5answ.Items.Add("False");
            B1ListenQuest5answ.Items.Add("Doesn't say");

        }

        private void btn_NextB2_Click(object sender, EventArgs e)
        {
            int point = 0;
            if (B2ReadQuest1answ.Text == ListB2WithCurAnswer[0].ToString())
            {
                point = point + 2;
            }
            if (B2ReadQuest2answ.Text == ListB2WithCurAnswer[1].ToString())
            {
                point = point + 2;
            }
            if (B2ReadQuest3answ.Text == ListB2WithCurAnswer[2].ToString())
            {
                point = point + 2;
            }
            if (B2ReadQuest4answ.Text == ListB2WithCurAnswer[3].ToString())
            {
                point = point + 2;
            }

            if (B2GramQuest1answ.Text == ListB2WithCurAnswer[4].ToString())
            {
                point = point + 2;
            }
            if (B2GramQuest2answ.Text == ListB2WithCurAnswer[5].ToString())
            {
                point = point + 2;
            }
            if (B2GramQuest3answ.Text == ListB2WithCurAnswer[6].ToString())
            {
                point = point + 2;
            }
            if (B2GramQuest4answ.Text == ListB2WithCurAnswer[7].ToString())
            {
                point = point + 2;
            }
            if (B2GramQuest5answ.Text == ListB2WithCurAnswer[8].ToString())
            {
                point = point + 2;
            }
            MessageBox.Show(point.ToString());

            FinishTest(stud_email.Text, "", point);
            content_container.SelectedTab = TEST_B2b;
            B2ListenQuest1answ.Items.Add("has come to symbolize New Zealand’s independence.");
            B2ListenQuest1answ.Items.Add("was taken from the language of the original inhabitants.");
            B2ListenQuest1answ.Items.Add("is associated with the first settlers in New Zealand.");
            B2ListenQuest1answ.Items.Add("bears mystic meaning for most New Zealanders.");

            B2ListenQuest2answ.Items.Add("a decoration on New Zealand military uniforms.");
            B2ListenQuest2answ.Items.Add("a nickname for all New Zealanders.");
            B2ListenQuest2answ.Items.Add("a symbol of courage and bravery.");
            B2ListenQuest2answ.Items.Add("a national symbol of New Zealand.");

            B2ListenQuest3answ.Items.Add("the New Zealand police officers.");
            B2ListenQuest3answ.Items.Add("the Maori, the native people of New Zealand.");
            B2ListenQuest3answ.Items.Add("the whole population of New Zealand.");
            B2ListenQuest3answ.Items.Add("the soldiers from the Second World War.");

            B2ListenQuest4answ.Items.Add("one of the oldest birds in the world.");
            B2ListenQuest4answ.Items.Add("the largest bird in the world.");
            B2ListenQuest4answ.Items.Add("the only bird with a tail but no wings.");
            B2ListenQuest4answ.Items.Add("one of the few birds in New Zealand.");

            B2ListenQuest5answ.Items.Add("lay the largest eggs of all birds.");
            B2ListenQuest5answ.Items.Add("have developed very good eyesight.");
            B2ListenQuest5answ.Items.Add("have an average weight of four kilos.");
            B2ListenQuest5answ.Items.Add("feed on plants and small animals.");
        }

        private void btn_FinishTestB2_Click(object sender, EventArgs e)
        {
            int point = 0;
            if (B2ListenQuest1answ.Text == "was taken from the language of the original inhabitants.")
            {
                point = point + 2;
            }
            if (B2ListenQuest2answ.Text == "a decoration on New Zealand military uniforms.")
            {
                point = point + 2;
            }
            if (B2ListenQuest3answ.Text == "the whole population of New Zealand.")
            {
                point = point + 2;
            }
            if (B2ListenQuest4answ.Text == "one of the oldest birds in the world.")
            {
                point = point + 2;
            }
            if (B2ListenQuest5answ.Text == "feed on plants and small animals.")
            {
                point = point + 2;
            }

            MessageBox.Show(point.ToString());
            FinishTestB(stud_email.Text,B2WritingText.Text,point);
            content_container.SelectedTab = Thanks_page;
        }

        private void btn_FinishB1_Click(object sender, EventArgs e)
        {
            int point = 0;
            if (B2ListenQuest1answ.Text == "False")
            {
                point = point + 2;
            }
            if (B2ListenQuest2answ.Text == "False")
            {
                point = point + 2;
            }
            if (B2ListenQuest3answ.Text == "True")
            {
                point = point + 2;
            }
            if (B2ListenQuest4answ.Text == "True")
            {
                point = point + 2;
            }
            if (B2ListenQuest5answ.Text == "Doesn't say")
            {
                point = point + 2;
            }

            MessageBox.Show(point.ToString());
            FinishTestB(stud_email.Text, B1WritngText.Text, point);
            content_container.SelectedTab = Thanks_page;
        }

        private void btn_TestTime_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=" + DataSourse + ",1433;" +
                "Network Library=DBMSSOCN;" +
                "Initial Catalog=DiplomnaDB;" +
                "User ID=THE_JOHNY_STYLE;Password=egn9211285320;"))
                {
                    try
                    {
                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM dbo.Marks WHERE TeacherEGN = '"+teach_egn.Text+"';", con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                SqlCommand updatePoint = new SqlCommand("UPDATE dbo.Marks SET TestTime='True' WHERE StudentEmail='" +dt.Rows[i][1].ToString() + "';", con);
                                updatePoint.ExecuteNonQuery();
                               
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ne se polu4ava");

                        }
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_std_module1_Click(object sender, EventArgs e)
        {
            string level = "";
            foreach (var student in students)
            {
                if (student.Email == stud_email.Text)
                {
                    level = student.Level;
                    break;
                }
            }
            if (level =="A1")
            {
                content_container.SelectedTab = A1Modul1_page;
            }
            else if (level == "A2")
            {
                content_container.SelectedTab = A2Modul1_page;
            }
            else if (level == "B1")
            {
                content_container.SelectedTab = B1Modul1_page;
            }
            else if (level == "B2")
            {
                content_container.SelectedTab = B2Modul1_page;
            }
        }

        private void btn_std_module2_Click(object sender, EventArgs e)
        {
            string level = "";
            foreach (var student in students)
            {
                if (student.Email == stud_email.Text)
                {
                    level = student.Level;
                    break;
                }
            }
            if (level == "A1")
            {
                content_container.SelectedTab = A1Modul2_page;
            }
            else if (level == "A2")
            {
                content_container.SelectedTab = A2Modul2_page;
            }
            else if (level == "B1")
            {
                content_container.SelectedTab = B1Modul2_page;
            }
            else if (level == "B2")
            {
                content_container.SelectedTab = B2Modul2_page;
            }
        }

        private void A1reading_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A1M1Read_page;
        }

        private void A1grammar_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A1M1Gram_page;
        }

        private void A1reading2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A1M2Read_page;
        }

        private void A1grammar2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A1M2Gram_page;
        }

        private void A2reading_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A2M1Read_page;
        }

        private void A2grammar_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A2M1Gram_page;
        }

        private void A2reading2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A2M2Read_page;
        }

        private void A2grammar2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = A2M2Gram_page;
        }

        private void B1reading_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B1M1Read_page;
        }

        private void B1grammar_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B1M1Gram_page;
        }

        private void B1reading2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B1M2Read_page;
        }

        private void B1grammar2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B1M2Gram_page;
        }

        private void B2reading_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B2M1Read_page;
        }

        private void B2grammar_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B2M1Gram_page;
        }

        private void B2reading2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B2M2Read_page;
        }

        private void B2grammar2_pic_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = B2M2Gram_page;
        }

        private void btn_playListen_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer soundPlayer = new SoundPlayer(Diplomen_proekt.Resources.Resource1.The_Kiwi);
                soundPlayer.Play();
                btn_playListen.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btn_playListenB1_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer soundPlayer = new SoundPlayer(Diplomen_proekt.Resources.Resource1.Glima);
                soundPlayer.Play();
                btn_playListenB1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = Home_page;
        }

        private void btn_Contact_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = Contact_page;
        }

        private void btn_backToReg_Click(object sender, EventArgs e)
        {
            content_container.SelectedTab = new_reg_page;
        }

        private void btn_module1_Click(object sender, EventArgs e)
        {
            string level = "";
            foreach (var teacher in teachers)
            {
                if (teacher.EGN == teach_egn.Text)
                {
                    level = teacher.Leveltch;
                    break;
                }
            }
            if (level == "A1")
            {
                content_container.SelectedTab = A1Modul1_page;
            }
            else if (level == "A2")
            {
                content_container.SelectedTab = A2Modul1_page;
            }
            else if (level == "B1")
            {
                content_container.SelectedTab = B1Modul1_page;
            }
            else if (level == "B2")
            {
                content_container.SelectedTab = B2Modul1_page;
            }
        }

        private void btn_module2_Click(object sender, EventArgs e)
        {
            string level = "";
            foreach (var teacher in teachers)
            {
                if (teacher.EGN == teach_egn.Text)
                {
                    level = teacher.Leveltch;
                    break;
                }
            }
            if (level == "A1")
            {
                content_container.SelectedTab = A1Modul2_page;
            }
            else if (level == "A2")
            {
                content_container.SelectedTab = A2Modul2_page;
            }
            else if (level == "B1")
            {
                content_container.SelectedTab = B1Modul2_page;
            }
            else if (level == "B2")
            {
                content_container.SelectedTab = B2Modul2_page;
            }
        }

        public void logOutFunc()
        {
            if (student_menu.Visible==true)
	        {
		        first_menu.Visible=true;
                student_menu.Visible = false;
                content_container.SelectedTab = Home_page;
                btn_Contact.Enabled = true;
                btn_Reg_Log.Enabled = true;
                btn_Home.Enabled = true;
                btn_Teachers.Enabled = true;
	        }
            else if (teacher_menu.Visible == true)
            {
                first_menu.Visible = true;
                teacher_menu.Visible = false;
                content_container.SelectedTab = Home_page;
            }
            else if (maneger_menu.Visible == true)
            {
                first_menu.Visible = true;
                maneger_menu.Visible = false;
                content_container.SelectedTab = Home_page;
            }
        }

        private void studLogOut_Click(object sender, EventArgs e)
        {
            logOutFunc();
        }

        private void menagLogOut_Click(object sender, EventArgs e)
        {
            logOutFunc();
        }

        private void teachLogOut_Click(object sender, EventArgs e)
        {
            logOutFunc();
        }

    }
}
