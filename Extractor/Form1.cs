namespace Extractor
{
    using System.IO;
    using System.Text;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string fn = "F:\\img.img";
            bool writing = false;
            BinaryWriter bw = null;
            int FileNo = 1;
            FileStream fsw= null;
            ulong read = 0;

            using (FileStream fs= new FileStream(fn, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs, new ASCIIEncoding()))
                {
                    bool found = false;
                    byte[] data;
                    const int BUFF_SIZE = 1000000;
                    while (!found)
                    {
                        data = br.ReadBytes(BUFF_SIZE);
                        read += (ulong)(Math.Abs(data.Length));
                        long MB = (long)(read / 1000000);
                        label1.Text = MB.ToString();
                        label1.Refresh();

                        if(data.Length < BUFF_SIZE)
                        {
                            bw.Close();
                            fsw.Close();
                            found = true;
                            break;
                        }
                        bool isNewFile = false;
                        for (int i = 0; i < data.Length-4; i++)
                        {
                            if (data[i] == 0x52)
                            {
                                if (data[i + 1] == 0x49)
                                {
                                    if (data[i + 2] == 0x46)
                                    {
                                        if (data[i + 3] == 0x46)
                                        {
                                            if(bw!=null)
                                            {
                                                bw.Close();
                                                fsw.Close();
                                            }
                                            string fileName = string.Format("f:\\extract\\{0}.avi", FileNo.ToString());
                                            label2.Text = FileNo.ToString();
                                            label2.Refresh();
                                            FileNo++;
                                            fsw = File.Open(fileName, FileMode.Create);
                                            bw = new BinaryWriter(fsw, Encoding.UTF8, true);
                                            int lengthToWrite = BUFF_SIZE - i;
                                            bw.Write(data, i, lengthToWrite);
                                            isNewFile = true;
                                            writing = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if(!isNewFile)
                        {
                            if (writing)
                            {
                                bw.Write(data);
                            }
                        }


                    }
                }
            }
        }
    }
}