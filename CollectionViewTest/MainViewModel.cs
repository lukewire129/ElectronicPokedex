using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CollectionViewTest.api;
using CollectionViewTest.Models;
using System.Collections.ObjectModel;
namespace CollectionViewTest
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] ObservableCollection<PokeMonModel> pokeMonModels;
        [ObservableProperty] bool isLoading = false;
        IPokeMonApi _api;
        int nowindex = 0;
        public MainViewModel(IPokeMonApi api)
        {
            _api = api;
            this.PokeMonModels = new ();
            IsLoading = true;
            Task.Run (async () =>
            {
                foreach (var item in  await _api.GetPocketMons (nowindex))
                {
                    MainThread.BeginInvokeOnMainThread (() =>
                    {
                        this.PokeMonModels.Add (item);
                    });
                }
                IsLoading = false;
            });
        }
        [RelayCommand]
        private void aaa()
        {
            if (IsLoading == true)
                return;
            IsLoading = true;
            nowindex++;
            Task.Run (async () =>
            {
                foreach (var item in await _api.GetPocketMons (nowindex))
                {
                    MainThread.BeginInvokeOnMainThread (() =>
                    {
                        this.PokeMonModels.Add (item);
                    });
                }
                IsLoading = false;
            });
        }
    }
}
