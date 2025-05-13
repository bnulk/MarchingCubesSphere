using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MarchingCubesSphere
{
    // Game 类继承自 OpenTK 提供的 GameWindow，这是应用的主窗口类
    internal class Game : GameWindow
    {
        // 顶部添加字段
        private VoxelVolume voxelVolume;

        // 构造函数，创建窗口，设置尺寸和标题
        public Game()
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),              // 设置窗口大小：800x600 像素
                Title = "Marching Cubes Sphere - Step 1"          // 设置窗口标题
            })
        { }

        // OnLoad：窗口加载时调用。用于初始化 OpenGL 设置
        protected override void OnLoad()
        {
            base.OnLoad();

            // 设置清屏颜色（背景色），RGBA 值：深蓝色
            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);

            // 启用深度测试（用于 3D 物体前后遮挡）
            GL.Enable(EnableCap.DepthTest);

            // 初始化体素网格，50³ 个体素，每个体素边长 0.1 单位
            voxelVolume = new VoxelVolume(50, 50, 50, 0.1f);
        }

        // OnRenderFrame：每帧渲染调用一次
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // 清除颜色缓冲区 和 深度缓冲区（准备开始渲染）
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // 后续我们将在这里添加 Marching Cubes 渲染逻辑

            // 交换前后台缓冲区，显示当前帧
            SwapBuffers();
        }

        // OnUpdateFrame：每帧更新调用一次（处理逻辑、输入）
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // 如果按下 Esc 键，则关闭窗口
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        // OnResize：窗口大小改变时调用
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // 更新视口大小为窗口新尺寸
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
    }
}
