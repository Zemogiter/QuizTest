using NAudio.Wave;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuizTest
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        //quiz game variables
        public int correctAnswer;
        int questionNumber = 1;
        int score;
        float precentage;
        readonly int totalQuestions;
        
        public Form1()
        {
            InitializeComponent();

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 50, 50));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 50, 50));
            button3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 50, 50));
            button4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button4.Width, button4.Height, 50, 50));
            this.Paint += new PaintEventHandler(Set_background); //form background, not buttons
            this.SizeChanged += Form1_SizeChanged;
            this.KeyPress += Form1_KeyPress;
            this.KeyDown += Form1_KeyDown;
            AskQuestion(questionNumber);
            totalQuestions = 10;
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //ToDo
            }
        }
        //handles the space and enter key presses (we dont want them to be pressed bc they default to the first button), only works for the first question
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Enter)
                    e.Handled = true;
                      
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.KeyCode == Keys.Space)
                e.Handled = true;
        }
        private void Set_background(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            //the rectangle, the same size as our Form
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);

            //define gradient's properties
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(0, 0, 0), Color.FromArgb(57, 128, 227), 65f);

            //apply gradient         
            graphics.FillRectangle(b, gradient_rectangle);
        }

        private void CheckAnswer(object sender, EventArgs e) //activated each time one of the 4 buttons are pressed
        {
            var senderObject = (Button)sender;
            int buttonTag = Convert.ToInt32(senderObject.Tag);
            
            if (buttonTag == correctAnswer) //correct answer
            {
                byte[] fileByte = Properties.Resources.snare_roll_84943;
                Stream stream = new MemoryStream(fileByte);
                var reader = new NAudio.Wave.Mp3FileReader(stream);
                var waveOut = new WaveOut();
                waveOut.Init(reader);
                waveOut.Play();
                score++;
            }
            if (buttonTag != correctAnswer) //incorrect answer
            {
                //give a error pop-up bc wrong answer
                byte[] fileByte = Properties.Resources.you_stupid_vine__1_;
                Stream stream = new MemoryStream(fileByte);
                var reader = new NAudio.Wave.Mp3FileReader(stream);
                var waveOut = new WaveOut();
                waveOut.Init(reader);
                waveOut.Play();
                MessageBox.Show("Błędna odpowiedź (" + buttonTag +  ", prawidłowa to " + correctAnswer + "). Naciśni OK aby przejść dalej.");
            }
            if (questionNumber == totalQuestions) //all questions asked and anserwed
            {
                precentage = (int)Math.Round((double)(score * 100) / totalQuestions);
                MessageBox.Show("Quiz zakończony" + Environment.NewLine + "Odpowiedzałeś/aś na " + score + " pytań prawidłowo" + Environment.NewLine + "Procent prawidłowych odpowiedzi: " + precentage + "%" + Environment.NewLine + "Naciśni OK aby zacząć quiz od nowa.");
                score = 0;
                questionNumber = 0;
                AskQuestion(questionNumber);
            }
            
            questionNumber++;
            AskQuestion(questionNumber);
        }

        public void AskQuestion(int qNumber) //main function and question/answer storage
        {
            switch (qNumber)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.question_generic;
                    labelQuestion.Text = "Jakie ubranie będzie Ci potrzebne na wakacjach nad morzem?";
                    button1.Text = "strój do tańca";
                    button2.Text = "strój kąpielowy";
                    button3.Text = "kombinezon narciarski";
                    button4.Text = "kombinezon medyczny";
                    correctAnswer = 2;
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "Polskie morze leży na:";
                    button1.Text = "Polska nie ma \n dostępu do morza.";
                    button2.Text = "południu kraju";
                    button3.Text = "północy kraju";
                    button4.Text = "zachodzie kraju";
                    correctAnswer = 3;
                    break;
                case 3:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "By naszej skóry nie spaliło słońce, należy posmarować się:";
                    button1.Text = "masłem Delma";
                    button2.Text = "masłem orzechowym";
                    button3.Text = "masłem kakaowym";
                    button4.Text = "olejkiem lub kremem \n z filtrem UV";
                    correctAnswer = 4;
                    break;
                case 4:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "Na plaży nie wolno:";
                    button1.Text = "opalać się";
                    button2.Text = "budować zamków z piasku";
                    button3.Text = "śmiecić";
                    button4.Text = "walczyć na miecze";
                    correctAnswer = 3;
                    break;
                case 5:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "Po co zakłada się klapki podczas pobytu na basenie?";
                    button1.Text = "bo taka jest moda";
                    button2.Text = "ze względów higienicznych";
                    button3.Text = "żeby nie było zimno w nogi";
                    button4.Text = "bo rodzice kazali";
                    correctAnswer = 2;
                    break;
                case 6:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "Na wakacjach można:";
                    button1.Text = "cieszyć się i bawić";
                    button2.Text = "jeść lody i budować \n zamki z piasku";
                    button3.Text = "obie powyższe odpowiedzi \n są poprawne";
                    button4.Text = "leżeć w domu i nic nie robić";
                    correctAnswer = 3;
                    break;
                case 7:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "Podczas upałów należy pamiętać:";
                    button1.Text = "o piciu dużej ilości wody \n i zabezpieczeniu skóry \n przed słońcem";
                    button2.Text = "o zjedzeniu ciepłego rosołu";
                    button3.Text = "o zbieraniu brązowych grzybów";
                    button4.Text = "o wyrzuceniu śmieci";
                    correctAnswer = 1;
                    break;
                case 8:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "Gdy wybieramy się na przepływ kajakiem, wszyscy uczestnicy wyjazdu mają mieć na sobie:";
                    button1.Text = "sandały";
                    button2.Text = "szelki";
                    button3.Text = "kamizelki ratunkowe";
                    button4.Text = "różaniec";
                    correctAnswer = 3;
                    break;
                case 9:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "Gdy w kąpielisku wodnym widzimy czerwoną boję, oznacza to:";
                    button1.Text = "że w tym miejscu jest płytko";
                    button2.Text = "że jest tam dużo ryb";
                    button3.Text = "że nie wolno pływać dalej";
                    button4.Text = "miejsce działań poligonowych";
                    correctAnswer = 3;
                    break;
                case 10:
                    pictureBox1.Image = Properties.Resources.question_generic; 
                    labelQuestion.Text = "W jakim terminie zazwyczaj odbywają się w Polsce wakacje dla dzieci w wieku szkolnym?";
                    button1.Text = "od połowy maja \n do połowy lipca";
                    button2.Text = "od końca czerwca \n do końca sierpnia";
                    button3.Text = "od początku lipca \n do końca sierpnia";
                    button4.Text = "od początku sierpnia \n do końca września";
                    correctAnswer = 2;
                    break;
            }
        }
    }
    /*public class CustomizeFormSizeController : WindowController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            Window.TemplateChanged += Window_TemplateChanged;
        }
        private void Window_TemplateChanged(object sender, EventArgs e)
        {
            if (Window.Template is System.Windows.Forms.Form &&
    Window.Template is ISupportStoreSettings)
            {
                ((ISupportStoreSettings)Window.Template).SettingsReloaded +=
    OnFormReadyForCustomizations;
            }
        }
        private void OnFormReadyForCustomizations(object sender, EventArgs e)
        {
            if (YourCustomBusinessCondition(Window.View))
            {
                ((System.Windows.Forms.Form)sender).Size =
    ((IFormSizeProvider)Window.View.CurrentObject).GetFormSize();
            }
        }
        private bool YourCustomBusinessCondition(View view)
        {
            return view != null && view.CurrentObject is IFormSizeProvider;
        }
        protected override void OnDeactivated()
        {
            Window.TemplateChanged -= Window_TemplateChanged;
            base.OnDeactivated();
        }
    }*/
}
