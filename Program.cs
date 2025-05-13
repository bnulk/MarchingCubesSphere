namespace MarchingCubesSphere
{
    internal class Program
    {
        static void Main()
        {
            // 创建窗口实例并运行
            using var window = new Game();
            window.Run();   // 进入主循环，调用 OnLoad → OnUpdateFrame → OnRenderFrame
        }
    }
}
