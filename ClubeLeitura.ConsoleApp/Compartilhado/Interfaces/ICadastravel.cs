namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface ICadastravel
    {
        void InserirRegistro();
        void EditarRegistro();
        void ExcluirRegistro();
        bool VisualizarRegistros(string tipoVisualizado);
    }
}
