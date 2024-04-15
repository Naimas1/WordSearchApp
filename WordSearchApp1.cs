namespace WordSearchApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWord.Text) || string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                MessageBox.Show("Please enter a word and file path.");
                return;
            }

            string word = txtWord.Text.ToLower();
            string filePath = txtFilePath.Text;

            try
            {
                int count = await SearchWordAsync(word, filePath);
                MessageBox.Show($"The word '{word}' was found {count} times in the file.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async Task<int> SearchWordAsync(string word, string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                int count = 0;
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] words = line.ToLower().Split(new char[] { ' ', '\t', '\n', '\r', ',', '.', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string w in words)
                    {
                        if (w == word)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }
    }
}
