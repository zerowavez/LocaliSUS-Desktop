using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Localimed.Model;
using Microsoft.Maui.Controls;

namespace Localimed.ModelView
{
    public class VisualizarEstoqueViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<EstoqueModel> EstoqueList { get; set; } = new ObservableCollection<EstoqueModel>();

        private EstoqueModel selectedEstoque;
        public EstoqueModel SelectedEstoque
        {
            get => selectedEstoque;
            set
            {
                if (selectedEstoque != value)
                {
                    selectedEstoque = value;
                    OnPropertyChanged(nameof(SelectedEstoque));
                    ((Command)BotaoRemoverMedicamento).ChangeCanExecute();
                }
            }
        }

        public ICommand BotaoInserirMedicamentos { get; }
        public ICommand BotaoRemoverMedicamento { get; }

        public VisualizarEstoqueViewModel()
        {
            BotaoInserirMedicamentos = new Command(OnBotaoInserirMedicamentosClicked);
            BotaoRemoverMedicamento = new Command(OnBotaoRemoverMedicamentosClicked, CanRemoverMedicamento);

            //MEDICAMENTO EXEMPLO
            EstoqueList.Add(new EstoqueModel { Quantidade = 10, Nome = "Ibuprofeno", ExigeTermo = false });
        }

        private void OnBotaoInserirMedicamentosClicked()
        {
            EstoqueList.Add(new EstoqueModel { Nome = "Novo Medicamento", Quantidade = 1, ExigeTermo = false });
        }

        private void OnBotaoRemoverMedicamentosClicked()
        {
            if (SelectedEstoque != null)
            {
                EstoqueList.Remove(SelectedEstoque);
                SelectedEstoque = null;
            }
        }

        private bool CanRemoverMedicamento()
        {
            return SelectedEstoque != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class EstoqueModel
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public bool ExigeTermo { get; set; }
    }
}
