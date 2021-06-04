using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GsoRetinalCameraSaver
{
    public partial class FrmNewFile : Form
    {
        public FrmNewFile(string filename)
        {
            InitializeComponent();
            Filename = filename;
            txtFilename.Text = filename;
            var patients = GetExistingPatients().ToArray();
            cmbSurname.Items.AddRange(patients.OrderBy(p => p.Lastname).Select(p => p.Lastname).Cast<object>().ToArray());
            cmbFirstname.Items.AddRange(patients.OrderBy(p => p.Firstname).Select(p => p.Firstname).Cast<object>().ToArray());

            FileInfo = new FileInfo(Filename);
            Patient = PatientName.Parse(FileInfo.Name);
            txtDate.Text = Patient.Created.ToString("F");
            cmbFirstname.Text = Patient.Firstname;
            cmbSurname.Text = Patient.Lastname;
        }

        public string Filename { get; }
        public FileInfo FileInfo { get; }
        public PatientName Patient { get; }

        public static IEnumerable<PatientName> GetExistingPatients()
        {
            var folders = Directory.EnumerateDirectories("R:\\");
            var rv = new List<PatientName>();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            
            foreach (var _ in folders)
            {
                var dir = new DirectoryInfo(_);
                PatientName rvs;
                string fn;
                string ln;
                string i="";
                if (dir.Name.Contains(","))
                {
                    ln = dir.Name.Substring(0, dir.Name.IndexOf(",") ).Trim().ToUpper();
                    ln = textInfo.ToTitleCase(ln);
                    fn = dir.Name.Substring(dir.Name.IndexOf(",")+1 , dir.Name.Length- dir.Name.IndexOf(",")-1).Trim();
                    fn = textInfo.ToTitleCase(fn);
                    if (rv.Any(p => p.Lastname == ln && p.Firstname == fn))
                    {
                        continue;
                    }
                    
                }
                else
                {
                    ln = dir.Name.ToUpper();
                    ln = textInfo.ToTitleCase(ln);
                    fn ="";
                    if (rv.Any(p => p.Lastname == ln && p.Firstname == fn))
                    {
                        continue;
                    }
                   
                }
                if (fn.Contains(" "))
                {
                    i = fn.Substring(fn.IndexOf(" ") + 1, fn.Length - fn.IndexOf(" ") - 1).Trim().ToUpper();
                    fn = fn.Substring(0, fn.IndexOf(" ")).Trim().ToUpper();
                    if (rv.Any(p => p.Lastname == ln && p.Firstname == fn))
                    {
                        continue;
                    }

                }
                rvs = new PatientName { Firstname = fn, Lastname = ln,Inital=i };
                rv.Add(rvs);
                yield return rvs;
            }
        }
       public struct PatientName
        {
            static Regex _nameRegex = new Regex("(?<last>[^_]+)[_](?<first>[^_]+)_(?<id>[^_]+)_(?<day>[^_]{2})(?<month>[^_]{2})(?<year>[^_]{4})_(?<end>.*)");
            public static PatientName Parse(string name)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                if (!_nameRegex.IsMatch(name)) return default(PatientName);
                var m = _nameRegex.Match(name);
               var Firstname = m.Groups["first"].Value;
                Firstname = textInfo.ToTitleCase(Firstname);
                var Lastname = m.Groups["last"].Value.ToUpper();
                Lastname = textInfo.ToTitleCase(Lastname);
                var Id = m.Groups["id"].Value;
                var Created = new DateTime(int.Parse(m.Groups["year"].Value), int.Parse(m.Groups["month"].Value), int.Parse(m.Groups["day"].Value));
                return new PatientName { Firstname = Firstname, Lastname = Lastname, Id = Id, Created = Created,Inital="" };

            }
            // LAST_FIRST_ID_DATECREATED_RL_SEQ
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Inital { get; set; }
            public string Id { get; set; }
            public DateTime Created { get; set; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        enum FolderStatus
        {
            New,
            Existing,
            Ambiguous
        }
        struct FolderName
        {
            public FolderStatus Status;
            public string Name;
        }
        FolderName GetPatientFolderName()
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            var rootfolder = "R:\\";
            var rv = new List<FolderName>();
            foreach(var _ in Directory.EnumerateDirectories(rootfolder))
            {
                var di = new DirectoryInfo(_);
                if (ManualFolderNameRegex.IsMatch(di.Name))
                {
                    var m = ManualFolderNameRegex.Match(di.Name);
                    var firstname = m.Groups["firstname"].Value.ToLower();
                    var surname = m.Groups["surname"].Value.ToLower();
                    var initial = m.Groups["initial"].Success? m.Groups["initial"].Value.ToLower():"";
                    if (firstname==cmbFirstname.Text.ToLower().Trim() && surname == cmbSurname.Text.ToLower().Trim() && initial == txtInitial.Text.ToLower().Trim())
                    {
                        var oath = Path.Combine(rootfolder, di.Name);
                        rv.Add(new  FolderName { Status = FolderStatus.Existing, Name = oath });
                    }
                }
            }
            if (!rv.Any())
            {
                var patientfolder = $"{textInfo.ToTitleCase(cmbSurname.Text.ToLower())}, {textInfo.ToTitleCase(cmbFirstname.Text)}"+(txtInitial.Text!=""?(" "+txtInitial.Text.Trim().ToUpper()):"");
                var oath2 = Path.Combine(rootfolder, patientfolder);
                rv.Add(new FolderName { Status = FolderStatus.New, Name = oath2 });
            }
            var folder = rv.FirstOrDefault();
            if (rv.Count > 1)
            {
                folder.Status = FolderStatus.Ambiguous;
            }
            return folder;

        }

        static Regex ManualFolderNameRegex = new Regex("^(?<surname>[a-zA-Z\\'\\-]+)[ ]*[\\,]?(?:(?:[ ]+)|(?:[ ]*[\\,][ ]*))(?<firstname>[a-zA-Z\\'\\-]+)(?:[ ]+(?<initial>.*))?$");
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFirstname.Text.Trim() == "")
                {
                    MessageBox.Show("You must enter a firstname");
                    return;
                }
                if (cmbSurname.Text.Trim() == "")
                {
                    MessageBox.Show("You must enter a surname");
                    return;
                }

                var patientFolder = GetPatientFolderName();





                if (!Directory.Exists(patientFolder.Name)) Directory.CreateDirectory(patientFolder.Name);
                var datefolder = Patient.Created.ToString("dd-MM-yyyy");
                var oath = Path.Combine(patientFolder.Name, datefolder);
                if (!Directory.Exists(oath)) Directory.CreateDirectory(oath);
                oath = Path.Combine(oath, FileInfo.Name);
                if (File.Exists(oath))
                {
                    var result = MessageBox.Show(this, "A file with this name already exists in this location. Click 'Yes' to replace this file, 'No' to delete the new file, or 'Cancel' to ignore.", "File already exists", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            File.Delete(oath);
                            Thread.Sleep(1000);
                            File.Move(FileInfo.FullName, oath);
                            break;
                        case DialogResult.No:
                            File.Delete(FileInfo.FullName);
                            Thread.Sleep(1000);
                            break;
                        case DialogResult.Cancel:
                            break;
                    }
                    return;
                }
                else
                {
                    File.Move(FileInfo.FullName, oath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
            finally
            {

                Close();
            }

        }
        void UpdateFolder()
        {
            var folder = GetPatientFolderName();
            switch (folder.Status)
            {
                case FolderStatus.Ambiguous:
                    txtFolder.Text = folder.Name + " (Ambiguous Folder)";
                    txtFolder.ForeColor = Color.White;
                    txtFolder.BackColor = Color.Red;
                    btnSave.Enabled = false;
                    btnMerge.Visible = true;
                    break;
                case FolderStatus.Existing:
                    txtFolder.Text = folder.Name ;
                    txtFolder.ForeColor = Color.Black;
                    txtFolder.BackColor = Color.White;
                    btnSave.Enabled = true;
                    btnMerge.Visible = false;
                    break;
                case FolderStatus.New:
                    txtFolder.Text = folder.Name + " (New Patient)";
                    txtFolder.ForeColor = Color.White;
                    txtFolder.BackColor = Color.Blue;
                    btnSave.Enabled = true;
                    btnMerge.Visible = false;
                    break;
            }
            
        }
      
        private void cmbSurname_TextChanged(object sender, EventArgs e)
        {
            UpdateFolder();
        }


        private void cmbFirstname_TextChanged(object sender, EventArgs e)
        {
            UpdateFolder();
        }
        void MoveFile(FileInfo file, DirectoryInfo dest)
        {
            if (dest.EnumerateFiles().Select(p => p.Name.ToLower()).Contains(file.Name.ToLower()))
            {
                var index = 2;
                var fn = file.Name.Substring(0,file.Name.Length-file.Extension.Length) + " (" + index + ")"+file.Extension;
                while(dest.EnumerateFiles().Select(p => p.Name.ToLower()).Contains(fn.ToLower())){
                    index++;
                    fn = file.Name.Substring(0, file.Name.Length - file.Extension.Length) + " (" + index + ")" + file.Extension;
                }
                File.Move(file.FullName, Path.Combine(dest.FullName, fn));
            }
            else
            {
                File.Move(file.FullName, Path.Combine(dest.FullName, file.Name));
            }
        }
        void MoveFolder(DirectoryInfo dir,DirectoryInfo dest)
        {
            if (dest.EnumerateDirectories().Select(p => p.Name.ToLower()).Contains(dir.Name.ToLower())){
                var existingDir = dest.EnumerateDirectories().First(p => p.Name.ToLower() == dir.Name.ToLower());
                foreach(var _ in dir.EnumerateDirectories())
                {
                    MoveFolder(_, existingDir);
                }
                foreach (var _ in dir.EnumerateFiles())
                {
                    MoveFile(_, existingDir);
                }
            }
            else
            {
                var existingDir = dest.CreateSubdirectory(dir.Name);
                foreach (var _ in dir.EnumerateDirectories())
                {
                    MoveFolder(_, existingDir);
                }
                foreach (var _ in dir.EnumerateFiles())
                {
                    MoveFile(_, existingDir);
                }
            }
            dir.Delete();
        }
        private void btnMerge_Click(object sender, EventArgs e)
        {
            var rootfolder = "R:\\"; 
            var tmpfolder = Path.Combine(rootfolder, Guid.NewGuid().ToString());

            Directory.CreateDirectory(tmpfolder);
            var tmpdir = new DirectoryInfo(tmpfolder);
            foreach (var _ in Directory.EnumerateDirectories(rootfolder))
            {
                var di = new DirectoryInfo(_);
                if (ManualFolderNameRegex.IsMatch(di.Name))
                {
                    var m = ManualFolderNameRegex.Match(di.Name);
                    var firstname = m.Groups["firstname"].Value.ToLower();
                    var surname = m.Groups["surname"].Value.ToLower();
                    var initial = m.Groups["initial"].Success ? m.Groups["initial"].Value.ToLower() : "";
                    if (firstname == cmbFirstname.Text.ToLower().Trim() && surname == cmbSurname.Text.ToLower().Trim() && initial == txtInitial.Text.ToLower().Trim())
                    {
                        foreach(var __ in di.EnumerateDirectories())
                        {
                            MoveFolder(__, tmpdir);
                        }
                        foreach (var __ in di.EnumerateFiles())
                        {
                            MoveFile(__, tmpdir);
                        }

                        di.Delete();
                      
                    }
                }
            }
            var newname = GetPatientFolderName();
            Directory.Move(tmpfolder, newname.Name);
            UpdateFolder();

           
        }

        private void txtInitial_TextChanged(object sender, EventArgs e)
        {
            UpdateFolder();

        }
    }
}
