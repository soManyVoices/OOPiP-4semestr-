namespace DynamicWinFormApp
{
    public class MainForm : Form
    {
        private TextBox textBox;
        private Button openButton;
        private VScrollBar vScrollBar;

        public MainForm()
        {
            InitializeComponents();
            AttachEvents();
        }

        private void InitializeComponents()
        {
            this.Text = "Text Viewer";
            this.Size = new System.Drawing.Size(400, 300);

            textBox = new TextBox();
            textBox.Multiline = true;
            textBox.Dock = DockStyle.Fill;
            textBox.ScrollBars = ScrollBars.Both; 

            openButton = new Button();
            openButton.Text = "Open File";
            openButton.Dock = DockStyle.Top;

            vScrollBar = new VScrollBar();
            vScrollBar.Dock = DockStyle.Right;
            vScrollBar.Visible = false;

            this.Controls.Add(textBox);
            this.Controls.Add(openButton);
        }



        private void AttachEvents()
        {
            openButton.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    ReadTextFile(fileName);
                }
            };

            textBox.TextChanged += new EventHandler(TextChangedHandler);
        }

        private void TextChangedHandler(object sender, EventArgs e)
        {
            vScrollBar.Visible = textBox.Lines.Length > textBox.Height / textBox.Font.Height;
            vScrollBar.Maximum = Math.Max(0, textBox.Lines.Length - textBox.Height / textBox.Font.Height);
        }

        private void ReadTextFile(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    textBox.Text = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }

    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
