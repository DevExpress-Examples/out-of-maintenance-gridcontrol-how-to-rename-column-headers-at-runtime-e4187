using DevExpress.Mvvm.DataAnnotations;
using System.Collections.ObjectModel;

namespace T686902.ViewModels {
    [POCOViewModel]
    public class MainViewModel {
        protected MainViewModel() {
            Items = new ObservableCollection<Item>();
            for (int i = 0; i < 10; i++)
                Items.Add(new Item() { ID = i, Name = "Item " + i });
        }
        public virtual ObservableCollection<Item> Items { get; set; }
    }

    public class Item {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}