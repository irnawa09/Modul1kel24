using Kelompok24_Modul1.Model;
using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kelompok24_Modul1.View
{
    public class HalamanHapusData : ContentPage
    {
        private ListView _listView;
        private Entry _nama;
        private Entry _jurusan;
        private Button _hapus;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");
        DataMahasiswa dataMahasiswa = new DataMahasiswa();
        public HalamanHapusData()
        {
            this.Title = "Hapus Data";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _hapus = new Button();
            _hapus.Text = "Delete";
            _hapus.Clicked += _hapus_Clicked;
            stackLayout.Children.Add(_hapus);

            Content = stackLayout;
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            dataMahasiswa = (DataMahasiswa)e.SelectedItem;
        }

        private async void _hapus_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<DataMahasiswa>().Delete(x => x.Id == dataMahasiswa.Id);

            await DisplayAlert(null, "Data " + dataMahasiswa.Nama + " Berhasil Dihapus", "Ok");
            db.Delete(dataMahasiswa);
            
        }

    }
}
