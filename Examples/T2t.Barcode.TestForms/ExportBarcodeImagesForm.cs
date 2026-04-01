using System.ComponentModel;

namespace T2t.Barcode.TestForms;

public partial class ExportBarcodeImagesForm : Form
{
    public ExportBarcodeImagesForm()
    {
        InitializeComponent();
    }

    [DefaultValue(false)]
    public string RootFolder
    {
        get
        {
            return rootFolder.Text;
        }
        set
        {
            rootFolder.Text = value;
        }
    }

    [DefaultValue(false)]
    public bool OverwriteFiles
    {
        get
        {
            return overwriteExistingFiles.Checked;
        }
        set
        {
            overwriteExistingFiles.Checked = value;
        }
    }

    [DefaultValue(false)]
    public bool FlattenHierarchy
    {
        get
        {
            return flattenHiararchy.Checked;
        }
        set
        {
            flattenHiararchy.Checked = value;
        }
    }

    private void BrowseButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(rootFolder.Text.Trim()))
        {
            exportRootFolderBrowser.SelectedPath = rootFolder.Text;
        }
        else
        {
            exportRootFolderBrowser.RootFolder = Environment.SpecialFolder.Personal;
        }
        if (exportRootFolderBrowser.ShowDialog() == DialogResult.OK)
        {
            rootFolder.Text = exportRootFolderBrowser.SelectedPath;
        }
    }
}