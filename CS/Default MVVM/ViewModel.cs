using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DXWpfApplication
{
    //Model
    public class TestData
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }

    //Base View Model

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //View Model
    public class TestDataViewModel : BaseViewModel
    {

        TestData data;
        public TestDataViewModel(TestData data)
        {
            this.data = data;
        }

        public TestDataViewModel() : this(new TestData()) { }

        public string Text
        {
            get { return data.Text; }
            set
            {
                if (data.Text != value)
                {
                    data.Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public int Number
        {
            get { return data.Number; }
            set
            {
                if (data.Number != value)
                {
                    data.Number = value;
                    OnPropertyChanged("Number");
                }
            }
        }
    }

    //Views Model
    public class TestDataViewsModel : BaseViewModel
    {
        public TestDataViewsModel()
        {
            Records = new ObservableCollection<TestDataViewModel>();
            List<TestData> list = new List<TestData>();
            for (int i = 0; i < 10; i++)
                Records.Add(new TestDataViewModel(new TestData() { Text = "Row" + i, Number = i }));
        }

        ObservableCollection<TestDataViewModel> records;

        public ObservableCollection<TestDataViewModel> Records
        {
            get { return records; }
            set
            {
                if (records != value)
                {
                    records = value;
                    OnPropertyChanged("Records");
                }
            }
        }
    }
}
