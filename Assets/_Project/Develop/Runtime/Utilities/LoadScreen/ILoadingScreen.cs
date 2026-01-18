namespace _Project.Develop.Runtime.Utilities.LoadScreen
{
    public interface ILoadingScreen
    {
        bool IsShowing { get; }
        void Show();
        void Hide();
    }
}
