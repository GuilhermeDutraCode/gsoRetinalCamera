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
            var folders = Directory.EnumerateDirectories(Properties.Settings.Default.FolderLocation);
            var rv = new List<PatientName>();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            
            foreach (var _ in folders)
            {
                var dir = new DirectoryInfo(_);
                PatientName rvs;
                if (dir.Name.Contains(","))
                {
                    var ln = dir.Name.Substring(0, dir.Name.IndexOf(",") ).Trim().ToUpper();
                    var fn = dir.Name.Substring(dir.Name.IndexOf(",")+1 , dir.Name.Length- dir.Name.IndexOf(",")-1).Trim();
                    fn = textInfo.ToTitleCase(fn);
                    if (rv.Any(p => p.Lastname == ln && p.Firstname == fn))
                    {
                        continue;
                    }
                    rvs = new PatientName { Firstname = fn, Lastname = ln };
                    rv.Add(rvs);
                    yield return rvs;
                }
                else
                {
                    var ln = dir.Name.ToUpper();
                    var fn ="";
                    if (rv.Any(p => p.Lastname == ln && p.Firstname == fn))
                    {
                        continue;
                    }
                    rvs = new PatientName { Firstname = fn, Lastname = ln };
                    rv.Add(rvs);
                    yield return rvs;
                }
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
                var Id = m.Groups["id"].Value;
                var Created = new DateTime(int.Parse(m.Groups["year"].Value), int.Parse(m.Groups["month"].Value), int.Parse(m.Groups["day"].Value));
                return new PatientName { Firstname = Firstname, Lastname = Lastname, Id = Id, Created = Created };

            }
            // LAST_FIRST_ID_DATECREATED_RL_SEQ
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Id { get; set; }
            public DateTime Created { get; set; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        struct FolderName
        {
            public bool IsNew;
            public string Name;
        }
        FolderName GetPatientFolderName()
        {
            var rootfolder = Properties.Settings.Default.FolderLocation;
            foreach(var _ in Directory.EnumerateDirectories(rootfolder))
            {
                var di = new DirectoryInfo(_);
                if (ManualFolderNameRegex.IsMatch(di.Name))
                {
                    var m = ManualFolderNameRegex.Match(di.Name);
                    var firstname = m.Groups["firstname"].Value.ToLower();
                    var surname = m.Groups["surname"].Value.ToLower();
                    if(firstname==cmbFirstname.Text.ToLower().Trim()&& surname == cmbSurname.Text.ToLower().Trim())
                    {
                        var oath = Path.Combine(rootfolder, di.Name);
                        return new FolderName { IsNew = false, Name = oath };
                    }
                }
            }
            var patientfolder = $"{cmbSurname.Text.ToUpper()}, {cmbFirstname.Text}";
            var oath2 = Path.Combine(rootfolder, patientfolder);
            return new FolderName { IsNew = true, Name = oath2 };
        }

        static Regex ManualFolderNameRegex = new Regex("^(?<surname>[a-zA-Z\\'\\-]+)[ ]*[\\,]?[ ]*(?<firstname>[a-zA-Z\\'\\-]+)");
        private void btnSave_Click(object sender, EventArgs e)
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
                MessageBox.Show(this,"A file with this name already exists in this location.", "File already exists", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            FileInfo.MoveTo(oath);
            Close();
        }
        void UpdateFolder()
        {
            var folder = GetPatientFolderName();
            txtFolder.Text = folder.Name + (folder.IsNew ? " (New)" : "");
        }
      
        private void cmbSurname_TextChanged(object sender, EventArgs e)
        {
            UpdateFolder();
        }


        private void cmbFirstname_TextChanged(object sender, EventArgs e)
        {
            UpdateFolder();
        }
    }
}
