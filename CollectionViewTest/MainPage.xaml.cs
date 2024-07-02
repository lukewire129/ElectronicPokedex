using CollectionViewTest.api;

namespace CollectionViewTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage(IPokeMonApi pokeMonApi)
        {
            InitializeComponent ();
            this.BindingContext = new MainViewModel (pokeMonApi);
        }
    }

}
