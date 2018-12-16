using NAudio.Wave;
using System;
using System.Media;
using System.Windows.Forms;
namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        //ループ回数
        int loop_cnt = 0;
        int additional_cnt = 0;
        int timeLeft = 0;
        //これはBGM用
        private System.Media.SoundPlayer player = null;
        //5秒前からのアラーム
        string SoundFile = "count.wav";
        bool flag = true;
        bool flag2 = true;
        WaveOut waveOut = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label7.Text = "0";
            InitializeTimer();
        }

        static void timer_Elapsed(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now);
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                label1.Text = "次のカウントまで";
                if (timeLeft > 0)
                {
                    timeLeft = timeLeft - 1;
                    label4.Text = timeLeft + " seconds";
                }
                else
                {
                    timer2.Stop();
                    label4.Text = "Time's up!";
                }
            });
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (int.TryParse(textBox1.Text, out loop_cnt))
            {
                button3.Text = "入場";
                AudioFileReader reader = new AudioFileReader("start.wav");
                reader.Position = 0;
                WaveOut takada = new WaveOut();
                takada.Init(reader);
                takada.Play();
                //変換出来たら、dにその数値が入る
                label6.Text = loop_cnt.ToString();
                label1.Text = "実行開始";
                // works(loop_cnt);
                timer1.Enabled = true;
                timer2.Enabled = true;
            }
            else
            {
                MessageBox.Show("{0} は数字ではありません。", textBox1.Text);
            }
        }

        private void InitializeTimer()
        {
            //初回は15秒（音声開始時刻）
            timer1.Interval = 15000;
            //UIカウント用
            timer2.Interval = 1000;
            button2.Text = "start";
            timer1.Tick += new EventHandler(Timer1_Tick);
            button1.Click += new EventHandler(button1_Click);
            timer2.Tick += new EventHandler(timer2_Tick);
            //1回目
            timeLeft = 35;
        }


        private void Timer1_Tick(object Sender, EventArgs e)
        {
            loop_cnt--;
            if (flag)
            {
                //初回ループのみ
                label6.Text = loop_cnt.ToString();
                //オーディオファイル名
                ////2回目の秒数
                playSoundEffect();
                timer1.Interval = 20000;
                timeLeft = 20;
                timer2.Enabled = true;
                flag = false;
            }
            else if (loop_cnt > 0)
            {
                label6.Text = loop_cnt.ToString();
                //オーディオファイル名
                playSoundEffect();
                timeLeft = 20;
                timer2.Enabled = true;
            }
            else
            {
                MessageBox.Show("終了");
                timer1.Stop();
            }

        }
       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int tmp_cnt = 0;
            //延長時間分（０から９までじゃないと挙動がおかしくなるよ）
            if (int.TryParse(textBox2.Text, out tmp_cnt))
            {
                loop_cnt += tmp_cnt;
                additional_cnt += tmp_cnt;
                label7.Text = additional_cnt.ToString();
            }
        }

        private void playSoundEffect()
        {
            AudioFileReader reader = new AudioFileReader(SoundFile);
            reader.Position = 0;
            if (waveOut == null)
            {
                waveOut = new WaveOut();
            }
            waveOut.Init(reader);
            waveOut.Play();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (player == null)
            {
                button2.Text = "Stop";
                player = new SoundPlayer("bgm2.wav");
                player.PlayLooping();
            }
            else
            {
                button2.Text = "Start";
                player.Stop();
                player.Dispose();
                player = null;
            }
        }
        /**
         * こういう書き方をして激しく後悔してる
         * 
         */ 
        private void button3_Click(object sender, EventArgs e)
        {
            AudioFileReader reader = new AudioFileReader("entrance.wav");
            reader.Position = 0;
            WaveOut takada = new WaveOut();
            takada.Init(reader);
            takada.Play();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            AudioFileReader reader = new AudioFileReader("countcall.wav");
            reader.Position = 0;

            WaveOut takada = new WaveOut();

            takada.Init(reader);
            takada.Play();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            AudioFileReader reader = new AudioFileReader("321.wav");
            reader.Position = 0;
            WaveOut takada = new WaveOut();
            takada.Init(reader);
            takada.Play();

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            AudioFileReader reader = new AudioFileReader("result.wav");
            reader.Position = 0;
            WaveOut takada = new WaveOut();
            takada.Init(reader);
            takada.Play();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            AudioFileReader reader = new AudioFileReader("goback.wav");
            reader.Position = 0;
            WaveOut takada = new WaveOut();
            takada.Init(reader);
            takada.Play();
        }
    }
}

